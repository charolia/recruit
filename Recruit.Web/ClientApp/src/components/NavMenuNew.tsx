import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { withRouter } from 'react-router-dom';

import './NavMenu.css';

export class NavMenuNew extends Component {
    static displayName = NavMenuNew.name;

    state = {
        navigate: false
    }

    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this.props.history.push('/redirect-to');
    }

    handleClick(e) {
        e.preventDefault()
        /* Look at here, you can add it here */
        this.props.history.push('/contents');
    }

    render() {
        

        return (
            <header>
                <Navbar className=" ng-white border-bottom box-shadow mb-3" light>
                    <NavbarToggler onClick={this.handleClick} className="mr-2" />
                </Navbar>
            </header>
        );
    }
}

export default withRouter(NavMenuNew);