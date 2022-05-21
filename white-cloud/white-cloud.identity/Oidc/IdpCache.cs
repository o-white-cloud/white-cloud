namespace white_cloud.identity
{
    public class IdpCache : Dictionary<string, IdpInfo>
    {
        public IdpCache(IEnumerable<IdpInfo> idps)
        {
            foreach(var idp in idps)
            {
                Add(idp.Name, idp);
            }
        }
    }
}
