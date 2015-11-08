// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Backlog.Project.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using BL4N.Data;
using Newtonsoft.Json;

namespace BL4N
{
    /// <summary> The backlog. for Project API </summary>
    public partial class Backlog
    {
        #region project

        /// <summary>
        /// Get Project List
        /// Returns list of projects.
        /// </summary>
        /// <returns>list of <see cref="IProject"/> </returns>
        public IList<IProject> GetProjects()
        {
            var api = GetApiUri(new[] { "projects" });
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = GetApiResult<List<Project>>(api, jss);
            return res.Result.ToList<IProject>();
        }

        /// <summary>
        /// Add Project
        /// Adds new project.
        /// </summary>
        /// <param name="options">project to create</param>
        /// <returns>created <see cref="IProject"/></returns>
        public IProject AddProject(AddProjectOptions options)
        {
            var api = GetApiUri(new[] { "projects" });
            var jss = new JsonSerializerSettings();
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<Project>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Get Project
        /// Returns information about project.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns><see cref="IProject"/></returns>
        public IProject GetProject(long projectId)
        {
            return GetProject(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Get Project
        /// Returns information about project.
        /// </summary>
        /// <param name="projectKey">project key string</param>
        /// <returns><see cref="IProject"/></returns>
        public IProject GetProject(string projectKey)
        {
            var api = GetApiUri(new[] { "projects", projectKey });
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = GetApiResult<Project>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Update Project
        /// Updates information about project.
        /// </summary>
        /// <param name="projectId">project key</param>
        /// <param name="options">update option</param>
        /// <returns>updated <see cref="IProject"/></returns>
        public IProject UpdateProject(long projectId, UpdateProjectOptions options)
        {
            return UpdateProject(string.Format("{0}", projectId), options);
        }

        /// <summary>
        /// Update Project
        /// Updates information about project.
        /// </summary>
        /// <param name="projectKey">project key string</param>
        /// <param name="options">update option</param>
        /// <returns>updated <see cref="IProject"/></returns>
        public IProject UpdateProject(string projectKey, UpdateProjectOptions options)
        {
            var api = GetApiUri(new[] { "projects", projectKey });
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var kvs = options.ToKeyValurPairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PatchApiResult<Project>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Delete Project
        /// Deletes project.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns>deleted project</returns>
        public IProject DeleteProject(long projectId)
        {
            return DeleteProject(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Delete Project
        /// Deletes project.
        /// </summary>
        /// <param name="projectKey">project key string</param>
        /// <returns>deleted project</returns>
        public IProject DeleteProject(string projectKey)
        {
            var api = GetApiUri(new[] { "projects", projectKey });
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = DeleteApiResult<Project>(api, jss);
            return res.Result;
        }

        #endregion

        #region project/misc

        /// <summary>
        /// Get Project Icon
        /// Downloads project icon.
        /// </summary>
        /// <param name="projectId">project key</param>
        /// <returns>project icon</returns>
        public ILogo GetProjectImage(long projectId)
        {
            return GetProjectImage(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Get Project Icon
        /// Downloads project icon.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <returns>project icon</returns>
        public ILogo GetProjectImage(string projectKey)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "image" });
            var res = GetApiResultAsFile(api);

            return new Logo(res.Result.Item1, res.Result.Item2);
        }

        /// <summary>
        /// Get Project Recent Updates
        /// Returns recent update in the project.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="filter">activity filtering option</param>
        /// <returns>list of <see cref="IActivity"/></returns>
        public IList<IActivity> GetProjectRecentUpdates(long projectId, RecentUpdateFilterOptions filter = null)
        {
            return GetProjectRecentUpdates(string.Format("{0}", projectId), filter);
        }

        /// <summary>
        /// Get Project Recent Updates
        /// Returns recent update in the project.
        /// </summary>
        /// <param name="projectKey">project key string</param>
        /// <param name="filter">activity filtering option</param>
        /// <returns>list of <see cref="IActivity"/></returns>
        public IList<IActivity> GetProjectRecentUpdates(string projectKey, RecentUpdateFilterOptions filter = null)
        {
            var query = filter == null ? null : filter.ToKeyValuePairs();
            var api = GetApiUri(new[] { "projects", projectKey, "activities" }, query);
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                Converters = new JsonConverter[] { new ActivityConverter() }
            };
            var res = GetApiResult<List<Activity>>(api, jss);
            return res.Result.ToList<IActivity>();
        }

        /// <summary>
        /// Get Project Disk Usage
        /// Returns information about project disk usage.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns>project's <see cref="IDiskUsageDetail"/></returns>
        public IDiskUsageDetail GetProjectDiskUsage(long projectId)
        {
            return GetProjectDiskUsage(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Get Project Disk Usage
        /// Returns information about project disk usage.
        /// </summary>
        /// <param name="projectkey"></param>
        /// <returns>project's <see cref="IDiskUsageDetail"/></returns>
        public IDiskUsageDetail GetProjectDiskUsage(string projectkey)
        {
            var api = GetApiUri(new[] { "projects", projectkey, "diskUsage" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<DiskUsageDetail>(api, jss);
            return res.Result;
        }

        #endregion

        #region project/user

        /// <summary>
        /// Add Project User
        /// Adds user to list of project members.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="uid">user id</param>
        /// <returns>added <see cref="IUser"/></returns>
        public IUser AddProjectUser(long projectId, long uid)
        {
            return AddProjectUser(string.Format("{0}", projectId), uid);
        }

        /// <summary>
        /// Add Project User
        /// Adds user to list of project members.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="uid">user id</param>
        /// <returns>added <see cref="IUser"/></returns>
        public IUser AddProjectUser(string projectKey, long uid)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "users" });
            var jss = new JsonSerializerSettings();
            var kvs = new[] { new KeyValuePair<string, string>("userId", uid.ToString()) };
            var hc = new FormUrlEncodedContent(kvs);

            var res = PostApiResult<User>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Get Project User List
        /// Returns list of project members.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns>list of <see cref="IUser"/></returns>
        public IList<IUser> GetProjectUsers(long projectId)
        {
            return GetProjectUsers(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Get Project User List
        /// Returns list of project members.
        /// </summary>
        /// <param name="projectKey">project key string</param>
        /// <returns>list of <see cref="IUser"/></returns>
        public IList<IUser> GetProjectUsers(string projectKey)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "users" });
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<List<User>>(api, jss);
            return res.Result.ToList<IUser>();
        }

        /// <summary>
        /// Delete Project User
        /// Removes user from list project members.
        /// </summary>
        /// <param name="projectId">project key</param>
        /// <param name="uid">user id</param>
        /// <returns>deleted <see cref="IUser"/></returns>
        public IUser DeleteProjectUser(long projectId, long uid)
        {
            return DeleteProjectUser(string.Format("{0}", projectId), uid);
        }

        /// <summary>
        /// Delete Project User
        /// Removes user from list project members.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="uid">user id</param>
        /// <returns>deleted <see cref="IUser"/></returns>
        public IUser DeleteProjectUser(string projectKey, long uid)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "users" });
            var jss = new JsonSerializerSettings();
            var kvs = new[] { new KeyValuePair<string, string>("userId", uid.ToString()) };
            var hc = new FormUrlEncodedContent(kvs);

            var res = DeleteApiResult<User>(api, hc, jss);
            return res.Result;
        }

        #endregion

        #region project/admin

        /// <summary>
        /// Add Project Administrator
        /// Adds "Project Administrator" role to user
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="userId">user id</param>
        /// <returns>changed <see cref="IUser"/></returns>
        public IUser AddProjectAdministrator(long projectId, long userId)
        {
            return AddProjectAdministrator(string.Format("{0}", projectId), userId);
        }

        /// <summary>
        /// Add Project Administrator
        /// Adds "Project Administrator" role to user
        /// </summary>
        /// <param name="projectKey">project key string</param>
        /// <param name="userId">user id</param>
        /// <returns>changed <see cref="IUser"/></returns>
        public IUser AddProjectAdministrator(string projectKey, long userId)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "administrators" });
            var jss = new JsonSerializerSettings();
            var kvs = new[] { new KeyValuePair<string, string>("userId", userId.ToString()) };
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<User>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Get List of Project Administrators
        /// Returns list of users who has Project Administrator role
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns>list of administrator <see cref="IUser"/></returns>
        public IList<IUser> GetProjectAdministrators(long projectId)
        {
            return GetProjectAdministrators(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Get List of Project Administrators
        /// Returns list of users who has Project Administrator role
        /// </summary>
        /// <param name="projectKey">project key string</param>
        /// <returns>list of administrator <see cref="IUser"/></returns>
        public IList<IUser> GetProjectAdministrators(string projectKey)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "administrators" });
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<List<User>>(api, jss);
            return res.Result.ToList<IUser>();
        }

        /// <summary>
        /// Delete Project Administrator
        /// Removes Project Administrator role from user
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="uid">user id</param>
        /// <returns>deleted <see cref="IUser"/></returns>
        public IUser DeleteProjectAdministrator(long projectId, long uid)
        {
            return DeleteProjectAdministrator(string.Format("{0}", projectId), uid);
        }

        /// <summary>
        /// Delete Project Administrator
        /// Removes Project Administrator role from user
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="uid">user id</param>
        /// <returns>deleted <see cref="IUser"/></returns>
        public IUser DeleteProjectAdministrator(string projectKey, long uid)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "administrators" });
            var jss = new JsonSerializerSettings();
            var kvs = new[] { new KeyValuePair<string, string>("userId", uid.ToString()) };
            var hc = new FormUrlEncodedContent(kvs);
            var res = DeleteApiResult<User>(api, hc, jss);
            return res.Result;
        }

        #endregion

        #region project/issueType

        /// <summary>
        /// Get Issue Type List
        /// Returns list of Issue Types in the project.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns>list of <see cref="IIssueType"/></returns>
        public IList<IIssueType> GetProjectIssueTypes(long projectId)
        {
            return GetProjectIssueTypes(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Get Issue Type List
        /// Returns list of Issue Types in the project.
        /// </summary>
        /// <param name="projectKey">project key string</param>
        /// <returns>list of <see cref="IIssueType"/></returns>
        public IList<IIssueType> GetProjectIssueTypes(string projectKey)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "issueTypes" });
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<List<IssueType>>(api, jss);
            return res.Result.ToList<IIssueType>();
        }

        /// <summary>
        /// Add Issue Type
        /// Adds new Issue Type to the project.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="options">issue type to add</param>
        /// <returns>added <see cref="IIssueType"/></returns>
        public IIssueType AddProjectIssueType(long projectId, AddProjectIssueTypeOptions options)
        {
            return AddProjectIssueType(string.Format("{0}", projectId), options);
        }

        /// <summary>
        /// Add Issue Type
        /// Adds new Issue Type to the project.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="options">issue type to add</param>
        /// <returns>added <see cref="IIssueType"/></returns>
        public IIssueType AddProjectIssueType(string projectKey, AddProjectIssueTypeOptions options)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "issueTypes" });
            var jss = new JsonSerializerSettings();
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<IssueType>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Update Issue Type
        /// Updates information about Issue Type.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="id">issue type id</param>
        /// <param name="options">issue type to change</param>
        /// <returns>updated <see cref="IIssueType"/></returns>
        public IIssueType UpdateProjectIssueType(long projectId, long id, UpdateProjectIssueTypeOptions options)
        {
            return UpdateProjectIssueType(string.Format("{0}", projectId), id, options);
        }

        /// <summary>
        /// Update Issue Type
        /// Updates information about Issue Type.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="id">issue type id</param>
        /// <param name="options">issue type to change</param>
        /// <returns>updated <see cref="IIssueType"/></returns>
        public IIssueType UpdateProjectIssueType(string projectKey, long id, UpdateProjectIssueTypeOptions options)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "issueTypes", string.Format("{0}", id) });
            var jss = new JsonSerializerSettings();
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PatchApiResult<IssueType>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Delete Issue Type
        /// Deletes Issue Type.
        /// </summary>
        /// <param name="projectId">project key</param>
        /// <param name="id">delete issueType id</param>
        /// <param name="substituteIssueTypeId">Substitute Issue Type Id</param>
        /// <returns>deleted <see cref="IIssueType"/></returns>
        public IIssueType DeleteProjectIssueType(long projectId, long id, long substituteIssueTypeId)
        {
            return DeleteProjectIssueType(string.Format("{0}", projectId), id, substituteIssueTypeId);
        }

        /// <summary>
        /// Delete Issue Type
        /// Deletes Issue Type.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="id">delete issueType id</param>
        /// <param name="substituteIssueTypeId">Substitute Issue Type Id</param>
        /// <returns>deleted <see cref="IIssueType"/></returns>
        public IIssueType DeleteProjectIssueType(string projectKey, long id, long substituteIssueTypeId)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "issueTypes", string.Format("{0}", id) });
            var jss = new JsonSerializerSettings();
            var kvs = new[] { new KeyValuePair<string, string>("substituteIssueTypeId", substituteIssueTypeId.ToString()) };
            var hc = new FormUrlEncodedContent(kvs);
            var res = DeleteApiResult<IssueType>(api, hc, jss);
            return res.Result;
        }

        #endregion

        #region project/category

        /// <summary>
        /// Get Category List
        /// Returns list of Categories in the project.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns>list of project's <see cref="ICategory"/></returns>
        public IList<ICategory> GetProjectCategories(long projectId)
        {
            return GetProjectCategories(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Get Category List
        /// Returns list of Categories in the project.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <returns>list of project's <see cref="ICategory"/></returns>
        public IList<ICategory> GetProjectCategories(string projectKey)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "categories" });
            var jss = new JsonSerializerSettings();
            var res = GetApiResult<List<Category>>(api, jss);
            return res.Result.ToList<ICategory>();
        }

        /// <summary>
        /// Add Category
        /// Adds new Category to the project.
        /// </summary>
        /// <param name="projectId">project key</param>
        /// <param name="options">category to add</param>
        /// <returns>added <see cref="ICategory"/></returns>
        public ICategory AddProjectCategory(long projectId, AddProjectCategoryOptions options)
        {
            return AddProjectCategory(string.Format("{0}", projectId), options);
        }

        /// <summary>
        /// Add Category
        /// Adds new Category to the project.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="options">category to add</param>
        /// <returns>added <see cref="ICategory"/></returns>
        public ICategory AddProjectCategory(string projectKey, AddProjectCategoryOptions options)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "categories" });
            var jss = new JsonSerializerSettings();
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<Category>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Update Category
        /// Updates information about Category.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="categoryId">category id</param>
        /// <param name="options">update category option</param>
        /// <returns>updated <see cref="ICategory"/></returns>
        public ICategory UpdateProjectCategory(long projectId, long categoryId, UpdateProjectCategoryOptions options)
        {
            return UpdateProjectCategory(string.Format("{0}", projectId), categoryId, options);
        }

        /// <summary>
        /// Update Category
        /// Updates information about Category.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="categoryId">category id</param>
        /// <param name="options">update category option</param>
        /// <returns>updated <see cref="ICategory"/></returns>
        public ICategory UpdateProjectCategory(string projectKey, long categoryId, UpdateProjectCategoryOptions options)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "categories", string.Format("{0}", categoryId) });
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var jss = new JsonSerializerSettings();
            var res = PatchApiResult<Category>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Delete Category
        /// Deletes Category.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="categoryId">category id to del</param>
        /// <returns>deletec <see cref="ICategory"/></returns>
        public ICategory DeleteProjectCategory(long projectId, long categoryId)
        {
            return DeleteProjectCategory(string.Format("{0}", projectId), categoryId);
        }

        /// <summary>
        /// Delete Category
        /// Deletes Category.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="categoryId">category id to del</param>
        /// <returns>deletec <see cref="ICategory"/></returns>
        public ICategory DeleteProjectCategory(string projectKey, long categoryId)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "categories", string.Format("{0}", categoryId) });
            var jss = new JsonSerializerSettings();
            var res = DeleteApiResult<Category>(api, jss);
            return res.Result;
        }

        #endregion

        #region project/version

        /// <summary>
        /// Get Version List
        /// Returns list of Versions in the project.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns>list of <see cref="IVersion"/></returns>
        public IList<IVersion> GetProjectVersions(long projectId)
        {
            return GetProjectVersions(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Get Version List
        /// Returns list of Versions in the project.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <returns>list of <see cref="IVersion"/></returns>
        public IList<IVersion> GetProjectVersions(string projectKey)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "versions" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<Data.Version>>(api, jss);
            return res.Result.ToList<IVersion>();
        }

        /// <summary>
        /// Add Version
        /// Adds new Version to the project.
        /// </summary>
        /// <param name="projectId">project key </param>
        /// <param name="options">adding version options</param>
        /// <returns>added <see cref="IVersion"/></returns>
        public IVersion AddProjectVersion(long projectId, AddProjectVersionOptions options)
        {
            return AddProjectVersion(string.Format("{0}", projectId), options);
        }

        /// <summary>
        /// Add Version
        /// Adds new Version to the project.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="options">adding version options</param>
        /// <returns>added <see cref="IVersion"/></returns>
        public IVersion AddProjectVersion(string projectKey, AddProjectVersionOptions options)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "versions" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Include
            };
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<Data.Version>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Update Version
        /// Updates information about Version.
        /// </summary>
        /// <param name="projectId">project key </param>
        /// <param name="id">version id</param>
        /// <param name="options">new version info </param>
        /// <returns>updated <see cref="IVersion"/></returns>
        public IVersion UpdateProjectVersion(long projectId, long id, UpdateProjectVersionOptions options)
        {
            return UpdateProjectVersion(string.Format("{0}", projectId), id, options);
        }

        /// <summary>
        /// Update Version
        /// Updates information about Version.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="id">version id</param>
        /// <param name="options">new version info </param>
        /// <returns>updated <see cref="IVersion"/></returns>
        public IVersion UpdateProjectVersion(string projectKey, long id, UpdateProjectVersionOptions options)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "versions", string.Format("{0}", id) });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Include
            };

            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PatchApiResult<Data.Version>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Delete Version
        /// Deletes Version.
        /// </summary>
        /// <param name="projectId">project key </param>
        /// <param name="id">version id to delete</param>
        /// <returns>deleted <see cref="IVersion"/></returns>
        public IVersion DeleteProjectVersion(long projectId, long id)
        {
            return DeleteProjectVersion(string.Format("{0}", projectId), id);
        }

        /// <summary>
        /// Delete Version
        /// Deletes Version.
        /// </summary>
        /// <param name="projectKey">project key </param>
        /// <param name="id">version id to delete</param>
        /// <returns>deleted <see cref="IVersion"/></returns>
        public IVersion DeleteProjectVersion(string projectKey, long id)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "versions", string.Format("{0}", id) });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };

            var res = DeleteApiResult<Data.Version>(api, jss);
            return res.Result;
        }

        #endregion

        #region project/customfield

        /// <summary>
        /// Get Custom Field List
        /// Returns list of Custom Fields in the project.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns>list of <see cref="ICustomField"/></returns>
        public IList<ICustomField> GetProjectCustomFields(long projectId)
        {
            return GetProjectCustomFields(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Get Custom Field List
        /// Returns list of Custom Fields in the project.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <returns>list of <see cref="ICustomField"/></returns>
        public IList<ICustomField> GetProjectCustomFields(string projectKey)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "customFields" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<CustomField>>(api, jss);
            return res.Result.ToList<ICustomField>();
        }

        /// <summary>
        /// Add Custom Field
        /// Adds new Custom Field to the project.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="options">custom field options to add</param>
        /// <returns>created <see cref="ICustomField"/></returns>
        public ICustomField AddProjectCustomField(long projectId, AddTextTypeCustomFieldOptions options)
        {
            return AddProjectCustomField(string.Format("{0}", projectId), options);
        }

        /// <summary>
        /// Add Custom Field
        /// Adds new Custom Field to the project.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="options">custom field options to add</param>
        /// <returns>created <see cref="ICustomField"/></returns>
        public ICustomField AddProjectCustomField(string projectKey, AddCustomFieldOptions options)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "customFields" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };

            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<CustomField>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Update Custom Field
        /// Updates Custom Field.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="id">custom filed id</param>
        /// <param name="options">field to update</param>
        /// <returns>updated <see cref="ICustomField"/></returns>
        public ICustomField UpdateProjectCustomField(long projectId, long id, UpdateTextCustomFieldOptions options)
        {
            return UpdateProjectCustomField(string.Format("{0}", projectId), id, options);
        }

        /// <summary>
        /// Update Custom Field
        /// Updates Custom Field.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="id">custom filed id</param>
        /// <param name="options">field to update</param>
        /// <returns>updated <see cref="ICustomField"/></returns>
        public ICustomField UpdateProjectCustomField(string projectKey, long id, UpdateCustomFieldOptions options)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "customFields", string.Format("{0}", id) });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };

            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PatchApiResult<CustomField>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Delete Custom Field
        /// Deletes Custom Field.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="id">custom field id</param>
        /// <returns>deleted <see cref="ICustomField"/></returns>
        public ICustomField DeleteProjectCustomField(long projectId, long id)
        {
            return DeleteProjectCustomField(string.Format("{0}", projectId), id);
        }

        /// <summary>
        /// Delete Custom Field
        /// Deletes Custom Field.
        /// </summary>
        /// <param name="projectKey">project key string</param>
        /// <param name="id">custom field id</param>
        /// <returns>deleted <see cref="ICustomField"/></returns>
        public ICustomField DeleteProjectCustomField(string projectKey, long id)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "customFields", string.Format("{0}", id) });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = DeleteApiResult<CustomField>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Add List Item for List Type Custom Field
        /// Adds new list item for list type custom field.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="id">custom field id</param>
        /// <param name="name">name to add</param>
        /// <returns>udated <see cref="ICustomField"/></returns>
        public ICustomField AddProjectCustomFieldListItem(long projectId, long id, string name)
        {
            return AddProjectCustomFieldListItem(string.Format("{0}", projectId), id, name);
        }

        /// <summary>
        /// Add List Item for List Type Custom Field
        /// Adds new list item for list type custom field.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="id">custom field id</param>
        /// <param name="name">name to add</param>
        /// <returns>udated <see cref="ICustomField"/></returns>
        public ICustomField AddProjectCustomFieldListItem(string projectKey, long id, string name)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "customFields", string.Format("{0}", id), "items" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var kvs = new[] { new KeyValuePair<string, string>("name", name) };
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<CustomField>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Update List Item for List Type Custom Field
        /// Updates list item for list type custom field.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="fieldId">field id</param>
        /// <param name="itemId">item id</param>
        /// <param name="name">name to update</param>
        /// <returns>updated <see cref="ICustomField"/></returns>
        public ICustomField UpdateProjectCustomFieldListItem(long projectId, long fieldId, long itemId, string name)
        {
            return UpdateProjectCustomFieldListItem(string.Format("{0}", projectId), fieldId, itemId, name);
        }

        /// <summary>
        /// Update List Item for List Type Custom Field
        /// Updates list item for list type custom field.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="fieldId">field id</param>
        /// <param name="itemId">item id</param>
        /// <param name="name">name to update</param>
        /// <returns>updated <see cref="ICustomField"/></returns>
        public ICustomField UpdateProjectCustomFieldListItem(string projectKey, long fieldId, long itemId, string name)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "customFields", string.Format("{0}", fieldId), "items", string.Format("{0}", itemId) });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var kvs = new[] { new KeyValuePair<string, string>("name", name) };
            var hc = new FormUrlEncodedContent(kvs);
            var res = PatchApiResult<CustomField>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Delete List Item for List Type Custom Field
        /// Deletes list item for list type custom field.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="fieldId">field id</param>
        /// <param name="itemId">item id</param>
        /// <returns>deleted <see cref="ICustomField"/></returns>
        public ICustomField DeleteProjectCustomFieldListItem(long projectId, long fieldId, long itemId)
        {
            return DeleteProjectCustomFieldListItem(string.Format("{0}", projectId), fieldId, itemId);
        }

        /// <summary>
        /// Delete List Item for List Type Custom Field
        /// Deletes list item for list type custom field.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="fieldId">field id</param>
        /// <param name="itemId">item id</param>
        /// <returns>deleted <see cref="ICustomField"/></returns>
        public ICustomField DeleteProjectCustomFieldListItem(string projectKey, long fieldId, long itemId)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "customFields", string.Format("{0}", fieldId), "items", string.Format("{0}", itemId) });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };

            var res = DeleteApiResult<CustomField>(api, jss);
            return res.Result;
        }

        #endregion

        #region project/file

        /// <summary>
        /// Get List of Shared Files
        /// Gets list of Shared Files ( meta data only )
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="path">path </param>
        /// <param name="offset">offset options</param>
        /// <returns>list of <see cref="ISharedFile"/> in path</returns>
        public IList<ISharedFile> GetProjectSharedFiles(long projectId, string path = "", OffsetOptions offset = null)
        {
            return GetProjectSharedFiles(string.Format("{0}", projectId), path, offset);
        }

        /// <summary>
        /// Get List of Shared Files
        /// Gets list of Shared Files ( meta data only )
        /// </summary>
        /// <param name="projectkey">project key</param>
        /// <param name="path">path </param>
        /// <param name="offset">offset options</param>
        /// <returns>list of <see cref="ISharedFile"/> in path</returns>
        public IList<ISharedFile> GetProjectSharedFiles(string projectkey, string path = "", OffsetOptions offset = null)
        {
            var query = offset == null ? null : offset.ToKeyValuePairs();
            var apig = GetApiUri(new[] { "projects", projectkey, "files", "metadata", path }, query);
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };

            var res = GetApiResult<List<SharedFile>>(apig, jss);
            return res.Result.ToList<ISharedFile>();
        }

        /// <summary>
        /// Get File
        /// Downloads the file.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="id">shared file id</param>
        /// <returns>downloaded <see cref="ISharedFileData"/></returns>
        public ISharedFileData GetProjectSharedFile(long projectId, long id)
        {
            return GetProjectSharedFile(string.Format("{0}", projectId), id);
        }

        /// <summary>
        /// Get File
        /// Downloads the file.
        /// </summary>
        /// <param name="projectkey">project key</param>
        /// <param name="id">shared file id</param>
        /// <returns>downloaded <see cref="ISharedFileData"/></returns>
        public ISharedFileData GetProjectSharedFile(string projectkey, long id)
        {
            var api = GetApiUri(new[] { "projects", projectkey, "files", string.Format("{0}", id) });
            var res = GetApiResultAsFile(api);
            var file = new SharedFileData(res.Result.Item1, res.Result.Item2);
            return file;
        }

        #endregion

        #region project/webhook

        /// <summary>
        /// Get List of Webhooks
        /// Returns list of webhooks.
        /// </summary>
        /// <param name="projectId">project key</param>
        /// <returns>list of <see cref="IWebHook"/></returns>
        public IList<IWebHook> GetProjectWebHooks(long projectId)
        {
            return GetProjectWebHooks(string.Format("{0}", projectId));
        }

        /// <summary>
        /// Get List of Webhooks
        /// Returns list of webhooks.
        /// </summary>
        /// <param name="projectkey">project key</param>
        /// <returns>list of <see cref="IWebHook"/></returns>
        public IList<IWebHook> GetProjectWebHooks(string projectkey)
        {
            var api = GetApiUri(new[] { "projects", projectkey, "webhooks" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<WebHook>>(api, jss);
            return res.Result.ToList<IWebHook>();
        }

        /// <summary>
        /// Add Webhook
        /// Adds new webhook.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="options">new webhook options</param>
        /// <returns>added <see cref="IWebHook"/></returns>
        public IWebHook AddProjectWebHook(long projectId, AddWebHookOptions options)
        {
            return AddProjectWebHook(string.Format("{0}", projectId), options);
        }

        /// <summary>
        /// Add Webhook
        /// Adds new webhook.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="options">new webhook options</param>
        /// <returns>added <see cref="IWebHook"/></returns>
        public IWebHook AddProjectWebHook(string projectKey, AddWebHookOptions options)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "webhooks" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PostApiResult<WebHook>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Get Webhook
        /// Returns information about webhook.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="id">webhook id</param>
        /// <returns><see cref="IWebHook"/></returns>
        public IWebHook GetProjectWebHook(long projectId, long id)
        {
            return GetProjectWebHook(string.Format("{0}", projectId), id);
        }

        /// <summary>
        /// Get Webhook
        /// Returns information about webhook.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="id">webhook id</param>
        /// <returns><see cref="IWebHook"/></returns>
        public IWebHook GetProjectWebHook(string projectKey, long id)
        {
            var api = GetApiUri(new[] { "projects", projectKey, "webhooks", string.Format("{0}", id) });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<WebHook>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Update Webhook
        /// Updates information about webhook.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="hookId">webhook id</param>
        /// <param name="options">webhook to update</param>
        /// <returns>updated <see cref="IWebHook"/></returns>
        public IWebHook UpdateProjectWebHook(long projectId, long hookId, UpdateWebHookOptions options)
        {
            return UpdateProjectWebHook(string.Format("{0}", projectId), hookId, options);
        }

        /// <summary>
        /// Update Webhook
        /// Updates information about webhook.
        /// </summary>
        /// <param name="projectkey">project key</param>
        /// <param name="hookId">webhook id</param>
        /// <param name="options">webhook to update</param>
        /// <returns>updated <see cref="IWebHook"/></returns>
        public IWebHook UpdateProjectWebHook(string projectkey, long hookId, UpdateWebHookOptions options)
        {
            var api = GetApiUri(new[] { "projects", projectkey, "webhooks", string.Format("{0}", hookId) });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var kvs = options.ToKeyValuePairs();
            var hc = new FormUrlEncodedContent(kvs);
            var res = PatchApiResult<WebHook>(api, hc, jss);
            return res.Result;
        }

        /// <summary>
        /// Delete Webhook
        /// Deletes webhook.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="hookId">web hook id</param>
        /// <returns>deleted <see cref="IWebHook"/></returns>
        public IWebHook DeleteProjectWebHook(long projectId, long hookId)
        {
            return DeleteProjectWebHook(string.Format("{0}", projectId), hookId);
        }

        /// <summary>
        /// Delete Webhook
        /// Deletes webhook.
        /// </summary>
        /// <param name="projectkey">project key</param>
        /// <param name="hookId">web hook id</param>
        /// <returns>deleted <see cref="IWebHook"/></returns>
        public IWebHook DeleteProjectWebHook(string projectkey, long hookId)
        {
            var api = GetApiUri(new[] { "projects", projectkey, "webhooks", string.Format("{0}", hookId) });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = DeleteApiResult<WebHook>(api, jss);
            return res.Result;
        }

        #endregion

        #region project/git

        /// <summary>
        /// Get List of Git Repositories
        /// Returns list of Git repositories.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns>list of <see cref="IRepositoryDetail"/></returns>
        public IList<IRepositoryDetail> GetProjectGitRepositories(long projectId)
        {
            return GetProjectGitRepositories($"{projectId}");
        }

        /// <summary>
        /// Get List of Git Repositories
        /// Returns list of Git repositories.
        /// </summary>
        /// <param name="projectKey">project key string</param>
        /// <returns>list of <see cref="IRepositoryDetail"/></returns>
        public IList<IRepositoryDetail> GetProjectGitRepositories(string projectKey)
        {
            // /api/v2/projects/:projectIdOrKey/git/repositories
            var api = GetApiUri(new[] { "projects", projectKey, "git", "repositories" });
            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore
            };
            var res = GetApiResult<List<RepositoryDetail>>(api, jss);
            return res.Result.ToList<IRepositoryDetail>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="repoName">repository name</param>
        /// <returns>list of <see cref="IRepositoryDetail"/></returns>
        public IRepositoryDetail GetProjectGitRepository(long projectId, string repoName)
        {
            return GetProjectGitRepository($"{projectId}", repoName);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="repoId">repository id</param>
        /// <returns></returns>
        public IRepositoryDetail GetProjectGitRepository(long projectId, long repoId)
        {
            return GetProjectGitRepository($"{projectId}", $"{repoId}");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="repoId">repository id</param>
        /// <returns></returns>
        public IRepositoryDetail GetProjectGitRepository(string projectKey, long repoId)
        {
            return GetProjectGitRepository(projectKey, $"{repoId}");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="repoName">repository name</param>
        /// <returns>list of <see cref="IRepositoryDetail"/></returns>
        public IRepositoryDetail GetProjectGitRepository(string projectKey, string repoName)
        {
            // /api/v2/projects/:projectIdOrKey/git/repositories/:repoName
            var api = GetApiUri(new[] { "projects", projectKey, "git", "repositories", repoName });
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = GetApiResult<RepositoryDetail>(api, jss);
            return res.Result;
        }

        /// <summary>
        /// Get Pull Request List
        /// Returns list of pull requests.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="repoName">repository name</param>
        /// <returns>list of <see cref="IPullRequest"/></returns>
        public IList<IPullRequest> GetProjectGitRepositoryPullRequests(string projectKey, string repoName)
        {
            // /api/v2/projects/:projectIdOrKey/git/repositories/:repoName/pullRequests
            var api = GetApiUri(new[] { "projects", projectKey, "git", "repositories", repoName, "pullRequests" });
            var jss = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var res = GetApiResult<List<PullRequest>>(api, jss);
            return res.Result.ToList<IPullRequest>();
        }

        /// <summary>
        /// Get Pull Request List
        /// Returns list of pull requests.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="repoName">repository name</param>
        /// <returns>list of <see cref="IPullRequest"/></returns>
        public IList<IPullRequest> GetProjectGitRepositoryPullRequests(long projectId, string repoName)
        {
            return GetProjectGitRepositoryPullRequests($"{projectId}", repoName);
        }

        /// <summary>
        /// Get Pull Request List
        /// Returns list of pull requests.
        /// </summary>
        /// <param name="projectKey">project key</param>
        /// <param name="repoId">repository id</param>
        /// <returns>list of <see cref="IPullRequest"/></returns>
        public IList<IPullRequest> GetProjectGitRepositoryPullRequests(string projectKey, long repoId)
        {
            return GetProjectGitRepositoryPullRequests(projectKey, $"{repoId}");
        }

        /// <summary>
        /// Get Pull Request List
        /// Returns list of pull requests.
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="repoId">repository id</param>
        /// <returns>list of <see cref="IPullRequest"/></returns>
        public IList<IPullRequest> GetProjectGitRepositoryPullRequests(long projectId, long repoId)
        {
            return GetProjectGitRepositoryPullRequests($"{projectId}", $"{repoId}");
        }

        #endregion
    }
}