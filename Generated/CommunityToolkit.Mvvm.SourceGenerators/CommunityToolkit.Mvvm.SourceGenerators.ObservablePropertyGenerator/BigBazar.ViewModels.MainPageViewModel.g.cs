﻿// <auto-generated/>
#pragma warning disable
#nullable enable
namespace BigBazar.ViewModels
{
    /// <inheritdoc/>
    partial class MainPageViewModel
    {
        /// <inheritdoc cref="showSettings"/>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public bool ShowSettings
        {
            get => showSettings;
            set
            {
                if (!global::System.Collections.Generic.EqualityComparer<bool>.Default.Equals(showSettings, value))
                {
                    OnShowSettingsChanging(value);
                    OnShowSettingsChanging(default, value);
                    OnPropertyChanging(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangingArgs.ShowSettings);
                    showSettings = value;
                    OnShowSettingsChanged(value);
                    OnShowSettingsChanged(default, value);
                    OnPropertyChanged(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShowSettings);
                }
            }
        }

        /// <summary>Executes the logic for when <see cref="ShowSettings"/> is changing.</summary>
        /// <param name="value">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="ShowSettings"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnShowSettingsChanging(bool value);
        /// <summary>Executes the logic for when <see cref="ShowSettings"/> is changing.</summary>
        /// <param name="oldValue">The previous property value that is being replaced.</param>
        /// <param name="newValue">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="ShowSettings"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnShowSettingsChanging(bool oldValue, bool newValue);
        /// <summary>Executes the logic for when <see cref="ShowSettings"/> just changed.</summary>
        /// <param name="value">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="ShowSettings"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnShowSettingsChanged(bool value);
        /// <summary>Executes the logic for when <see cref="ShowSettings"/> just changed.</summary>
        /// <param name="oldValue">The previous property value that was replaced.</param>
        /// <param name="newValue">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="ShowSettings"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnShowSettingsChanged(bool oldValue, bool newValue);
    }
}