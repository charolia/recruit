import React from "react";

export class CardRegisterForm extends React.Component {

    constructor() {
        super();

        this.state = {
            ccNumber: '',
            cvcNumber: '',
            expiry: ''
        };

    }

    handleCCChange = evt => {
        this.setState({ ccNumber: evt.target.value });
    };

    handleCVCChange = evt => {
        this.setState({ cvcNumber: evt.target.value });
    };

    handleExpiryChange = evt => {
        this.setState({ expiry: evt.target.value });
    };

    handleSubmit = (e) => {
        console.log(this.state);
        e.preventDefault();
    };

    render() {
        const { ccNumber, cvcNumber, expiry } = this.state;
        const isEnabled = ccNumber.length > 0 && cvcNumber.length > 0 && expiry.length > 0;
        return (
            <form onSubmit={this.handleSubmit}>

                <div className="row">

                    <div className="form-group col-12">
                        <label>Credit Card Number:</label>
                        <input className="form-control" aria-describedby="ccNumberHelp"
                            onChange={this.handleCCChange}
                            value={this.state.ccNumber} placeholder="credit card number" />
                        <small id="ccNumberHelp" className="form-text text-muted">We'll never share your credit card number with anyone else.</small>
                    </div>

                    <div className="form-group col-12 col-sm-6">
                        <label>CVC:</label>
                        <input className="form-control" aria-describedby="cvcNumberHelp"
                            onChange={this.handleCVCChange}
                            value={this.state.cvcNumber}
                            placeholder="cvc" />
                        <small id="cvcNumberHelp" className="form-text text-muted">enter cvc number mentioned on your card.</small>
                    </div>

                    <div className="form-group col-12 col-sm-6">
                        <label>Expiry:</label>
                        <input className="form-control" aria-describedby="expirtyHelp"
                            onChange={this.handleExpiryChange}
                            value={this.state.expiry}
                            placeholder="expiry date" />
                        <small id="expiryHelp" className="form-text text-muted">enter expiry date.</small>
                    </div>

                    <br />
                    <div className="form-group col-12 text-center">
                        <button className="btn btn-primary" disabled={!isEnabled}>Submit</button>
                    </div>

                </div>

            </form>
        );
    }
}