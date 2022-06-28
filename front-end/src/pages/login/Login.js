import GoogleButton from 'react-google-button';
import '../../App.scss';

function Login() {
    const logingoogle = () => {
        fetch("http://localhost:5000/api/Authentication/google-login").then(
            response => response.json()
        )
        .then(
            data => {
                console.log(data)
            }
        ).catch(error => {
            console.log(error);
        })
    }
    return (
        <>
            <div className="login ">
                <form className="admin-form">
                    <p className="">Nếu bạn là <strong>quản trị viên</strong>, đăng nhập ở đây</p>
                    <div className="">
                        <div className="form-row">
                            <input type="email" className="form-input" id="inputAccount"
                                placeholder="Tài khoản" />
                        </div>
                        <div className="form-row">
                            <input type="password" className="form-input" id="inputPassword"
                                placeholder="Mật khẩu" />
                        </div>
                        <div className='form-row' >
                            <button className='form-submit'>Đăng nhập</button>
                        </div>
                    </div>
                </form>
                <div className="student-form">
                    <p className="">Nếu bạn là <strong>sinh viên</strong>, đăng nhập với fpt.edu.vn</p>
                    <GoogleButton className="googleButton"
                        onClick={logingoogle}
                    />
                </div>
            </div>
        </>
    )
}

export default Login;