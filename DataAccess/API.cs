using ECBuilder.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ECBuilder.DataAccess
{
    /// <summary>
    /// Class that runs data methods on the set <see cref="ECBuilderSettings.APIClient">Client</see>.
    /// </summary>
    public class API
    {
        private static HttpResponseMessage HttpResponseMessage { get; set; }

        /// <summary>
        /// Gets the entity whose <paramref name="id"/> is given.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="id">Entity ID</param>
        /// <param name="state">Entity state</param>
        /// <returns>Entity <typeparamref name="T"/></returns>
        public static async Task<T> Get<T>(int id, int state = 1) where T : IEntity
        {
            HttpResponseMessage = await ECBuilderSettings.APIClient.GetAsync($"api/{typeof(T).Name}/{typeof(T).Name.ToLower()}Get?entityState={state}&entityID={id}");
            string json = await HttpResponseMessage.Content.ReadAsStringAsync();
            return (JsonConvert.DeserializeObject<List<T>>(json)).FirstOrDefault();
        }

        /// <summary>
        /// Gets all entities given the entity type.
        /// </summary>
        /// <typeparam name="T">Entities type</typeparam>
        /// <param name="state">Entities state</param>
        /// <returns><see cref="List{T}"/></returns>
        public static async Task<List<T>> GetAll<T>(int state = 1) where T : IEntity
        {
            HttpResponseMessage = await ECBuilderSettings.APIClient.GetAsync($"api/{typeof(T).Name}/{typeof(T).Name.ToLower()}Get?entityState={state}&entityID=0");
            string json = await HttpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        /// <summary>
        /// Gets all entities given the entity type.
        /// </summary>
        /// <param name="type">Entities type</param>
        /// <param name="state">Entities state</param>
        /// <returns><see cref="List{T}"/></returns>
        public static async Task<List<IEntity>> GetAll(Type type, int state = 1)
        {
            HttpResponseMessage = await ECBuilderSettings.APIClient.GetAsync($"api/{type.Name}/{type.Name.ToLower()}Get?entityState={state}&entityID=0");
            string json = await HttpResponseMessage.Content.ReadAsStringAsync();

            IEnumerable<IEntity> list = (IEnumerable<IEntity>)JsonConvert.DeserializeObject(json, typeof(IEnumerable<>).MakeGenericType(type));
            return list.ToList();
        }

        /// <summary>
        /// Creates the given Entity.
        /// </summary>
        /// <param name="entity">Entity to be created</param>
        /// <returns><paramref name="entity"/> ID</returns>
        public static async Task<int> Create(IEntity entity)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            HttpResponseMessage = await ECBuilderSettings.APIClient.PostAsync($"api/{entity.GetType().Name}/{entity.GetType().Name.ToLower()}Add", content);
            string json = await HttpResponseMessage.Content.ReadAsStringAsync();

            JObject jobject = JObject.Parse(json);
            int value = Convert.ToInt32(jobject.GetValue($"{char.ToLower(entity.GetType().Name[0]) + entity.GetType().Name.Substring(1)}ID"));

            entity.GetType().GetProperty($"{entity.GetType().Name}ID").SetValue(entity, value);

            return value;
        }

        /// <summary>
        /// Updates the given Entity.
        /// </summary>
        /// <param name="entity">Entity to be updated</param>
        /// <returns>returned JSON message</returns>
        public static async Task<string> Update(IEntity entity)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            HttpResponseMessage = await ECBuilderSettings.APIClient.PostAsync($"api/{entity.GetType().Name}/{entity.GetType().Name.ToLower()}Update", content);
            string json = await HttpResponseMessage.Content.ReadAsStringAsync();
            return json;
        }

        /// <summary>
        /// Deletes the given Entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        /// <returns>returned JSON message</returns>
        public static async Task<string> Delete(IEntity entity)
        {
            HttpResponseMessage = await ECBuilderSettings.APIClient.GetAsync($"api/{entity.GetType().Name}/{entity.GetType().Name.ToLower()}Delete?id={entity.GetType().GetProperty($"{entity.GetType().Name}ID").GetValue(entity)}");
            string json = await HttpResponseMessage.Content.ReadAsStringAsync();
            return json;
        }

        /// <summary>
        /// Logical deletes the given Entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        /// <returns>returned JSON message</returns>
        public static async Task LogicalDelete(IEntity entity)
        {
            entity.GetType().GetProperty($"{entity.GetType().Name}State").SetValue(entity, false);
            await Update(entity);
        }
    }
}
