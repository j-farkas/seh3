import React, { Component } from 'react';
import "./styles.css";
import axios from 'axios';
import qs from 'qs';

export class Home extends Component {
  static displayName = Home.name;

  constructor (props) {
    super(props);
    this.state = { images: [], title: "", text: ""};
    this.getImages = this.getImages.bind(this);
    this.createPpt = this.createPpt.bind(this);
    this.selectImage = this.selectImage.bind(this);
    this.handleChange = this.handleChange.bind(this);
    this.handleTextChange = this.handleTextChange.bind(this);
  }

handleChange(event){
  this.setState({title: event.target.value})
  console.log(this.state);
}

handleTextChange(event){
  this.setState({text: event.target.value})
  console.log(this.state);
}

getImages(){
  console.log('images');
  let bolded = this.state.text.split(" ");
  bolded = bolded.map(e=>{
    if(e.split("(b)").length > 0){
      return e.split("(b)")[1];
    }
  })
  let words = this.state.title + bolded.join(" ");
  console.log(words);
  fetch('api/SampleData/ImageList/'+words)
    .then(response => response.json())
    .then(data => {
      this.setState({ images: data});
      console.log(this.state);
    });
}

createPpt(){
  console.log('clicked');
  let bolded = this.state.text.split(" ");
  bolded = bolded.map(e=>{
    if(e.split("(b)").length > 1){
      return e.split("(b)")[1];
    }
    else {
      return e;
    }
  })
  let words = bolded.join(" ");
  let selected = this.state.images.filter(e=>
  (e.selected === true))
  let jason = qs.stringify({
        "title": this.state.title,
        "text": words,
        "imageList": selected
    })
    console.log(jason);
    axios({
      method: 'post',
      url: 'api/sampleData/CreatePpt/',
      data:{
        jason,
      },
      headers: {
        'Content-Type': 'application/json',
      },
    }).then((response) =>
  {
    console.log(response);
  },
(error) => {
  console.log(error)
});
  // fetch('api/sampleData/CreatePpt'),{
  //   method: 'GET',
  //   headers:{
  //     'Content-Type': 'application/json'
  //   },
  //   credentials: "same-origin",
  //   body: jason
  //   }
}

selectImage(uri){
  for(let i = 0; i< this.state.images.length; i++){
    if(this.state.images[i].imageURI === uri){
      this.state.images[i].selected = !this.state.images[i].selected;
    }
    this.setState({images: this.state.images});
  }
console.log(this.state);
}


  render () {
    return (
      <div>
        <button className='link' onClick={()=>this.createPpt()}> Create Powerpoint</button>
        <form>
          <label>Title:
            <input type = 'text' id ='title' name = 'title' value = {this.state.title} onChange={this.handleChange}/>
          </label>
          <br></br>
          <label>Text: (Use (b) before a word to bold it)
            <textarea type = 'textarea' rows = '5'
              cols = '65' id ='text' name = 'text' value = {this.state.text} onChange={this.handleTextChange}/>
          </label>
          <h3 className = 'link' onClick={()=>this.getImages()}>Find Images</h3>

        </form>
          {this.state.images.map( (e,index)=>
                  <img className ={e.selected ? 'highlight' : 'dont'} id = {index} src = {e.imageURI} onClick={()=>this.selectImage(e.imageURI)}/>
          )}

      </div>

    );
  }
}

