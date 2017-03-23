﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBConnector
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="fujitsu")]
	public partial class FujitsuConnectorDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void Insertsdk(sdk instance);
    partial void Updatesdk(sdk instance);
    partial void Deletesdk(sdk instance);
    partial void Insertsdk_map(sdk_map instance);
    partial void Updatesdk_map(sdk_map instance);
    partial void Deletesdk_map(sdk_map instance);
    #endregion
		
		public FujitsuConnectorDataContext() : 
				base(global::DBConnector.Properties.Settings.Default.fujitsuConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public FujitsuConnectorDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FujitsuConnectorDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FujitsuConnectorDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FujitsuConnectorDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<sdk> sdks
		{
			get
			{
				return this.GetTable<sdk>();
			}
		}
		
		public System.Data.Linq.Table<sdk_map> sdk_maps
		{
			get
			{
				return this.GetTable<sdk_map>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.sdk")]
	public partial class sdk : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _name;
		
		private string _output_path;
		
		private EntitySet<sdk_map> _sdk_maps;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnnameChanging(string value);
    partial void OnnameChanged();
    partial void Onoutput_pathChanging(string value);
    partial void Onoutput_pathChanged();
    #endregion
		
		public sdk()
		{
			this._sdk_maps = new EntitySet<sdk_map>(new Action<sdk_map>(this.attach_sdk_maps), new Action<sdk_map>(this.detach_sdk_maps));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_name", DbType="VarChar(896)")]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this.OnnameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("name");
					this.OnnameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_output_path", DbType="VarChar(MAX)")]
		public string output_path
		{
			get
			{
				return this._output_path;
			}
			set
			{
				if ((this._output_path != value))
				{
					this.Onoutput_pathChanging(value);
					this.SendPropertyChanging();
					this._output_path = value;
					this.SendPropertyChanged("output_path");
					this.Onoutput_pathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="sdk_sdk_map", Storage="_sdk_maps", ThisKey="id", OtherKey="sdk_id")]
		public EntitySet<sdk_map> sdk_maps
		{
			get
			{
				return this._sdk_maps;
			}
			set
			{
				this._sdk_maps.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_sdk_maps(sdk_map entity)
		{
			this.SendPropertyChanging();
			entity.sdk = this;
		}
		
		private void detach_sdk_maps(sdk_map entity)
		{
			this.SendPropertyChanging();
			entity.sdk = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.sdk_map")]
	public partial class sdk_map : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _model_identifier;
		
		private int _sdk_id;
		
		private string _old_classname;
		
		private string _new_classname;
		
		private string _old_assembly_path;
		
		private string _new_assembly_path;
		
		private string _old_namespace;
		
		private string _new_namespace;
		
		private string _new_assembly_full_name;
		
		private EntityRef<sdk> _sdk;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void Onmodel_identifierChanging(string value);
    partial void Onmodel_identifierChanged();
    partial void Onsdk_idChanging(int value);
    partial void Onsdk_idChanged();
    partial void Onold_classnameChanging(string value);
    partial void Onold_classnameChanged();
    partial void Onnew_classnameChanging(string value);
    partial void Onnew_classnameChanged();
    partial void Onold_assembly_pathChanging(string value);
    partial void Onold_assembly_pathChanged();
    partial void Onnew_assembly_pathChanging(string value);
    partial void Onnew_assembly_pathChanged();
    partial void Onold_namespaceChanging(string value);
    partial void Onold_namespaceChanged();
    partial void Onnew_namespaceChanging(string value);
    partial void Onnew_namespaceChanged();
    partial void Onnew_assembly_full_nameChanging(string value);
    partial void Onnew_assembly_full_nameChanged();
    #endregion
		
		public sdk_map()
		{
			this._sdk = default(EntityRef<sdk>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_model_identifier", DbType="VarChar(255) NOT NULL", CanBeNull=false)]
		public string model_identifier
		{
			get
			{
				return this._model_identifier;
			}
			set
			{
				if ((this._model_identifier != value))
				{
					this.Onmodel_identifierChanging(value);
					this.SendPropertyChanging();
					this._model_identifier = value;
					this.SendPropertyChanged("model_identifier");
					this.Onmodel_identifierChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_sdk_id", DbType="Int NOT NULL")]
		public int sdk_id
		{
			get
			{
				return this._sdk_id;
			}
			set
			{
				if ((this._sdk_id != value))
				{
					if (this._sdk.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.Onsdk_idChanging(value);
					this.SendPropertyChanging();
					this._sdk_id = value;
					this.SendPropertyChanged("sdk_id");
					this.Onsdk_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_old_classname", DbType="VarChar(MAX)")]
		public string old_classname
		{
			get
			{
				return this._old_classname;
			}
			set
			{
				if ((this._old_classname != value))
				{
					this.Onold_classnameChanging(value);
					this.SendPropertyChanging();
					this._old_classname = value;
					this.SendPropertyChanged("old_classname");
					this.Onold_classnameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_new_classname", DbType="VarChar(MAX)")]
		public string new_classname
		{
			get
			{
				return this._new_classname;
			}
			set
			{
				if ((this._new_classname != value))
				{
					this.Onnew_classnameChanging(value);
					this.SendPropertyChanging();
					this._new_classname = value;
					this.SendPropertyChanged("new_classname");
					this.Onnew_classnameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_old_assembly_path", DbType="VarChar(MAX)")]
		public string old_assembly_path
		{
			get
			{
				return this._old_assembly_path;
			}
			set
			{
				if ((this._old_assembly_path != value))
				{
					this.Onold_assembly_pathChanging(value);
					this.SendPropertyChanging();
					this._old_assembly_path = value;
					this.SendPropertyChanged("old_assembly_path");
					this.Onold_assembly_pathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_new_assembly_path", DbType="VarChar(MAX)")]
		public string new_assembly_path
		{
			get
			{
				return this._new_assembly_path;
			}
			set
			{
				if ((this._new_assembly_path != value))
				{
					this.Onnew_assembly_pathChanging(value);
					this.SendPropertyChanging();
					this._new_assembly_path = value;
					this.SendPropertyChanged("new_assembly_path");
					this.Onnew_assembly_pathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_old_namespace", DbType="VarChar(MAX)")]
		public string old_namespace
		{
			get
			{
				return this._old_namespace;
			}
			set
			{
				if ((this._old_namespace != value))
				{
					this.Onold_namespaceChanging(value);
					this.SendPropertyChanging();
					this._old_namespace = value;
					this.SendPropertyChanged("old_namespace");
					this.Onold_namespaceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_new_namespace", DbType="VarChar(MAX)")]
		public string new_namespace
		{
			get
			{
				return this._new_namespace;
			}
			set
			{
				if ((this._new_namespace != value))
				{
					this.Onnew_namespaceChanging(value);
					this.SendPropertyChanging();
					this._new_namespace = value;
					this.SendPropertyChanged("new_namespace");
					this.Onnew_namespaceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_new_assembly_full_name", DbType="VarChar(MAX)")]
		public string new_assembly_full_name
		{
			get
			{
				return this._new_assembly_full_name;
			}
			set
			{
				if ((this._new_assembly_full_name != value))
				{
					this.Onnew_assembly_full_nameChanging(value);
					this.SendPropertyChanging();
					this._new_assembly_full_name = value;
					this.SendPropertyChanged("new_assembly_full_name");
					this.Onnew_assembly_full_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="sdk_sdk_map", Storage="_sdk", ThisKey="sdk_id", OtherKey="id", IsForeignKey=true)]
		public sdk sdk
		{
			get
			{
				return this._sdk.Entity;
			}
			set
			{
				sdk previousValue = this._sdk.Entity;
				if (((previousValue != value) 
							|| (this._sdk.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._sdk.Entity = null;
						previousValue.sdk_maps.Remove(this);
					}
					this._sdk.Entity = value;
					if ((value != null))
					{
						value.sdk_maps.Add(this);
						this._sdk_id = value.id;
					}
					else
					{
						this._sdk_id = default(int);
					}
					this.SendPropertyChanged("sdk");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
