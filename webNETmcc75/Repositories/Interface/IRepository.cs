namespace webNETmcc75.Repositories.Interface
{
    interface IRepository<Key, Entity> where Entity : class
    {
        //GetAll
        List<Entity> GetAll();
        //GetByID
        Entity GetById(Key key);
        //crate
        int Insert(Entity entity);
        //Update
        int Update(Entity entity);
        //Delet
        int Delete(Key key);
    }
}
