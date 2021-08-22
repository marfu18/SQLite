using SQLite.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SQLite.controlador
{
    public class crud
    {
        conexion conn = new conexion();


        public Task<List<personas>> getReadPersonas()
        {
            return conn.GetConnectionAsync().Table<personas>().ToListAsync();
        }

        public Task<personas> getId(int id)
        {
            return conn
                .GetConnectionAsync()
                .Table<personas>()
                .Where(item => item.id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> getUpdateId(personas personas)
        {
            return conn
                .GetConnectionAsync()
                .UpdateAsync(personas);

        }

        public Task<int> Delete(personas personas)
        {
            return conn
                .GetConnectionAsync()
                .DeleteAsync(personas);
        }


    }
}

