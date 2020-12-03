using MoviesEF.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesEF.Entities;
using MoviesEF.Models;

namespace MoviesEF.Services
{
    public class DirectorService
    {
        MoviesContext db = new MoviesContext();
        public List<DirectorModel> List()    //düzenleyeceğin List of modelname
        {
            try
            {
                return db.Directors.OrderBy(director => director.Name).ThenBy(director => director.Surname).Select(director => new DirectorModel()
                {
                    Id = director.Id,
                    Name = director.Name,
                    Surname = director.Surname,
                    Retired = director.Retired
                }).ToList();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public void Add(DirectorModel directorModel)
        {
            try
            {
                var directorEntity = new Director()
                {
                    Id = directorModel.Id,
                    Name = directorModel.Name,
                    Surname = directorModel.Surname,
                    Retired = directorModel.Retired
                };
                db.Directors.Add(directorEntity);
                db.SaveChanges();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public DirectorModel Details(int id)
        {
            try
            {
                Director directorEntity = db.Directors.Find(id);
                DirectorModel directorModel = new DirectorModel()
                {
                    Id = directorEntity.Id,
                    Name = directorEntity.Name,
                    Surname = directorEntity.Surname,
                    Retired = directorEntity.Retired
                };
                return directorModel;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void Update(DirectorModel model)
        {
            try
            {
                Director entity = db.Directors.Find(model.Id);
                entity.Name = model.Name;
                entity.Surname = model.Surname;
                entity.Retired = model.Retired;
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
