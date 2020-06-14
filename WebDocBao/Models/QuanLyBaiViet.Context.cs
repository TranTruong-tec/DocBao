using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WebDocBao.Models;

namespace WebDocBao.Controllers
{
    public partial class DocBaoEntities : DbContext
    {
        public DocBaoEntities()
            : base("name=Personal_FSoftEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<BaiViet> BaiViets { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<LoaiBaiViet> LoaiBaiViets { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
    }
}