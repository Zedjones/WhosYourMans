import React, { Component } from 'react';
import { Message } from './Message';

export class History extends Component {


    render() {
        return (
            <div style={historyStyle}> 
                <ol>{this.props.questions && this.props.questions.slice(0).reverse().map((val, i) => {
                    let ans = "Unanswered"
                    if(val.answer === true)
                        ans = "Yes"
                    else if (val.answer === false)
                        ans = "No"
                    return <li key={i}><Message player={val.player} question={val.question} answer={ans}></Message></li>;
                    })}</ol>
            </div>
        );
    }
}

const historyStyle = {
    width: '100%',
    height: '400px',
    border: '1px solid #ccc',
    borderRadius: '5px',
    overflowY: 'scroll',
    marginTop: '5px'
}