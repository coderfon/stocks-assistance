import { Fragment } from 'react';
import { Outlet } from 'react-router-dom';
import './navigation-bar.style.css';

const NavigationBar = () => {

    return (
        <Fragment>
            <div className='navbar'>
                <span>Stocks Assistance</span>
            </div>
            <Outlet />
        </Fragment>
    );
};

export default NavigationBar;