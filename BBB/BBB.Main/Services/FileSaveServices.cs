using BBB.Data;
using BBB.Data.Entities;
using System;
using System.Threading.Tasks;

namespace BBB.Main.Services
{
    public class FileSaveServices : IFileSaveServices
    {
        private ApplicationDbContext _context;
        public FileSaveServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddFileSave(FileSave fileSave)
        {
            try
            {
                _context.FileSaves.Add(fileSave);
                var response = await _context.SaveChangesAsync();
                if (response < 1)
                {
                    return "Cannot execute. Plz contact Admin";
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public string DeleteFileSave(FileSave fileSave)
        {
            try
            {
                _context.FileSaves.Attach(fileSave);
                _context.FileSaves.Remove(fileSave);
                var response = _context.SaveChanges();
                if (response < 1)
                {
                    return "Cannot execute. Plz contact Admin";
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public async Task<string> UpdateCategory(FileSave fileSave)
        {
            try
            {
                _context.FileSaves.Update(fileSave);
                var response = await _context.SaveChangesAsync();
                if (response < 1)
                {
                    return "Cannot execute. Plz contact Admin";
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
