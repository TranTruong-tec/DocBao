
namespace WebDocBao.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class BaiViet
    {
        public BaiViet()
        {
            this.Comments = new HashSet<Comment>();
        }

        public string maBaiViet { get; set; }
        [Required(ErrorMessage = "Chưa nhập tự bài")]
        
        public string tuaBaiViet { get; set; }
        [Required(ErrorMessage = "Chưa nhập nội dung tóm tắt")]
        public string noiDungTomTat { get; set; }
        [Required(ErrorMessage = "Chưa nhập nội dung chính")]
        public string noiDungChinh { get; set; }
        
        public Nullable<System.DateTime> ngayDang { get; set; }
        
        public string maLoai { get; set; }
        
        public string hinhAnh { get; set; }
        public Nullable<int> soLuotXem { get; set; }

        public virtual LoaiBaiViet LoaiBaiViet { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
