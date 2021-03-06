﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Practices.ServiceLocation;
using UtilityLib.Mvvm;

namespace LinkBar.Model {
    public class Link : ValidatableEntity {
        #region Name Binding
        private string _Name;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Link name must not be empty!")]
        [StringLength(256, ErrorMessage = "Maximum allowed characters: 256")]
        public string Name {
            get { return _Name; }
            set {
                if (_Name == value)
                    return;

                _Name = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Url Binding
        private string _Url;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Link adress must not be empty!")]
        [StringLength(512, ErrorMessage = "Maximum allowed characters: 256")]
        public string Url {
            get { return _Url; }
            set {
                if (_Url == value)
                    return;

                _Url = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Description Binding
        private string _Description;
        [StringLength(512, ErrorMessage = "Maximum allowed characters: 512")]
        public string Description {
            get { return _Description; }
            set {
                if (_Description == value)
                    return;

                _Description = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public Int64 CategoryId { get; set; }
        #region Category Binding
        private Category _Category = null;
        /// <summary>
        /// Sets and gets the Categories property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public virtual Category Category {
            get { return _Category; }
            set {
                _Category = value;
                if (value != null && value.Id > 0)
                    CategoryId = value.Id;
                else
                    CategoryId = 0;

                RaisePropertyChanged();
            }
        }
        #endregion

        
        [InverseProperty("AssociatedLink")]
        public virtual List<Note> Notes { get; set; }
        public virtual ObservableCollection<Tag> Tags { get; set; }

        public Link() {
            Tags = new ObservableCollection<Tag>();
            Notes = new List<Note>();
        }

        internal Link Clone() {
            var link = new Link {
                Id = Id,
                Name = Name,
                Url = Url,
                Category = Category,
                Tags = new ObservableCollection<Tag>(Tags)
            };
            return link;
        }
    }
}