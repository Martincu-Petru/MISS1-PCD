var express = require("express");
var app = express();
var Book = require('../node_server/book');

var cors = require('cors');
const fs = require("fs");
const bodyParser = require('body-parser'); 

app.use(cors())
app.use(bodyParser.json()); 

app.get("/api/book", (req, res, next) => {
  fs.readdir("../node_server",  (err, files) => {
    res.send(files);
  });
});

app.get("/api/book/:id", (req, res, next) => {
  res.send('NOT IMPLEMENTED: Book DETAIL: ' + req.params.id);
});

app.post("/api/book", (req, res, next) => {

  var data = {
    bookName: req.body.bookName != null ? req.body.bookName : "null",
    author: req.body.author != null ? req.body.author : "null",
    //isbn: req.body.isbn != null ? req.body.isbn : "null",
    isbn: Math.floor(Math.random() * Math.floor(10000000000)),
    category: req.body.category != null ? req.body.category : "null",
    publishingHouse: req.body.publishingHouse != null ? req.body.publishingHouse : "null",
    text: req.body.text != null ? req.body.text : "null"
  }

  console.log(data);

  fs.appendFile("./books/" + data.isbn + ".json", JSON.stringify(data), function (err) {
    if (err) throw err;
    console.log('Book was saved!');
  });

  console.log(JSON.stringify(data));
});

app.delete("/api/book", (req, res, next) => {
  res.send('NOT IMPLEMENTED: Book DELETE');
});

app.listen(3000, () => {
 console.log("Server running on port 3000");
});