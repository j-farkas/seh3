import React, { Component } from 'react';
import "./styles.css";
import axios from 'axios';

export class Home extends Component {
  static displayName = Home.name;

  constructor (props) {
    super(props);
    this.state = { images: [], title: "", text: "", ppt: false};
    this.getImages = this.getImages.bind(this);
    this.createPpt = this.createPpt.bind(this);
    this.selectImage = this.selectImage.bind(this);
    this.handleChange = this.handleChange.bind(this);
    this.handleTextChange = this.handleTextChange.bind(this);
  }

handleChange(event){
  this.setState({title: event.target.value})

}

handleTextChange(event){
  this.setState({text: event.target.value})
}

getImages(){
  let bolded = this.state.text.split(" ");
  bolded = bolded.map(e=>{
    if(e.split("<b>").length > 0){
      return e.split("<b>")[1].split("</b>")[0];
    }
  })
  let words = this.state.title + bolded.join(" ");
  fetch('images/'+words)
    .then(response => response.json())
    .then(data => {
      this.setState({ images: data});

    });
}

createPpt(){
  this.setState({ppt:true})
  let bolded = this.state.text.split(" ");
  bolded = bolded.map(e=>{
    if(e.split("<b>").length > 1){
      return e.split("<b>")[1].split("</b>")[0];
    }
    else {
      return e;
    }
  })
  let words = bolded.join(" ");
  this.setState({text: words});
  let selected = this.state.images.filter(e=>
  (e.selected === true))
  let allUris = "";
  selected.forEach(e=>allUris+=e.imageURI+"`");
  let jason = JSON.stringify({
        "title": this.state.title,
        "text": words,
        "imageList": selected
    })
    axios({
      method: 'post',
      url: 'images',
      data:{
       title: this.state.title,
       text: words,
       imageList: selected
      },
      headers: {
        'Content-Type': 'application/json',
        title: this.state.title,
       text: words,
       imageList: allUris
      },
    }).then((response) =>
  {
    console.log(response);
  },
(error) => {
  console.log(error)
});
}

selectImage(uri){
  for(let i = 0; i< this.state.images.length; i++){
    if(this.state.images[i].imageURI === uri){
      this.state.images[i].selected = !this.state.images[i].selected;
    }
    this.setState({images: this.state.images});
  }
}


  render () {
    if(this.state.ppt === false)
    {

    return (
      <div>
        <button className='link' onClick={()=>this.createPpt()}> Create Powerpoint</button>
        <form>
          <label>Title:
            <input type = 'text' id ='title' name = 'title' value = {this.state.title} onChange={this.handleChange}/>
          </label>
          <br></br>
          <label>Text: (Use &lt;b&gt;around&lt;/b&gt; a word to emphasize it for search purposes)
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
          }else{
            return(
              <div>
                <h1>{this.state.title}</h1>
                <p>{this.state.text}</p>
                {this.state.images.map( (e,index)=>
                  <img className ={e.selected ? 'dont' : 'hide'} id = {index} src = {e.imageURI} />
          )}
              </div>

            );
          }
  }
}

