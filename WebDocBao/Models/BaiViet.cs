
namespace WebDocBao.Models
{
    using System;
    using System.Collections.Generic;

    public partial class BaiViet
    {
        public BaiViet()
        {
            this.Comments = new HashSet<Comment>();
        }

        public string maBaiViet { get; set; }
        public string tuaBaiViet { get; set; }
        public string noiDungTomTat { get; set; }
        public string noiDungChinh { get; set; }
        public Nullable<System.DateTime> ngayDang { get; set; }
        public string maLoai { get; set; }
        public string hinhAnh { get; set; }
        public Nullable<int> soLuotXem { get; set; }

        public virtual LoaiBaiViet LoaiBaiViet { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
