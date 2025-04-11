using System;
using System.IO;
using Cysharp.Threading.Tasks;
using ServiceLocator;
using UnityEngine;

namespace TestInventory
{
    public class SaveManager : IService
    {
        private const string SaveName = "Save";
        private const string SaveExtension = "json";
        private readonly string _gameDataPath;
        private readonly string _saveDataPath;
        private readonly string _saveFilePath;

        public SaveManager()
        {
#if UNITY_EDITOR
            _gameDataPath =  Application.dataPath;
#else
            _gameDataPath = Application.persistentDataPath;
#endif
            _saveDataPath = string.Concat(_gameDataPath, "/SaveData");
            _saveFilePath = string.Concat(_saveDataPath, "/", SaveName, $".{SaveExtension}");
        }
        
        public async UniTask Save(SaveState data)
        {
            await SerializeData(data);
        }

        public async UniTask<SaveState> Load()
        {
            if (!SaveExists())
                throw new Exception($"Save not found.");
            var data = await DeserializeData();
            return data;
        }
        
        public async UniTask<SaveState> LoadOrDefault()
        {
            if (!SaveExists())
                await SerializeData(new());
            return await Load();
        }

        private bool SaveExists()
        {
            return File.Exists(_saveFilePath);
        }

        private async UniTask SerializeData(SaveState data)
        {
            var jsonData = JsonUtility.ToJson(data, true);
            Directory.CreateDirectory(_saveDataPath);
            await using var stream = File.CreateText(_saveFilePath);
            await stream.WriteAsync(jsonData);
        }

        private async UniTask<SaveState> DeserializeData()
        {
            var jsonData = default(string);
            using var stream = File.OpenText(_saveFilePath);
            jsonData = await stream.ReadToEndAsync();
            return JsonUtility.FromJson<SaveState>(jsonData) ?? new SaveState();
        }
    }
}