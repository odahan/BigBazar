﻿// <auto-generated/>
#pragma warning disable
#nullable enable
namespace BigBazar.ViewModels
{
    /// <inheritdoc/>
    partial class CategoryPageViewModel
    {
        /// <inheritdoc cref="categories"/>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public global::System.Collections.ObjectModel.ObservableCollection<global::BigBazar.Models.Category> Categories
        {
            get => categories;
            [global::System.Diagnostics.CodeAnalysis.MemberNotNull("categories")]
            set
            {
                if (!global::System.Collections.Generic.EqualityComparer<global::System.Collections.ObjectModel.ObservableCollection<global::BigBazar.Models.Category>>.Default.Equals(categories, value))
                {
                    OnCategoriesChanging(value);
                    OnCategoriesChanging(default, value);
                    OnPropertyChanging(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangingArgs.Categories);
                    categories = value;
                    OnCategoriesChanged(value);
                    OnCategoriesChanged(default, value);
                    OnPropertyChanged(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Categories);
                }
            }
        }

        /// <inheritdoc cref="selectedCategory"/>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public global::BigBazar.Models.Category SelectedCategory
        {
            get => selectedCategory;
            [global::System.Diagnostics.CodeAnalysis.MemberNotNull("selectedCategory")]
            set
            {
                if (!global::System.Collections.Generic.EqualityComparer<global::BigBazar.Models.Category>.Default.Equals(selectedCategory, value))
                {
                    OnSelectedCategoryChanging(value);
                    OnSelectedCategoryChanging(default, value);
                    OnPropertyChanging(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangingArgs.SelectedCategory);
                    selectedCategory = value;
                    OnSelectedCategoryChanged(value);
                    OnSelectedCategoryChanged(default, value);
                    OnPropertyChanged(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.SelectedCategory);
                }
            }
        }

        /// <summary>Executes the logic for when <see cref="Categories"/> is changing.</summary>
        /// <param name="value">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="Categories"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnCategoriesChanging(global::System.Collections.ObjectModel.ObservableCollection<global::BigBazar.Models.Category> value);
        /// <summary>Executes the logic for when <see cref="Categories"/> is changing.</summary>
        /// <param name="oldValue">The previous property value that is being replaced.</param>
        /// <param name="newValue">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="Categories"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnCategoriesChanging(global::System.Collections.ObjectModel.ObservableCollection<global::BigBazar.Models.Category>? oldValue, global::System.Collections.ObjectModel.ObservableCollection<global::BigBazar.Models.Category> newValue);
        /// <summary>Executes the logic for when <see cref="Categories"/> just changed.</summary>
        /// <param name="value">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="Categories"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnCategoriesChanged(global::System.Collections.ObjectModel.ObservableCollection<global::BigBazar.Models.Category> value);
        /// <summary>Executes the logic for when <see cref="Categories"/> just changed.</summary>
        /// <param name="oldValue">The previous property value that was replaced.</param>
        /// <param name="newValue">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="Categories"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnCategoriesChanged(global::System.Collections.ObjectModel.ObservableCollection<global::BigBazar.Models.Category>? oldValue, global::System.Collections.ObjectModel.ObservableCollection<global::BigBazar.Models.Category> newValue);
        /// <summary>Executes the logic for when <see cref="SelectedCategory"/> is changing.</summary>
        /// <param name="value">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="SelectedCategory"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSelectedCategoryChanging(global::BigBazar.Models.Category value);
        /// <summary>Executes the logic for when <see cref="SelectedCategory"/> is changing.</summary>
        /// <param name="oldValue">The previous property value that is being replaced.</param>
        /// <param name="newValue">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="SelectedCategory"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSelectedCategoryChanging(global::BigBazar.Models.Category? oldValue, global::BigBazar.Models.Category newValue);
        /// <summary>Executes the logic for when <see cref="SelectedCategory"/> just changed.</summary>
        /// <param name="value">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="SelectedCategory"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSelectedCategoryChanged(global::BigBazar.Models.Category value);
        /// <summary>Executes the logic for when <see cref="SelectedCategory"/> just changed.</summary>
        /// <param name="oldValue">The previous property value that was replaced.</param>
        /// <param name="newValue">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="SelectedCategory"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSelectedCategoryChanged(global::BigBazar.Models.Category? oldValue, global::BigBazar.Models.Category newValue);
    }
}