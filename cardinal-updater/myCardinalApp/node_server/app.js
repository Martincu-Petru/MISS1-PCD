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
  console.log(req.body);
});

app.delete("/api/book", (req, res, next) => {
  res.send('NOT IMPLEMENTED: Book DELETE');
});

app.listen(3000, () => {
 console.log("Server running on port 3000");
});