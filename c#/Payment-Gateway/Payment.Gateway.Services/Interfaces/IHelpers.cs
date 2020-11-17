namespace Payment.Gateway.Logic.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="IHelpers" />
    /// </summary>
    public interface IHelpers
    {
        /// <summary>
        /// The GetAccessToken
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns>The <see cref="Task{TEntity}"/></returns>
        Task<TEntity> GetAccessTokenAsync<TEntity>() where TEntity : class;

        /// <summary>
        /// The AddDigitalOrderField
        /// </summary>
        /// <param name="key">The key<see cref="string"/></param>
        /// <param name="value">The value<see cref="string"/></param>
        void AddDigitalOrderField(string key, string value);

        /// <summary>
        /// The CreateSha256SignatureAsync
        /// </summary>
        /// <param name="useRequest">The useRequest<see cref="bool"/></param>
        /// <returns>The <see cref="Task{string}"/></returns>
        Task<string> CreateSha256SignatureAsync(bool useRequest);

        /// <summary>
        /// The GetHiddenRequestFormAsync
        /// </summary>
        /// <returns>The <see cref="Task{string}"/></returns>
        Task<string> GetHiddenRequestFormAsync();

        /// <summary>
        /// The CreateUser
        /// </summary>
        /// <returns>The <see cref="Task{string}"/></returns>
        Task<string> CreateReferenceIDAsync(string uuid);

        /// <summary>
        /// The AddSecureHashToXmlDocAsync
        /// </summary>
        /// <param name="doc">The doc<see cref="XmlDocument"/></param>
        /// <param name="root">The root<see cref="XmlNode"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task AddSecureHashToXmlDocAsync(XmlDocument doc, XmlNode root);

        /// <summary>
        /// The GetRequestRaw
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        string GetRequestRaw();

        /// <summary>
        /// The BuildObject
        /// </summary>
        /// <param name="resultString">The resultString<see cref="string"/></param>
        /// <returns>The <see cref="Dictionary{string, dynamic}"/></returns>
        Dictionary<string, dynamic> BuildObject(string resultString);
    }
}
