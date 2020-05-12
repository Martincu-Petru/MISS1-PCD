var express = require("express");
var app = express();
var Book = require('../node_server/book');

var cors = require('cors');
const fs = require("fs");
const bodyParser = require('body-parser'); 

app.use(cors())
app.use(bodyParser.json()); 

app.get("/api/books", (req, res, next) => {
  var books = [];
  fs.readdirSync("./books/").forEach(file => {
    var contents = fs.readFileSync("./books/" + file, 'utf8');
    books.push(JSON.parse(contents));
    }
  )
  
  res.setHeader('Content-Type', 'application/json');
  res.send(JSON.stringify(books));
  res.end();
});

app.get("/api/book/:isbn", (req, res, next) => {
  fs.readFile("./books/" + req.params.isbn + ".json", 'utf8', function(error, contents) {
    if (error) {
      throw error;
    } else {
      res.setHeader('Content-Type', 'application/json');
      res.send(JSON.parse(contents));
      res.end();
    }
  });
});

app.post("/api/book", (req, res, next) => {

  var data = {
    bookName: req.body.bookName,
    author: req.body.author,
    isbn: req.body.isbn,
    pageNumber: req.body.pageNumber,
    category: req.body.category,
    fileSize: req.body.fileSize,
    publishingHouse: req.body.publishingHouse,
    fileName: req.body.fileName,
    fileExtension: req.body.fileExtension
  }

  fs.writeFile("./books/" + data.isbn + ".json", JSON.stringify(data), function (error) {
    if (error) {
      throw error;
    } else {
      console.log("Book " + req.body.bookName + " was saved!");
      console.log(JSON.stringify(data));

      res.setHeader('Content-Type', 'application/json');
      res.send(JSON.stringify(data));
      res.end();
    }
  });
});

app.delete("/api/book/:isbn", (req, res, next) => {
  fs.unlink("./books/" + req.params.isbn + ".json", (error) => {
    if (error) {
      throw error;
    } else {
      res.setHeader('Content-Type', 'application/json');
      res.send(JSON.stringify({isbn: req.params.isbn}));
      res.end();
    }
  });
});

app.listen(3000, () => {
 console.log("Server running on port 3000");
});