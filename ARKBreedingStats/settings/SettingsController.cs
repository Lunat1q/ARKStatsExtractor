﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARKBreedingStats.settings
{
    public class SettingsController<T> where T : class, new()
    {
        private T _settings;
        public string StorageFolder { get; set; } = "Config";

        public string ConfigFileName { get; set; } = "settings.config";

        private string AssemblyDirectory { get; }

        private string ConfigPath => Path.Combine(this.AssemblyDirectory, this.StorageFolder, this.ConfigFileName);

        public T Settings
        {
            get => this._settings ?? (this._settings = this.Initialize());
            private set => this._settings = value;
        }

        public SettingsController(string assemblyDirectory)
        {
            this.AssemblyDirectory = assemblyDirectory;
        }

        public T Initialize()
        {
            return JsonSerialization.DeserializeDataFromFile<T>(this.ConfigPath) ?? new T();
        }

        public void Save()
        {
            this.Settings.SerializeDataJson(this.ConfigPath);
        }

        public void Reset()
        {
            this.Settings = new T();
        }
    }
}
