﻿// <auto-generated/>
#pragma warning disable
#nullable enable
namespace BigBazar.ViewModels
{
    /// <inheritdoc/>
    partial class BoxListPageViewModel
    {
        /// <inheritdoc cref="searchModes"/>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public global::System.Collections.IEnumerable SearchModes
        {
            get => searchModes;
            [global::System.Diagnostics.CodeAnalysis.MemberNotNull("searchModes")]
            set
            {
                if (!global::System.Collections.Generic.EqualityComparer<global::System.Collections.IEnumerable>.Default.Equals(searchModes, value))
                {
                    OnSearchModesChanging(value);
                    OnSearchModesChanging(default, value);
                    OnPropertyChanging(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangingArgs.SearchModes);
                    searchModes = value;
                    OnSearchModesChanged(value);
                    OnSearchModesChanged(default, value);
                    OnPropertyChanged(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.SearchModes);
                }
            }
        }

        /// <inheritdoc cref="areas"/>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public global::System.Collections.Generic.List<global::BigBazar.Models.Area> Areas
        {
            get => areas;
            [global::System.Diagnostics.CodeAnalysis.MemberNotNull("areas")]
            set
            {
                if (!global::System.Collections.Generic.EqualityComparer<global::System.Collections.Generic.List<global::BigBazar.Models.Area>>.Default.Equals(areas, value))
                {
                    OnAreasChanging(value);
                    OnAreasChanging(default, value);
                    OnPropertyChanging(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangingArgs.Areas);
                    areas = value;
                    OnAreasChanged(value);
                    OnAreasChanged(default, value);
                    OnPropertyChanged(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Areas);
                }
            }
        }

        /// <inheritdoc cref="selectedArea"/>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public global::BigBazar.Models.Area? SelectedArea
        {
            get => selectedArea;
            set
            {
                if (!global::System.Collections.Generic.EqualityComparer<global::BigBazar.Models.Area?>.Default.Equals(selectedArea, value))
                {
                    OnSelectedAreaChanging(value);
                    OnSelectedAreaChanging(default, value);
                    OnPropertyChanging(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangingArgs.SelectedArea);
                    selectedArea = value;
                    OnSelectedAreaChanged(value);
                    OnSelectedAreaChanged(default, value);
                    OnPropertyChanged(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.SelectedArea);
                }
            }
        }

        /// <inheritdoc cref="categories"/>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public global::System.Collections.Generic.List<global::BigBazar.Models.Category> Categories
        {
            get => categories;
            [global::System.Diagnostics.CodeAnalysis.MemberNotNull("categories")]
            set
            {
                if (!global::System.Collections.Generic.EqualityComparer<global::System.Collections.Generic.List<global::BigBazar.Models.Category>>.Default.Equals(categories, value))
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
        public global::BigBazar.Models.Category? SelectedCategory
        {
            get => selectedCategory;
            set
            {
                if (!global::System.Collections.Generic.EqualityComparer<global::BigBazar.Models.Category?>.Default.Equals(selectedCategory, value))
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

        /// <inheritdoc cref="searchResults"/>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public global::System.Collections.Generic.List<global::BigBazar.Models.Box> SearchResults
        {
            get => searchResults;
            [global::System.Diagnostics.CodeAnalysis.MemberNotNull("searchResults")]
            set
            {
                if (!global::System.Collections.Generic.EqualityComparer<global::System.Collections.Generic.List<global::BigBazar.Models.Box>>.Default.Equals(searchResults, value))
                {
                    OnSearchResultsChanging(value);
                    OnSearchResultsChanging(default, value);
                    OnPropertyChanging(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangingArgs.SearchResults);
                    searchResults = value;
                    OnSearchResultsChanged(value);
                    OnSearchResultsChanged(default, value);
                    OnPropertyChanged(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.SearchResults);
                }
            }
        }

        /// <inheritdoc cref="searchPlaceHolder"/>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public string SearchPlaceHolder
        {
            get => searchPlaceHolder;
            [global::System.Diagnostics.CodeAnalysis.MemberNotNull("searchPlaceHolder")]
            set
            {
                if (!global::System.Collections.Generic.EqualityComparer<string>.Default.Equals(searchPlaceHolder, value))
                {
                    OnSearchPlaceHolderChanging(value);
                    OnSearchPlaceHolderChanging(default, value);
                    OnPropertyChanging(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangingArgs.SearchPlaceHolder);
                    searchPlaceHolder = value;
                    OnSearchPlaceHolderChanged(value);
                    OnSearchPlaceHolderChanged(default, value);
                    OnPropertyChanged(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.SearchPlaceHolder);
                }
            }
        }

        /// <inheritdoc cref="currentSearchMode"/>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public global::BigBazar.Utils.BoxSearchMode CurrentSearchMode
        {
            get => currentSearchMode;
            set
            {
                if (!global::System.Collections.Generic.EqualityComparer<global::BigBazar.Utils.BoxSearchMode>.Default.Equals(currentSearchMode, value))
                {
                    global::BigBazar.Utils.BoxSearchMode __oldValue = currentSearchMode;
                    OnCurrentSearchModeChanging(value);
                    OnCurrentSearchModeChanging(__oldValue, value);
                    OnPropertyChanging(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangingArgs.CurrentSearchMode);
                    currentSearchMode = value;
                    OnCurrentSearchModeChanged(value);
                    OnCurrentSearchModeChanged(__oldValue, value);
                    OnPropertyChanged(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.CurrentSearchMode);
                }
            }
        }

        /// <inheritdoc cref="isDescriptionSearchMode"/>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public bool IsDescriptionSearchMode
        {
            get => isDescriptionSearchMode;
            set
            {
                if (!global::System.Collections.Generic.EqualityComparer<bool>.Default.Equals(isDescriptionSearchMode, value))
                {
                    OnIsDescriptionSearchModeChanging(value);
                    OnIsDescriptionSearchModeChanging(default, value);
                    OnPropertyChanging(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangingArgs.IsDescriptionSearchMode);
                    isDescriptionSearchMode = value;
                    OnIsDescriptionSearchModeChanged(value);
                    OnIsDescriptionSearchModeChanged(default, value);
                    OnPropertyChanged(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsDescriptionSearchMode);
                }
            }
        }

        /// <inheritdoc cref="isAreaSearchMode"/>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public bool IsAreaSearchMode
        {
            get => isAreaSearchMode;
            set
            {
                if (!global::System.Collections.Generic.EqualityComparer<bool>.Default.Equals(isAreaSearchMode, value))
                {
                    OnIsAreaSearchModeChanging(value);
                    OnIsAreaSearchModeChanging(default, value);
                    OnPropertyChanging(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangingArgs.IsAreaSearchMode);
                    isAreaSearchMode = value;
                    OnIsAreaSearchModeChanged(value);
                    OnIsAreaSearchModeChanged(default, value);
                    OnPropertyChanged(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsAreaSearchMode);
                }
            }
        }

        /// <inheritdoc cref="isCategorySearchMode"/>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public bool IsCategorySearchMode
        {
            get => isCategorySearchMode;
            set
            {
                if (!global::System.Collections.Generic.EqualityComparer<bool>.Default.Equals(isCategorySearchMode, value))
                {
                    OnIsCategorySearchModeChanging(value);
                    OnIsCategorySearchModeChanging(default, value);
                    OnPropertyChanging(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangingArgs.IsCategorySearchMode);
                    isCategorySearchMode = value;
                    OnIsCategorySearchModeChanged(value);
                    OnIsCategorySearchModeChanged(default, value);
                    OnPropertyChanged(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsCategorySearchMode);
                }
            }
        }

        /// <summary>Executes the logic for when <see cref="SearchModes"/> is changing.</summary>
        /// <param name="value">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="SearchModes"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSearchModesChanging(global::System.Collections.IEnumerable value);
        /// <summary>Executes the logic for when <see cref="SearchModes"/> is changing.</summary>
        /// <param name="oldValue">The previous property value that is being replaced.</param>
        /// <param name="newValue">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="SearchModes"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSearchModesChanging(global::System.Collections.IEnumerable? oldValue, global::System.Collections.IEnumerable newValue);
        /// <summary>Executes the logic for when <see cref="SearchModes"/> just changed.</summary>
        /// <param name="value">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="SearchModes"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSearchModesChanged(global::System.Collections.IEnumerable value);
        /// <summary>Executes the logic for when <see cref="SearchModes"/> just changed.</summary>
        /// <param name="oldValue">The previous property value that was replaced.</param>
        /// <param name="newValue">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="SearchModes"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSearchModesChanged(global::System.Collections.IEnumerable? oldValue, global::System.Collections.IEnumerable newValue);
        /// <summary>Executes the logic for when <see cref="Areas"/> is changing.</summary>
        /// <param name="value">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="Areas"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnAreasChanging(global::System.Collections.Generic.List<global::BigBazar.Models.Area> value);
        /// <summary>Executes the logic for when <see cref="Areas"/> is changing.</summary>
        /// <param name="oldValue">The previous property value that is being replaced.</param>
        /// <param name="newValue">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="Areas"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnAreasChanging(global::System.Collections.Generic.List<global::BigBazar.Models.Area>? oldValue, global::System.Collections.Generic.List<global::BigBazar.Models.Area> newValue);
        /// <summary>Executes the logic for when <see cref="Areas"/> just changed.</summary>
        /// <param name="value">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="Areas"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnAreasChanged(global::System.Collections.Generic.List<global::BigBazar.Models.Area> value);
        /// <summary>Executes the logic for when <see cref="Areas"/> just changed.</summary>
        /// <param name="oldValue">The previous property value that was replaced.</param>
        /// <param name="newValue">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="Areas"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnAreasChanged(global::System.Collections.Generic.List<global::BigBazar.Models.Area>? oldValue, global::System.Collections.Generic.List<global::BigBazar.Models.Area> newValue);
        /// <summary>Executes the logic for when <see cref="SelectedArea"/> is changing.</summary>
        /// <param name="value">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="SelectedArea"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSelectedAreaChanging(global::BigBazar.Models.Area? value);
        /// <summary>Executes the logic for when <see cref="SelectedArea"/> is changing.</summary>
        /// <param name="oldValue">The previous property value that is being replaced.</param>
        /// <param name="newValue">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="SelectedArea"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSelectedAreaChanging(global::BigBazar.Models.Area? oldValue, global::BigBazar.Models.Area? newValue);
        /// <summary>Executes the logic for when <see cref="SelectedArea"/> just changed.</summary>
        /// <param name="value">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="SelectedArea"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSelectedAreaChanged(global::BigBazar.Models.Area? value);
        /// <summary>Executes the logic for when <see cref="SelectedArea"/> just changed.</summary>
        /// <param name="oldValue">The previous property value that was replaced.</param>
        /// <param name="newValue">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="SelectedArea"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSelectedAreaChanged(global::BigBazar.Models.Area? oldValue, global::BigBazar.Models.Area? newValue);
        /// <summary>Executes the logic for when <see cref="Categories"/> is changing.</summary>
        /// <param name="value">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="Categories"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnCategoriesChanging(global::System.Collections.Generic.List<global::BigBazar.Models.Category> value);
        /// <summary>Executes the logic for when <see cref="Categories"/> is changing.</summary>
        /// <param name="oldValue">The previous property value that is being replaced.</param>
        /// <param name="newValue">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="Categories"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnCategoriesChanging(global::System.Collections.Generic.List<global::BigBazar.Models.Category>? oldValue, global::System.Collections.Generic.List<global::BigBazar.Models.Category> newValue);
        /// <summary>Executes the logic for when <see cref="Categories"/> just changed.</summary>
        /// <param name="value">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="Categories"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnCategoriesChanged(global::System.Collections.Generic.List<global::BigBazar.Models.Category> value);
        /// <summary>Executes the logic for when <see cref="Categories"/> just changed.</summary>
        /// <param name="oldValue">The previous property value that was replaced.</param>
        /// <param name="newValue">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="Categories"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnCategoriesChanged(global::System.Collections.Generic.List<global::BigBazar.Models.Category>? oldValue, global::System.Collections.Generic.List<global::BigBazar.Models.Category> newValue);
        /// <summary>Executes the logic for when <see cref="SelectedCategory"/> is changing.</summary>
        /// <param name="value">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="SelectedCategory"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSelectedCategoryChanging(global::BigBazar.Models.Category? value);
        /// <summary>Executes the logic for when <see cref="SelectedCategory"/> is changing.</summary>
        /// <param name="oldValue">The previous property value that is being replaced.</param>
        /// <param name="newValue">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="SelectedCategory"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSelectedCategoryChanging(global::BigBazar.Models.Category? oldValue, global::BigBazar.Models.Category? newValue);
        /// <summary>Executes the logic for when <see cref="SelectedCategory"/> just changed.</summary>
        /// <param name="value">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="SelectedCategory"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSelectedCategoryChanged(global::BigBazar.Models.Category? value);
        /// <summary>Executes the logic for when <see cref="SelectedCategory"/> just changed.</summary>
        /// <param name="oldValue">The previous property value that was replaced.</param>
        /// <param name="newValue">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="SelectedCategory"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSelectedCategoryChanged(global::BigBazar.Models.Category? oldValue, global::BigBazar.Models.Category? newValue);
        /// <summary>Executes the logic for when <see cref="SearchResults"/> is changing.</summary>
        /// <param name="value">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="SearchResults"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSearchResultsChanging(global::System.Collections.Generic.List<global::BigBazar.Models.Box> value);
        /// <summary>Executes the logic for when <see cref="SearchResults"/> is changing.</summary>
        /// <param name="oldValue">The previous property value that is being replaced.</param>
        /// <param name="newValue">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="SearchResults"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSearchResultsChanging(global::System.Collections.Generic.List<global::BigBazar.Models.Box>? oldValue, global::System.Collections.Generic.List<global::BigBazar.Models.Box> newValue);
        /// <summary>Executes the logic for when <see cref="SearchResults"/> just changed.</summary>
        /// <param name="value">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="SearchResults"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSearchResultsChanged(global::System.Collections.Generic.List<global::BigBazar.Models.Box> value);
        /// <summary>Executes the logic for when <see cref="SearchResults"/> just changed.</summary>
        /// <param name="oldValue">The previous property value that was replaced.</param>
        /// <param name="newValue">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="SearchResults"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSearchResultsChanged(global::System.Collections.Generic.List<global::BigBazar.Models.Box>? oldValue, global::System.Collections.Generic.List<global::BigBazar.Models.Box> newValue);
        /// <summary>Executes the logic for when <see cref="SearchPlaceHolder"/> is changing.</summary>
        /// <param name="value">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="SearchPlaceHolder"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSearchPlaceHolderChanging(string value);
        /// <summary>Executes the logic for when <see cref="SearchPlaceHolder"/> is changing.</summary>
        /// <param name="oldValue">The previous property value that is being replaced.</param>
        /// <param name="newValue">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="SearchPlaceHolder"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSearchPlaceHolderChanging(string? oldValue, string newValue);
        /// <summary>Executes the logic for when <see cref="SearchPlaceHolder"/> just changed.</summary>
        /// <param name="value">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="SearchPlaceHolder"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSearchPlaceHolderChanged(string value);
        /// <summary>Executes the logic for when <see cref="SearchPlaceHolder"/> just changed.</summary>
        /// <param name="oldValue">The previous property value that was replaced.</param>
        /// <param name="newValue">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="SearchPlaceHolder"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnSearchPlaceHolderChanged(string? oldValue, string newValue);
        /// <summary>Executes the logic for when <see cref="CurrentSearchMode"/> is changing.</summary>
        /// <param name="value">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="CurrentSearchMode"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnCurrentSearchModeChanging(global::BigBazar.Utils.BoxSearchMode value);
        /// <summary>Executes the logic for when <see cref="CurrentSearchMode"/> is changing.</summary>
        /// <param name="oldValue">The previous property value that is being replaced.</param>
        /// <param name="newValue">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="CurrentSearchMode"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnCurrentSearchModeChanging(global::BigBazar.Utils.BoxSearchMode oldValue, global::BigBazar.Utils.BoxSearchMode newValue);
        /// <summary>Executes the logic for when <see cref="CurrentSearchMode"/> just changed.</summary>
        /// <param name="value">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="CurrentSearchMode"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnCurrentSearchModeChanged(global::BigBazar.Utils.BoxSearchMode value);
        /// <summary>Executes the logic for when <see cref="CurrentSearchMode"/> just changed.</summary>
        /// <param name="oldValue">The previous property value that was replaced.</param>
        /// <param name="newValue">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="CurrentSearchMode"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnCurrentSearchModeChanged(global::BigBazar.Utils.BoxSearchMode oldValue, global::BigBazar.Utils.BoxSearchMode newValue);
        /// <summary>Executes the logic for when <see cref="IsDescriptionSearchMode"/> is changing.</summary>
        /// <param name="value">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="IsDescriptionSearchMode"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnIsDescriptionSearchModeChanging(bool value);
        /// <summary>Executes the logic for when <see cref="IsDescriptionSearchMode"/> is changing.</summary>
        /// <param name="oldValue">The previous property value that is being replaced.</param>
        /// <param name="newValue">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="IsDescriptionSearchMode"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnIsDescriptionSearchModeChanging(bool oldValue, bool newValue);
        /// <summary>Executes the logic for when <see cref="IsDescriptionSearchMode"/> just changed.</summary>
        /// <param name="value">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="IsDescriptionSearchMode"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnIsDescriptionSearchModeChanged(bool value);
        /// <summary>Executes the logic for when <see cref="IsDescriptionSearchMode"/> just changed.</summary>
        /// <param name="oldValue">The previous property value that was replaced.</param>
        /// <param name="newValue">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="IsDescriptionSearchMode"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnIsDescriptionSearchModeChanged(bool oldValue, bool newValue);
        /// <summary>Executes the logic for when <see cref="IsAreaSearchMode"/> is changing.</summary>
        /// <param name="value">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="IsAreaSearchMode"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnIsAreaSearchModeChanging(bool value);
        /// <summary>Executes the logic for when <see cref="IsAreaSearchMode"/> is changing.</summary>
        /// <param name="oldValue">The previous property value that is being replaced.</param>
        /// <param name="newValue">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="IsAreaSearchMode"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnIsAreaSearchModeChanging(bool oldValue, bool newValue);
        /// <summary>Executes the logic for when <see cref="IsAreaSearchMode"/> just changed.</summary>
        /// <param name="value">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="IsAreaSearchMode"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnIsAreaSearchModeChanged(bool value);
        /// <summary>Executes the logic for when <see cref="IsAreaSearchMode"/> just changed.</summary>
        /// <param name="oldValue">The previous property value that was replaced.</param>
        /// <param name="newValue">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="IsAreaSearchMode"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnIsAreaSearchModeChanged(bool oldValue, bool newValue);
        /// <summary>Executes the logic for when <see cref="IsCategorySearchMode"/> is changing.</summary>
        /// <param name="value">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="IsCategorySearchMode"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnIsCategorySearchModeChanging(bool value);
        /// <summary>Executes the logic for when <see cref="IsCategorySearchMode"/> is changing.</summary>
        /// <param name="oldValue">The previous property value that is being replaced.</param>
        /// <param name="newValue">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="IsCategorySearchMode"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnIsCategorySearchModeChanging(bool oldValue, bool newValue);
        /// <summary>Executes the logic for when <see cref="IsCategorySearchMode"/> just changed.</summary>
        /// <param name="value">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="IsCategorySearchMode"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnIsCategorySearchModeChanged(bool value);
        /// <summary>Executes the logic for when <see cref="IsCategorySearchMode"/> just changed.</summary>
        /// <param name="oldValue">The previous property value that was replaced.</param>
        /// <param name="newValue">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="IsCategorySearchMode"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnIsCategorySearchModeChanged(bool oldValue, bool newValue);
    }
}