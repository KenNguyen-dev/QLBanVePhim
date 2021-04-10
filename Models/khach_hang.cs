﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLBanVePhim.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class khach_hang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public khach_hang()
        {
            this.ve_dat = new HashSet<ve_dat>();
        }
    
        public int id { get; set; }
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(50), MinLength(5)]
        [Display(Name = "Tên User")]
        public string ho_ten { get; set; }

        [Display(Name = "CMND")]
        [Required(ErrorMessage = "CMND không được để trống")]
        public string so_cmnd { get; set; }

        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage = "Password không được để trống")]
        [DataType(DataType.Password)]
        public string mat_khau { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Điện thoại không được để trống")]
        [DataType(DataType.PhoneNumber)]
        public string so_dien_thoai { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email không được để trống")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Display(Name = "Địa chỉ")]
        public string dia_chi { get; set; }
        [Display(Name = "Ngày đăng ký")]
        public Nullable<System.DateTime> ngay_dang_ky { get; set; }

        [Display(Name = "Ngày Sinh")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ngay_sinh { get; set; }

        [Display(Name = "Giới tính")]
        public Nullable<bool> gioi_tinh { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ve_dat> ve_dat { get; set; }
    }
}
