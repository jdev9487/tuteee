import * as React from 'react';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import EmailIcon from '@mui/icons-material/Email';
import { Box, IconButton } from '@mui/material';
import ReceiptIcon from '@mui/icons-material/Receipt';
import Divider from '@mui/material/Divider';
import EditIcon from '@mui/icons-material/Edit';
import PostAddIcon from '@mui/icons-material/PostAdd';
import PersonAddIcon from '@mui/icons-material/PersonAdd';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import { updateCustomer } from '../Fetch/Customer';

export default function CustomerCard(props) {
    const [editCustomerOpen, setEditCustomerOpen] = React.useState(false);
    const [customerFirstName, setCustomerFirstName] = React.useState(props.firstName)
    const [customerLastName, setCustomerLastName] = React.useState(props.lastName)
    const [customerPhone, setCustomerPhone] = React.useState(props.phone)
    const [customerEmail, setCustomerEmail] = React.useState(props.email)

    const handleEditCustomerOpen = () => {
        setEditCustomerOpen(true);
    };

    const handleConfirmEditCustomer = (event) => {
        event.preventDefault();
        const firstName = event.target[0].value;
        const lastName = event.target[1].value;
        const email = event.target[2].value;
        const phone = event.target[3].value;
        updateCustomer(props.id, firstName, lastName, email, phone,
            (fn) => setCustomerFirstName(fn),
            (ln) => setCustomerLastName(ln),
            (e) => setCustomerEmail(e),
            (p) => setCustomerPhone(p));
        setEditCustomerOpen(false);
        return true;
    };
    const handleEditCustomerClose = () => {
        setEditCustomerOpen(false);
    };

    return (
        <div>
            <Card sx={{ m: 1, background: '#f0f0f0' }}>
                <CardContent>
                    <Box sx={{ display: 'flex', flexDirection: 'row', alignItems: 'center' }}>
                        <Box sx={{ flexGrow: 1, display: 'flex', flexDirection: 'row', alignItems: 'center' }}>
                            <Typography mr={1} align='left' variant="h5" component="div">
                                {customerFirstName} {customerLastName}
                            </Typography>
                            <IconButton onClick={handleEditCustomerOpen}>
                                <EditIcon />
                            </IconButton>
                            <IconButton>
                                <PostAddIcon />
                            </IconButton>
                            <IconButton>
                                <PersonAddIcon />
                            </IconButton>
                        </Box>
                        <Typography variant="h6" color='text.secondary' align='right' component="div">
                            {customerPhone}
                        </Typography>
                        <Divider sx={{ mx: 1 }} orientation="vertical" flexItem />
                        <Typography variant="h6" color='text.secondary' align='right' component="div">
                            {customerEmail}
                        </Typography>
                    </Box>
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
                        {[].concat(...props.students.map((x, i) => [<Typography key={i} variant='h6' sx={{ textDecoration: 'none' }}>
                            {x.firstName} {x.lastName}
                        </Typography>, <Divider key={i} sx={{ mx: 1 }} orientation="vertical" flexItem />])).slice(0, -1)}
                    </Box>
                </CardContent>
                <CardActions>
                    <Button variant="outlined" startIcon={<EmailIcon />}>
                        Contact
                    </Button>
                    <Button color='success' variant="outlined" startIcon={<ReceiptIcon />}>
                        Invoice
                    </Button>
                </CardActions>
            </Card>
            <Dialog open={editCustomerOpen} onClose={handleEditCustomerClose}>
                <DialogTitle>Edit {customerFirstName} {customerLastName}</DialogTitle>
                <DialogContent>
                    <DialogContentText align='center'>
                        ALL ACTIONS ARE FINAL!
                    </DialogContentText>
                    <form id='edit-customer-form' onSubmit={handleConfirmEditCustomer}>
                        <TextField margin="dense" defaultValue={customerFirstName} id="first-name" label="First name" fullWidth variant="standard" />
                        <TextField margin="dense" defaultValue={customerLastName} id="last-name" label="Last name" fullWidth variant="standard" />
                        <TextField margin="dense" defaultValue={customerEmail} id="email" label="Email" type='email' fullWidth variant="standard" />
                        <TextField margin="dense" defaultValue={customerPhone} id="phone" label="Phone" type='tel' fullWidth variant="standard" />
                    </form>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleEditCustomerClose}>Cancel</Button>
                    <Button type='submit' color='warning' form='edit-customer-form'>Update</Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}