import * as React from 'react';
import {
    Card,
    CardActions,
    CardContent,
    CardMedia,
    Button,
    Typography,
    Box,
    Grid,
    Paper,
    FormControl,
    Input,
    Select,
    MenuItem,
    experimentalStyled as styled,
} from '@mui/material/';
import { useEffect, useState } from 'react';
import { useAdminEvents } from '../../recoil/adminEvents';
import { useSnackbar } from '../../HOCs';
import { Link } from 'react-router-dom';


const Item = styled(Paper)(({ theme }) => ({
    backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
    ...theme.typography.body2,
    padding: theme.spacing(2),
    textAlign: 'center',
    color: theme.palette.text.secondary,
}));

const AdminHome = () => {
    const [events, setEvents] = useState([]);
    const showSackbar = useSnackbar();
    const { getEvents } = useAdminEvents();
    useEffect(() => {
        getEvents()
            .then((resposne) => {
                const data = resposne.data.data;
                setEvents(data);
            })
            .catch(() => {
                showSackbar({
                    severity: 'error',
                    children: 'Something went wrong, please try again later.',
                });
            });
    }, []);

    const [values, setValues] = useState();

    const handleChange = (event) => {
        this.setState({ value: event.target.value });
    };

    const handleSubmit = (event) => {
        alert('A name was submitted: ' + this.state.value);
        event.preventDefault();
    };

    return (
        <>
            <Box sx={{ flexGrow: 1 }}>
                <Grid
                    container
                    padding={{ xs: 2, md: 5 }}
                    columns={{ xs: 4, sm: 12 }}
                >
                    <Grid xs={4} padding={{ sm: 2 }}>
                        <FormControl fullWidth>
                            <Input
                                placeholder="Tên sự kiện..."
                                onChange={handleChange}
                                name="searchEvent"
                                id="searchEventName"
                                type="text"
                            />
                        </FormControl>
                    </Grid>
                    <Grid xs={4} padding={{ sm: 2 }}>
                        <FormControl fullWidth>
                            <Input
                                // onChange={eventTimeStart}
                                name="searchEvent"
                                id="searchEventTime"
                                type="date"
                            />
                        </FormControl>
                    </Grid>
                    <Grid xs={4} padding={{ sm: 2 }} >
                        <FormControl fullWidth variant="standard" >
                            <Select
                                id="searchStatus"
                                onChange={handleChange}
                                // defaultValue={EventStatus}
                            >
                                <MenuItem value={true}>Sắp diễn ra</MenuItem>
                                <MenuItem value={false}>Đã diễn ra</MenuItem>
                            </Select>
                        </FormControl>
                    </Grid>
                </Grid>
            </Box>
            <Box sx={{ flexGrow: 1 }}>
                <Grid
                    container
                    padding={{ xs: 2, md: 5 }}
                    spacing={{ xs: 2, md: 3 }}
                    columns={{ xs: 4, sm: 8, md: 12 }}
                >
                    {events?.map((event) => (
                        <Grid item xs={2} sm={4} md={4} key={event?.event_id}>
                            <Card>
                                <CardMedia
                                    component="img"
                                    height="140"
                                    image="/assets/images/campus.jpg"
                                    alt="green iguana"
                                />
                                <CardContent>
                                    <Typography
                                        gutterBottom
                                        variant="h5"
                                        component="div"
                                    >
                                        {event.event_name}
                                    </Typography>
                                    <Typography
                                        variant="body2"
                                        color="text.secondary"
                                    >
                                        {event.event_start} - {event.event_end}
                                    </Typography>
                                </CardContent>
                                <CardActions>
                                    <Button size="small">Share</Button>
                                    
                                    
                                    <Link
                                        to={`/user/eventdetail/${event.event_id}`}
                                    >
                                        <Button size="small">More detail</Button>
                                            
                                    </Link>
                                </CardActions>
                            </Card>
                        </Grid>
                    ))}
                </Grid>
            </Box>
        </>
    );
};

export default AdminHome;
