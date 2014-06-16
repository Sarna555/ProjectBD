﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Logic
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Database1")]
	public partial class DataClasses1DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertUser(User instance);
    partial void UpdateUser(User instance);
    partial void DeleteUser(User instance);
    partial void Insertgroup(group instance);
    partial void Updategroup(group instance);
    partial void Deletegroup(group instance);
    partial void Insertoperation(operation instance);
    partial void Updateoperation(operation instance);
    partial void Deleteoperation(operation instance);
    #endregion
		
		public DataClasses1DataContext() : 
				base(global::Logic.Properties.Settings.Default.Database1ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<User> Users
		{
			get
			{
				return this.GetTable<User>();
			}
		}
		
		public System.Data.Linq.Table<users2group> users2groups
		{
			get
			{
				return this.GetTable<users2group>();
			}
		}
		
		public System.Data.Linq.Table<group> groups
		{
			get
			{
				return this.GetTable<group>();
			}
		}
		
		public System.Data.Linq.Table<operation> operations
		{
			get
			{
				return this.GetTable<operation>();
			}
		}
		
		public System.Data.Linq.Table<users2operation> users2operations
		{
			get
			{
				return this.GetTable<users2operation>();
			}
		}
		
		public System.Data.Linq.Table<groups2operation> groups2operations
		{
			get
			{
				return this.GetTable<groups2operation>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Users")]
	public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _user_ID;
		
		private string _name;
		
		private string _surname;
		
		private string _email;
		
		private string _password;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Onuser_IDChanging(int value);
    partial void Onuser_IDChanged();
    partial void OnnameChanging(string value);
    partial void OnnameChanged();
    partial void OnsurnameChanging(string value);
    partial void OnsurnameChanged();
    partial void OnemailChanging(string value);
    partial void OnemailChanged();
    partial void OnpasswordChanging(string value);
    partial void OnpasswordChanged();
    #endregion
		
		public User()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_user_ID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int user_ID
		{
			get
			{
				return this._user_ID;
			}
			set
			{
				if ((this._user_ID != value))
				{
					this.Onuser_IDChanging(value);
					this.SendPropertyChanging();
					this._user_ID = value;
					this.SendPropertyChanged("user_ID");
					this.Onuser_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_name", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_surname", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string surname
		{
			get
			{
				return this._surname;
			}
			set
			{
				if ((this._surname != value))
				{
					this.OnsurnameChanging(value);
					this.SendPropertyChanging();
					this._surname = value;
					this.SendPropertyChanged("surname");
					this.OnsurnameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_email", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string email
		{
			get
			{
				return this._email;
			}
			set
			{
				if ((this._email != value))
				{
					this.OnemailChanging(value);
					this.SendPropertyChanging();
					this._email = value;
					this.SendPropertyChanged("email");
					this.OnemailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_password", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string password
		{
			get
			{
				return this._password;
			}
			set
			{
				if ((this._password != value))
				{
					this.OnpasswordChanging(value);
					this.SendPropertyChanging();
					this._password = value;
					this.SendPropertyChanged("password");
					this.OnpasswordChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.users2groups")]
	public partial class users2group
	{
		
		private int _user_ID;
		
		private int _group_ID;
		
		public users2group()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_user_ID", DbType="Int NOT NULL")]
		public int user_ID
		{
			get
			{
				return this._user_ID;
			}
			set
			{
				if ((this._user_ID != value))
				{
					this._user_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_group_ID", DbType="Int NOT NULL")]
		public int group_ID
		{
			get
			{
				return this._group_ID;
			}
			set
			{
				if ((this._group_ID != value))
				{
					this._group_ID = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.groups")]
	public partial class group : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _group_ID;
		
		private string _name;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Ongroup_IDChanging(int value);
    partial void Ongroup_IDChanged();
    partial void OnnameChanging(string value);
    partial void OnnameChanged();
    #endregion
		
		public group()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_group_ID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int group_ID
		{
			get
			{
				return this._group_ID;
			}
			set
			{
				if ((this._group_ID != value))
				{
					this.Ongroup_IDChanging(value);
					this.SendPropertyChanging();
					this._group_ID = value;
					this.SendPropertyChanged("group_ID");
					this.Ongroup_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_name", DbType="VarChar(20)")]
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.operations")]
	public partial class operation : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _operation_ID;
		
		private string _name;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Onoperation_IDChanging(int value);
    partial void Onoperation_IDChanged();
    partial void OnnameChanging(string value);
    partial void OnnameChanged();
    #endregion
		
		public operation()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_operation_ID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int operation_ID
		{
			get
			{
				return this._operation_ID;
			}
			set
			{
				if ((this._operation_ID != value))
				{
					this.Onoperation_IDChanging(value);
					this.SendPropertyChanging();
					this._operation_ID = value;
					this.SendPropertyChanged("operation_ID");
					this.Onoperation_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_name", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.users2operation")]
	public partial class users2operation
	{
		
		private int _user_ID;
		
		private int _operation_ID;
		
		public users2operation()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_user_ID", DbType="Int NOT NULL")]
		public int user_ID
		{
			get
			{
				return this._user_ID;
			}
			set
			{
				if ((this._user_ID != value))
				{
					this._user_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_operation_ID", DbType="Int NOT NULL")]
		public int operation_ID
		{
			get
			{
				return this._operation_ID;
			}
			set
			{
				if ((this._operation_ID != value))
				{
					this._operation_ID = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.groups2operations")]
	public partial class groups2operation
	{
		
		private int _group_ID;
		
		private int _operation_ID;
		
		public groups2operation()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_group_ID", DbType="Int NOT NULL")]
		public int group_ID
		{
			get
			{
				return this._group_ID;
			}
			set
			{
				if ((this._group_ID != value))
				{
					this._group_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_operation_ID", DbType="Int NOT NULL")]
		public int operation_ID
		{
			get
			{
				return this._operation_ID;
			}
			set
			{
				if ((this._operation_ID != value))
				{
					this._operation_ID = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
