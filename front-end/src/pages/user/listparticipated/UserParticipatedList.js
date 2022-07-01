
import { Table } from 'reactstrap';
import { useEffect, useState } from 'react';
import axios from 'axios';
import '../../../App.scss';
import { useParams } from 'react-router-dom';
import Loading from '../../../components/loading/loading';

function UserParticipatedList() {
    const { id } = useParams();
    const [loading, setLoading] = useState(false);
    const [students, setStudents] = useState();

    useEffect(() => {
        setLoading(true);
        axios
            .get(
                `http://localhost:5000/api/EventParticipated/get-user-list-from-event?id=${id}`,
            )
            .then((res) => {
                const data = res.data
                setStudents(data);
                setTimeout(() => {
                    setLoading(false);
                }, 500);
            })
            .catch((error) => {
                console.log(error.response);
                setTimeout(() => {
                    setLoading(false);
                }, 500);
            });
    }, []);

    return loading ? (
        <Loading />
    ) : (
        <div className="flex">
           
            <Table className="m-[20px] w-[900px]">
                <thead>
                    <tr>
                        <th>MSSV</th>
                        <th>Tên sinh viên</th>
                        <th>Ngày đăng kí</th>
                        <th>Thanh toán</th>
                        <th>Đã tham gia</th>
                    </tr>
                </thead>
                <tbody>
                    {students?.map((student) => {
                        return (
                            <tr className="hover:bg-[#f99779]">
                                <td>{student?.users_id}</td>
                                <td>asjbdsj</td>
                                <td>{student.date_participated}</td>
                                <td>
                                    <input type="checkbox" value="true" />
                                </td>
                                <td>
                                    <input type="checkbox" value="true" />
                                </td>
                            </tr>
                        );
                    })}
                </tbody>
            </Table>
        </div>
    );
}

export default UserParticipatedList;
