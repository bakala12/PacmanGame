﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PacmanGame.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<Key>Left</Key>")]
        public global::System.Windows.Input.Key LeftKey {
            get {
                return ((global::System.Windows.Input.Key)(this["LeftKey"]));
            }
            set {
                this["LeftKey"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<Key>Right</Key>")]
        public global::System.Windows.Input.Key RightKey {
            get {
                return ((global::System.Windows.Input.Key)(this["RightKey"]));
            }
            set {
                this["RightKey"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<Key>Up</Key>")]
        public global::System.Windows.Input.Key UpKey {
            get {
                return ((global::System.Windows.Input.Key)(this["UpKey"]));
            }
            set {
                this["UpKey"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<Key>Down</Key>")]
        public global::System.Windows.Input.Key DownKey {
            get {
                return ((global::System.Windows.Input.Key)(this["DownKey"]));
            }
            set {
                this["DownKey"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("500")]
        public uint EnemyMovementInterval {
            get {
                return ((uint)(this["EnemyMovementInterval"]));
            }
            set {
                this["EnemyMovementInterval"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Collections.Generic.List<PacmanGame.Highscores.Highscore> Highscores {
            get {
                return ((global::System.Collections.Generic.List<PacmanGame.Highscores.Highscore>)(this["Highscores"]));
            }
            set {
                this["Highscores"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public uint RememberedHighscoresCount {
            get {
                return ((uint)(this["RememberedHighscoresCount"]));
            }
            set {
                this["RememberedHighscoresCount"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<Key>Escape</Key>")]
        public global::System.Windows.Input.Key PauseKey {
            get {
                return ((global::System.Windows.Input.Key)(this["PauseKey"]));
            }
            set {
                this["PauseKey"] = value;
            }
        }
    }
}
