import React, { Component } from 'react';
import Bug from "./Types/Type_Bug.gif"
import Dark from "./Types/Type_Dark.gif"
import Dragon from "./Types/Type_Dragon.gif"
import Electric from "./Types/Type_Electric.gif"
import Fairy from "./Types/Type_Fairy.gif"
import Fighting from "./Types/Type_Fighting.gif"
import Fire from "./Types/Type_Fire.gif"
import Flying from "./Types/Type_Flying.gif"
import Ghost from "./Types/Type_Ghost.gif"
import Grass from "./Types/Type_Grass.gif"
import Ground from "./Types/Type_Ground.gif"
import Ice from "./Types/Type_Ice.gif"
import Normal from "./Types/Type_Normal.gif"
import Poison from "./Types/Type_Poison.gif"
import Psychic from "./Types/Type_Psychic.gif"
import Rock from "./Types/Type_Rock.gif"
import Steel from "./Types/Type_Steel.gif"
import Water from "./Types/Type_Water.gif"


export class CardInfo extends Component {
    constructor(props) {
        super(props)
        this.state = { marked: false }
        this.sendGuess = this.sendGuess.bind(this)
    }

    sendGuess() {
        fetch(`/api/room/guess?guess=${this.props.val.name}`, { method: 'post' })
            .then((resp) => {
                if (resp.ok === true){
                    this.props.gameOver(true)
                } else {
                    this.props.gameOver(false)
                }
            })
        this.props.updateGame()
    }

    render() {
        return (
            <div style={popupStyling}>
                <h4>{this.props.val.name}</h4>
                <div>{this.props.val.types.map((type, i) => { 
                    switch(type){
                        case "bug":
                            return <img src={Bug} alt="Bug" />
                        case "dark":
                            return <img src={Dark} alt="Dark" />
                        case "dragon":
                            return <img src={Dragon} alt="Dragon" />
                        case "electric":
                            return <img src={Electric} alt="Eletric" />
                        case "fairy":
                            return <img src={Fairy} alt="Fairy" />
                        case "fighting":
                            return <img src={Fighting} alt="Fighting" />
                        case "fire":
                            return <img src={Fire} alt="Fire" />
                        case "flying":
                            return <img src={Flying} alt="Flying" />
                        case "ghost":
                            return <img src={Ghost} alt="Ghost" />
                        case "grass":
                            return <img src={Grass} alt="Grass" />
                        case "ground":
                            return <img src={Ground} alt="Ground" />
                        case "ice":
                            return <img src={Ice} alt="Ice" />
                        case "normal":
                            return <img src={Normal} alt="Normal" />
                        case "poison":
                            return <img src={Poison} alt="Poison" />
                        case "psychic":
                            return <img src={Psychic} alt="Psychic" />
                        case "rock":
                            return <img src={Rock} alt="Rock" />
                        case "steel":
                            return <img src={Steel} alt="Steel" />
                        case "water":
                            return <img src={Water} alt="Water" />
                    }
                })}</div>
                <p>Generation: {this.props.val.generation}</p>
                <button style={buttonStyle} onClick={this.sendGuess}>Guess this Pokemon?</button>
            </div>
        )
    }
}

const popupStyling = { 
    cursor: 'pointer',
    border: 'none',
    alignContent: 'center',
    padding: '10px'
}

const buttonStyle = {
    border: '2px solid #62c1f5',
    borderRadius: '15px',
    padding: '5px'
}