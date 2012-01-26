﻿
'------------------------------------------------------------------------------
' <auto-generated>
'    This code was generated from a template.
'
'    Manual changes to this file may cause unexpected behavior in your application.
'    Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Data.Objects
Imports System.Data.Objects.DataClasses
Imports System.Data.EntityClient
Imports System.ComponentModel
Imports System.Xml.Serialization
Imports System.Runtime.Serialization

<Assembly: EdmSchemaAttribute("3ad5fc0a-ae4b-4aa4-b88e-c6e751e9553f")>
#Region "EDM Relationship Metadata"
<Assembly: EdmRelationshipAttribute("TaskModel", "TaskAssignment", "Person", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, GetType(Person), "Task", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, GetType(Task), True)>
<Assembly: EdmRelationshipAttribute("TaskModel", "ContextTask", "Context", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, GetType(Context), "Task", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, GetType(Task), True)>
<Assembly: EdmRelationshipAttribute("TaskModel", "ProjectTask", "Project", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, GetType(Project), "Task", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, GetType(Task), True)>

#End Region

#Region "Contexts"

''' <summary>
''' No Metadata Documentation available.
''' </summary>
Public Partial Class TaskModelContainer
    Inherits ObjectContext

    #Region "Constructors"

    ''' <summary>
    ''' Initializes a new TaskModelContainer object using the connection string found in the 'TaskModelContainer' section of the application configuration file.
    ''' </summary>
    Public Sub New()
        MyBase.New("name=TaskModelContainer", "TaskModelContainer")
    MyBase.ContextOptions.LazyLoadingEnabled = true
        OnContextCreated()
    End Sub

    ''' <summary>
    ''' Initialize a new TaskModelContainer object.
    ''' </summary>
    Public Sub New(ByVal connectionString As String)
        MyBase.New(connectionString, "TaskModelContainer")
    MyBase.ContextOptions.LazyLoadingEnabled = true
        OnContextCreated()
    End Sub

    ''' <summary>
    ''' Initialize a new TaskModelContainer object.
    ''' </summary>
    Public Sub New(ByVal connection As EntityConnection)
        MyBase.New(connection, "TaskModelContainer")
    MyBase.ContextOptions.LazyLoadingEnabled = true
        OnContextCreated()
    End Sub

    #End Region

    #Region "Partial Methods"

    Partial Private Sub OnContextCreated()
    End Sub

    #End Region

    #Region "ObjectSet Properties"

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    Public ReadOnly Property People() As ObjectSet(Of Person)
        Get
            If (_People Is Nothing) Then
                _People = MyBase.CreateObjectSet(Of Person)("People")
            End If
            Return _People
        End Get
    End Property

    Private _People As ObjectSet(Of Person)

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    Public ReadOnly Property Tasks() As ObjectSet(Of Task)
        Get
            If (_Tasks Is Nothing) Then
                _Tasks = MyBase.CreateObjectSet(Of Task)("Tasks")
            End If
            Return _Tasks
        End Get
    End Property

    Private _Tasks As ObjectSet(Of Task)

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    Public ReadOnly Property Contexts() As ObjectSet(Of Context)
        Get
            If (_Contexts Is Nothing) Then
                _Contexts = MyBase.CreateObjectSet(Of Context)("Contexts")
            End If
            Return _Contexts
        End Get
    End Property

    Private _Contexts As ObjectSet(Of Context)

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    Public ReadOnly Property Projects() As ObjectSet(Of Project)
        Get
            If (_Projects Is Nothing) Then
                _Projects = MyBase.CreateObjectSet(Of Project)("Projects")
            End If
            Return _Projects
        End Get
    End Property

    Private _Projects As ObjectSet(Of Project)

    #End Region
    #Region "AddTo Methods"

    ''' <summary>
    ''' Deprecated Method for adding a new object to the People EntitySet. Consider using the .Add method of the associated ObjectSet(Of T) property instead.
    ''' </summary>
    Public Sub AddToPeople(ByVal person As Person)
        MyBase.AddObject("People", person)
    End Sub

    ''' <summary>
    ''' Deprecated Method for adding a new object to the Tasks EntitySet. Consider using the .Add method of the associated ObjectSet(Of T) property instead.
    ''' </summary>
    Public Sub AddToTasks(ByVal task As Task)
        MyBase.AddObject("Tasks", task)
    End Sub

    ''' <summary>
    ''' Deprecated Method for adding a new object to the Contexts EntitySet. Consider using the .Add method of the associated ObjectSet(Of T) property instead.
    ''' </summary>
    Public Sub AddToContexts(ByVal context As Context)
        MyBase.AddObject("Contexts", context)
    End Sub

    ''' <summary>
    ''' Deprecated Method for adding a new object to the Projects EntitySet. Consider using the .Add method of the associated ObjectSet(Of T) property instead.
    ''' </summary>
    Public Sub AddToProjects(ByVal project As Project)
        MyBase.AddObject("Projects", project)
    End Sub

    #End Region
End Class

#End Region
#Region "Entities"

''' <summary>
''' No Metadata Documentation available.
''' </summary>
<EdmEntityTypeAttribute(NamespaceName:="TaskModel", Name:="Context")>
<Serializable()>
<DataContractAttribute(IsReference:=True)>
Public Partial Class Context
    Inherits EntityObject
    #Region "Factory Method"

    ''' <summary>
    ''' Create a new Context object.
    ''' </summary>
    ''' <param name="id">Initial value of the Id property.</param>
    ''' <param name="name">Initial value of the Name property.</param>
    ''' <param name="ownerId">Initial value of the OwnerId property.</param>
    Public Shared Function CreateContext(id As Global.System.Int32, name As Global.System.String, ownerId As Global.System.Guid) As Context
        Dim context as Context = New Context
        context.Id = id
        context.Name = name
        context.OwnerId = ownerId
        Return context
    End Function

    #End Region
    #Region "Primitive Properties"

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <EdmScalarPropertyAttribute(EntityKeyProperty:=true, IsNullable:=false)>
    <DataMemberAttribute()>
    Public Property Id() As Global.System.Int32
        Get
            Return _Id
        End Get
        Set
            If (_Id <> Value) Then
                OnIdChanging(value)
                ReportPropertyChanging("Id")
                _Id = StructuralObject.SetValidValue(value)
                ReportPropertyChanged("Id")
                OnIdChanged()
            End If
        End Set
    End Property

    Private _Id As Global.System.Int32
    Private Partial Sub OnIdChanging(value As Global.System.Int32)
    End Sub

    Private Partial Sub OnIdChanged()
    End Sub

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <EdmScalarPropertyAttribute(EntityKeyProperty:=false, IsNullable:=false)>
    <DataMemberAttribute()>
    Public Property Name() As Global.System.String
        Get
            Return _Name
        End Get
        Set
            OnNameChanging(value)
            ReportPropertyChanging("Name")
            _Name = StructuralObject.SetValidValue(value, false)
            ReportPropertyChanged("Name")
            OnNameChanged()
        End Set
    End Property

    Private _Name As Global.System.String
    Private Partial Sub OnNameChanging(value As Global.System.String)
    End Sub

    Private Partial Sub OnNameChanged()
    End Sub

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <EdmScalarPropertyAttribute(EntityKeyProperty:=false, IsNullable:=false)>
    <DataMemberAttribute()>
    Public Property OwnerId() As Global.System.Guid
        Get
            Return _OwnerId
        End Get
        Set
            OnOwnerIdChanging(value)
            ReportPropertyChanging("OwnerId")
            _OwnerId = StructuralObject.SetValidValue(value)
            ReportPropertyChanged("OwnerId")
            OnOwnerIdChanged()
        End Set
    End Property

    Private _OwnerId As Global.System.Guid
    Private Partial Sub OnOwnerIdChanging(value As Global.System.Guid)
    End Sub

    Private Partial Sub OnOwnerIdChanged()
    End Sub

    #End Region
    #Region "Navigation Properties"

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <XmlIgnoreAttribute()>
    <SoapIgnoreAttribute()>
    <DataMemberAttribute()>
    <EdmRelationshipNavigationPropertyAttribute("TaskModel", "ContextTask", "Task")>
     Public Property Tasks() As EntityCollection(Of Task)
        Get
            Return CType(Me,IEntityWithRelationships).RelationshipManager.GetRelatedCollection(Of Task)("TaskModel.ContextTask", "Task")
        End Get
        Set
            If (Not value Is Nothing)
                CType(Me, IEntityWithRelationships).RelationshipManager.InitializeRelatedCollection(Of Task)("TaskModel.ContextTask", "Task", value)
            End If
        End Set
    End Property

    #End Region
End Class

''' <summary>
''' No Metadata Documentation available.
''' </summary>
<EdmEntityTypeAttribute(NamespaceName:="TaskModel", Name:="Person")>
<Serializable()>
<DataContractAttribute(IsReference:=True)>
Public Partial Class Person
    Inherits EntityObject
    #Region "Factory Method"

    ''' <summary>
    ''' Create a new Person object.
    ''' </summary>
    ''' <param name="id">Initial value of the Id property.</param>
    ''' <param name="name">Initial value of the Name property.</param>
    ''' <param name="email">Initial value of the Email property.</param>
    ''' <param name="userId">Initial value of the UserId property.</param>
    ''' <param name="ownerId">Initial value of the OwnerId property.</param>
    Public Shared Function CreatePerson(id As Global.System.Int32, name As Global.System.String, email As Global.System.String, userId As Global.System.Guid, ownerId As Global.System.Guid) As Person
        Dim person as Person = New Person
        person.Id = id
        person.Name = name
        person.Email = email
        person.UserId = userId
        person.OwnerId = ownerId
        Return person
    End Function

    #End Region
    #Region "Primitive Properties"

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <EdmScalarPropertyAttribute(EntityKeyProperty:=true, IsNullable:=false)>
    <DataMemberAttribute()>
    Public Property Id() As Global.System.Int32
        Get
            Return _Id
        End Get
        Set
            If (_Id <> Value) Then
                OnIdChanging(value)
                ReportPropertyChanging("Id")
                _Id = StructuralObject.SetValidValue(value)
                ReportPropertyChanged("Id")
                OnIdChanged()
            End If
        End Set
    End Property

    Private _Id As Global.System.Int32
    Private Partial Sub OnIdChanging(value As Global.System.Int32)
    End Sub

    Private Partial Sub OnIdChanged()
    End Sub

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <EdmScalarPropertyAttribute(EntityKeyProperty:=false, IsNullable:=false)>
    <DataMemberAttribute()>
    Public Property Name() As Global.System.String
        Get
            Return _Name
        End Get
        Set
            OnNameChanging(value)
            ReportPropertyChanging("Name")
            _Name = StructuralObject.SetValidValue(value, false)
            ReportPropertyChanged("Name")
            OnNameChanged()
        End Set
    End Property

    Private _Name As Global.System.String
    Private Partial Sub OnNameChanging(value As Global.System.String)
    End Sub

    Private Partial Sub OnNameChanged()
    End Sub

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <EdmScalarPropertyAttribute(EntityKeyProperty:=false, IsNullable:=false)>
    <DataMemberAttribute()>
    Public Property Email() As Global.System.String
        Get
            Return _Email
        End Get
        Set
            OnEmailChanging(value)
            ReportPropertyChanging("Email")
            _Email = StructuralObject.SetValidValue(value, false)
            ReportPropertyChanged("Email")
            OnEmailChanged()
        End Set
    End Property

    Private _Email As Global.System.String
    Private Partial Sub OnEmailChanging(value As Global.System.String)
    End Sub

    Private Partial Sub OnEmailChanged()
    End Sub

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <EdmScalarPropertyAttribute(EntityKeyProperty:=false, IsNullable:=false)>
    <DataMemberAttribute()>
    Public Property UserId() As Global.System.Guid
        Get
            Return _UserId
        End Get
        Set
            OnUserIdChanging(value)
            ReportPropertyChanging("UserId")
            _UserId = StructuralObject.SetValidValue(value)
            ReportPropertyChanged("UserId")
            OnUserIdChanged()
        End Set
    End Property

    Private _UserId As Global.System.Guid
    Private Partial Sub OnUserIdChanging(value As Global.System.Guid)
    End Sub

    Private Partial Sub OnUserIdChanged()
    End Sub

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <EdmScalarPropertyAttribute(EntityKeyProperty:=false, IsNullable:=false)>
    <DataMemberAttribute()>
    Public Property OwnerId() As Global.System.Guid
        Get
            Return _OwnerId
        End Get
        Set
            OnOwnerIdChanging(value)
            ReportPropertyChanging("OwnerId")
            _OwnerId = StructuralObject.SetValidValue(value)
            ReportPropertyChanged("OwnerId")
            OnOwnerIdChanged()
        End Set
    End Property

    Private _OwnerId As Global.System.Guid
    Private Partial Sub OnOwnerIdChanging(value As Global.System.Guid)
    End Sub

    Private Partial Sub OnOwnerIdChanged()
    End Sub

    #End Region
    #Region "Navigation Properties"

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <XmlIgnoreAttribute()>
    <SoapIgnoreAttribute()>
    <DataMemberAttribute()>
    <EdmRelationshipNavigationPropertyAttribute("TaskModel", "TaskAssignment", "Task")>
     Public Property Task() As EntityCollection(Of Task)
        Get
            Return CType(Me,IEntityWithRelationships).RelationshipManager.GetRelatedCollection(Of Task)("TaskModel.TaskAssignment", "Task")
        End Get
        Set
            If (Not value Is Nothing)
                CType(Me, IEntityWithRelationships).RelationshipManager.InitializeRelatedCollection(Of Task)("TaskModel.TaskAssignment", "Task", value)
            End If
        End Set
    End Property

    #End Region
End Class

''' <summary>
''' No Metadata Documentation available.
''' </summary>
<EdmEntityTypeAttribute(NamespaceName:="TaskModel", Name:="Project")>
<Serializable()>
<DataContractAttribute(IsReference:=True)>
Public Partial Class Project
    Inherits EntityObject
    #Region "Factory Method"

    ''' <summary>
    ''' Create a new Project object.
    ''' </summary>
    ''' <param name="id">Initial value of the Id property.</param>
    ''' <param name="name">Initial value of the Name property.</param>
    ''' <param name="ownerId">Initial value of the OwnerId property.</param>
    Public Shared Function CreateProject(id As Global.System.Int32, name As Global.System.String, ownerId As Global.System.Guid) As Project
        Dim project as Project = New Project
        project.Id = id
        project.Name = name
        project.OwnerId = ownerId
        Return project
    End Function

    #End Region
    #Region "Primitive Properties"

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <EdmScalarPropertyAttribute(EntityKeyProperty:=true, IsNullable:=false)>
    <DataMemberAttribute()>
    Public Property Id() As Global.System.Int32
        Get
            Return _Id
        End Get
        Set
            If (_Id <> Value) Then
                OnIdChanging(value)
                ReportPropertyChanging("Id")
                _Id = StructuralObject.SetValidValue(value)
                ReportPropertyChanged("Id")
                OnIdChanged()
            End If
        End Set
    End Property

    Private _Id As Global.System.Int32
    Private Partial Sub OnIdChanging(value As Global.System.Int32)
    End Sub

    Private Partial Sub OnIdChanged()
    End Sub

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <EdmScalarPropertyAttribute(EntityKeyProperty:=false, IsNullable:=false)>
    <DataMemberAttribute()>
    Public Property Name() As Global.System.String
        Get
            Return _Name
        End Get
        Set
            OnNameChanging(value)
            ReportPropertyChanging("Name")
            _Name = StructuralObject.SetValidValue(value, false)
            ReportPropertyChanged("Name")
            OnNameChanged()
        End Set
    End Property

    Private _Name As Global.System.String
    Private Partial Sub OnNameChanging(value As Global.System.String)
    End Sub

    Private Partial Sub OnNameChanged()
    End Sub

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <EdmScalarPropertyAttribute(EntityKeyProperty:=false, IsNullable:=false)>
    <DataMemberAttribute()>
    Public Property OwnerId() As Global.System.Guid
        Get
            Return _OwnerId
        End Get
        Set
            OnOwnerIdChanging(value)
            ReportPropertyChanging("OwnerId")
            _OwnerId = StructuralObject.SetValidValue(value)
            ReportPropertyChanged("OwnerId")
            OnOwnerIdChanged()
        End Set
    End Property

    Private _OwnerId As Global.System.Guid
    Private Partial Sub OnOwnerIdChanging(value As Global.System.Guid)
    End Sub

    Private Partial Sub OnOwnerIdChanged()
    End Sub

    #End Region
    #Region "Navigation Properties"

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <XmlIgnoreAttribute()>
    <SoapIgnoreAttribute()>
    <DataMemberAttribute()>
    <EdmRelationshipNavigationPropertyAttribute("TaskModel", "ProjectTask", "Task")>
     Public Property Tasks() As EntityCollection(Of Task)
        Get
            Return CType(Me,IEntityWithRelationships).RelationshipManager.GetRelatedCollection(Of Task)("TaskModel.ProjectTask", "Task")
        End Get
        Set
            If (Not value Is Nothing)
                CType(Me, IEntityWithRelationships).RelationshipManager.InitializeRelatedCollection(Of Task)("TaskModel.ProjectTask", "Task", value)
            End If
        End Set
    End Property

    #End Region
End Class

''' <summary>
''' No Metadata Documentation available.
''' </summary>
<EdmEntityTypeAttribute(NamespaceName:="TaskModel", Name:="Task")>
<Serializable()>
<DataContractAttribute(IsReference:=True)>
Public Partial Class Task
    Inherits EntityObject
    #Region "Factory Method"

    ''' <summary>
    ''' Create a new Task object.
    ''' </summary>
    ''' <param name="id">Initial value of the Id property.</param>
    ''' <param name="title">Initial value of the Title property.</param>
    ''' <param name="notes">Initial value of the Notes property.</param>
    ''' <param name="ownerId">Initial value of the OwnerId property.</param>
    Public Shared Function CreateTask(id As Global.System.Int32, title As Global.System.String, notes As Global.System.String, ownerId As Global.System.Guid) As Task
        Dim task as Task = New Task
        task.Id = id
        task.Title = title
        task.Notes = notes
        task.OwnerId = ownerId
        Return task
    End Function

    #End Region
    #Region "Primitive Properties"

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <EdmScalarPropertyAttribute(EntityKeyProperty:=true, IsNullable:=false)>
    <DataMemberAttribute()>
    Public Property Id() As Global.System.Int32
        Get
            Return _Id
        End Get
        Set
            If (_Id <> Value) Then
                OnIdChanging(value)
                ReportPropertyChanging("Id")
                _Id = StructuralObject.SetValidValue(value)
                ReportPropertyChanged("Id")
                OnIdChanged()
            End If
        End Set
    End Property

    Private _Id As Global.System.Int32
    Private Partial Sub OnIdChanging(value As Global.System.Int32)
    End Sub

    Private Partial Sub OnIdChanged()
    End Sub

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <EdmScalarPropertyAttribute(EntityKeyProperty:=false, IsNullable:=false)>
    <DataMemberAttribute()>
    Public Property Title() As Global.System.String
        Get
            Return _Title
        End Get
        Set
            OnTitleChanging(value)
            ReportPropertyChanging("Title")
            _Title = StructuralObject.SetValidValue(value, false)
            ReportPropertyChanged("Title")
            OnTitleChanged()
        End Set
    End Property

    Private _Title As Global.System.String
    Private Partial Sub OnTitleChanging(value As Global.System.String)
    End Sub

    Private Partial Sub OnTitleChanged()
    End Sub

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <EdmScalarPropertyAttribute(EntityKeyProperty:=false, IsNullable:=false)>
    <DataMemberAttribute()>
    Public Property Notes() As Global.System.String
        Get
            Return _Notes
        End Get
        Set
            OnNotesChanging(value)
            ReportPropertyChanging("Notes")
            _Notes = StructuralObject.SetValidValue(value, false)
            ReportPropertyChanged("Notes")
            OnNotesChanged()
        End Set
    End Property

    Private _Notes As Global.System.String
    Private Partial Sub OnNotesChanging(value As Global.System.String)
    End Sub

    Private Partial Sub OnNotesChanged()
    End Sub

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <EdmScalarPropertyAttribute(EntityKeyProperty:=false, IsNullable:=false)>
    <DataMemberAttribute()>
    Public Property OwnerId() As Global.System.Guid
        Get
            Return _OwnerId
        End Get
        Set
            OnOwnerIdChanging(value)
            ReportPropertyChanging("OwnerId")
            _OwnerId = StructuralObject.SetValidValue(value)
            ReportPropertyChanged("OwnerId")
            OnOwnerIdChanged()
        End Set
    End Property

    Private _OwnerId As Global.System.Guid
    Private Partial Sub OnOwnerIdChanging(value As Global.System.Guid)
    End Sub

    Private Partial Sub OnOwnerIdChanged()
    End Sub

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <EdmScalarPropertyAttribute(EntityKeyProperty:=false, IsNullable:=true)>
    <DataMemberAttribute()>
    Public Property AssignedToId() As Nullable(Of Global.System.Int32)
        Get
            Return _AssignedToId
        End Get
        Set
            OnAssignedToIdChanging(value)
            ReportPropertyChanging("AssignedToId")
            _AssignedToId = StructuralObject.SetValidValue(value)
            ReportPropertyChanged("AssignedToId")
            OnAssignedToIdChanged()
        End Set
    End Property

    Private _AssignedToId As Nullable(Of Global.System.Int32)
    Private Partial Sub OnAssignedToIdChanging(value As Nullable(Of Global.System.Int32))
    End Sub

    Private Partial Sub OnAssignedToIdChanged()
    End Sub

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <EdmScalarPropertyAttribute(EntityKeyProperty:=false, IsNullable:=true)>
    <DataMemberAttribute()>
    Public Property ContextId() As Nullable(Of Global.System.Int32)
        Get
            Return _ContextId
        End Get
        Set
            OnContextIdChanging(value)
            ReportPropertyChanging("ContextId")
            _ContextId = StructuralObject.SetValidValue(value)
            ReportPropertyChanged("ContextId")
            OnContextIdChanged()
        End Set
    End Property

    Private _ContextId As Nullable(Of Global.System.Int32)
    Private Partial Sub OnContextIdChanging(value As Nullable(Of Global.System.Int32))
    End Sub

    Private Partial Sub OnContextIdChanged()
    End Sub

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <EdmScalarPropertyAttribute(EntityKeyProperty:=false, IsNullable:=true)>
    <DataMemberAttribute()>
    Public Property ProjectId() As Nullable(Of Global.System.Int32)
        Get
            Return _ProjectId
        End Get
        Set
            OnProjectIdChanging(value)
            ReportPropertyChanging("ProjectId")
            _ProjectId = StructuralObject.SetValidValue(value)
            ReportPropertyChanged("ProjectId")
            OnProjectIdChanged()
        End Set
    End Property

    Private _ProjectId As Nullable(Of Global.System.Int32)
    Private Partial Sub OnProjectIdChanging(value As Nullable(Of Global.System.Int32))
    End Sub

    Private Partial Sub OnProjectIdChanged()
    End Sub

    #End Region
    #Region "Navigation Properties"

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <XmlIgnoreAttribute()>
    <SoapIgnoreAttribute()>
    <DataMemberAttribute()>
    <EdmRelationshipNavigationPropertyAttribute("TaskModel", "TaskAssignment", "Person")>
    Public Property AssignedTo() As Person
        Get
            Return CType(Me, IEntityWithRelationships).RelationshipManager.GetRelatedReference(Of Person)("TaskModel.TaskAssignment", "Person").Value
        End Get
        Set
            CType(Me, IEntityWithRelationships).RelationshipManager.GetRelatedReference(Of Person)("TaskModel.TaskAssignment", "Person").Value = value
        End Set
    End Property
    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <BrowsableAttribute(False)>
    <DataMemberAttribute()>
    Public Property AssignedToReference() As EntityReference(Of Person)
        Get
            Return CType(Me, IEntityWithRelationships).RelationshipManager.GetRelatedReference(Of Person)("TaskModel.TaskAssignment", "Person")
        End Get
        Set
            If (Not value Is Nothing)
                CType(Me, IEntityWithRelationships).RelationshipManager.InitializeRelatedReference(Of Person)("TaskModel.TaskAssignment", "Person", value)
            End If
        End Set
    End Property

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <XmlIgnoreAttribute()>
    <SoapIgnoreAttribute()>
    <DataMemberAttribute()>
    <EdmRelationshipNavigationPropertyAttribute("TaskModel", "ContextTask", "Context")>
    Public Property Context() As Context
        Get
            Return CType(Me, IEntityWithRelationships).RelationshipManager.GetRelatedReference(Of Context)("TaskModel.ContextTask", "Context").Value
        End Get
        Set
            CType(Me, IEntityWithRelationships).RelationshipManager.GetRelatedReference(Of Context)("TaskModel.ContextTask", "Context").Value = value
        End Set
    End Property
    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <BrowsableAttribute(False)>
    <DataMemberAttribute()>
    Public Property ContextReference() As EntityReference(Of Context)
        Get
            Return CType(Me, IEntityWithRelationships).RelationshipManager.GetRelatedReference(Of Context)("TaskModel.ContextTask", "Context")
        End Get
        Set
            If (Not value Is Nothing)
                CType(Me, IEntityWithRelationships).RelationshipManager.InitializeRelatedReference(Of Context)("TaskModel.ContextTask", "Context", value)
            End If
        End Set
    End Property

    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <XmlIgnoreAttribute()>
    <SoapIgnoreAttribute()>
    <DataMemberAttribute()>
    <EdmRelationshipNavigationPropertyAttribute("TaskModel", "ProjectTask", "Project")>
    Public Property Project() As Project
        Get
            Return CType(Me, IEntityWithRelationships).RelationshipManager.GetRelatedReference(Of Project)("TaskModel.ProjectTask", "Project").Value
        End Get
        Set
            CType(Me, IEntityWithRelationships).RelationshipManager.GetRelatedReference(Of Project)("TaskModel.ProjectTask", "Project").Value = value
        End Set
    End Property
    ''' <summary>
    ''' No Metadata Documentation available.
    ''' </summary>
    <BrowsableAttribute(False)>
    <DataMemberAttribute()>
    Public Property ProjectReference() As EntityReference(Of Project)
        Get
            Return CType(Me, IEntityWithRelationships).RelationshipManager.GetRelatedReference(Of Project)("TaskModel.ProjectTask", "Project")
        End Get
        Set
            If (Not value Is Nothing)
                CType(Me, IEntityWithRelationships).RelationshipManager.InitializeRelatedReference(Of Project)("TaskModel.ProjectTask", "Project", value)
            End If
        End Set
    End Property

    #End Region
End Class

#End Region

