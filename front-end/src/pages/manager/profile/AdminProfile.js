import React from "react";
import Wrapper from "../../../components/layout/defaultLayout/wrapper/Wrapper";
import Sidebar from "../../../components/layout/sidebar/Sidebar";

const AdminProfile = () => {
    return (
        <div className="flex">
            <Sidebar />
            <Wrapper>
                <form className='Create-event max-w-[700px]' method='post'>
                    <div className='form-row' >
                        <label className="form-label">Tên quản trị viên</label>
                        <input className='form-input' type="text" name="user_name" required />
                    </div>
                    <div className='form-row' >
                        <label className="form-label">Email</label>
                        <input className="form-input" type="email" name="user_email" required />
                    </div>
                    <div className='form-row' >
                        <label className="form-label">Số điện thoại</label>
                        <input className="form-input" type="tel" name="user_phone" required />
                    </div>
                    <div className='form-row' >
                        <label className='form-label'>Link facebook</label>
                        <input className="form-input" type="url" name="user_link" required />
                    </div>
                    <div className='form-row' >
                        <button className='form-submit'>Cập nhật</button>
                    </div>
                </form>
            </Wrapper>
        </div>
    )
}
export default AdminProfile;