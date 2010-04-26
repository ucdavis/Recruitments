using System;
using System.Reflection;

namespace CAESDO.Recruitment.Test
{
    public class EntityIdSetter
    {
        /// <summary>
        /// Uses reflection to set the Id of a <see cref="EntityWithTypedId" />.
        /// </summary>
        public static void SetIdOf<IdT>(object Entity, IdT id)
        {
            // Set the data property reflectively
            PropertyInfo idProperty = Entity.GetType().GetProperty("ID",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

            if (idProperty == null) throw new ArgumentException("idProperty could not be found");

            idProperty.SetValue(Entity, id, null);
        }
    }
}