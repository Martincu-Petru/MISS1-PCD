import ContainerController from '../../cardinal/controllers/base-controllers/ContainerController.js';

var model = {
    books: []
}

var bindObject = {
    bookName: {
        label: "Book Title",
        name: "bookName",
        value: ''
    },
    author: {
        label: "Author",
        name: "author",
        value: ''
    },
    isbn: {
        label: "ISBN",
        name: "isbn",
        readonly: true,
        value: ''
    },
    pageNumber: {
        label: "Number of pages",
        name: "pageNumber",
        value: ''
    },
    category: {
        label: "Category",
        name: "category",
        value: ''
    },
    fileSize: {
        label: "File Size",
        name: "fileSize",
        value: ''
    },
    publishingHouse: {
        label: "Publishing House",
        name: "publishingHouse",
        value: ''
    }
}

var booksToSet;

var bindBooks = function(arr) {
    console.log(arr);

    model = {
        books: []
    }
    
    arr.forEach(book => {
        let bookToPush = Object.assign({}, bindObject);
        bookToPush.author.value = book.author;
        bookToPush.bookName.value = book.bookName;
        bookToPush.isbn.value = book.isbn;
        bookToPush.pageNumber.value = book.pageNumber;
        bookToPush.publishingHouse.value = book.publishingHouse;
        bookToPush.fileSize.value = book.fileSize;
        bookToPush.category.value = book.category;
        model.books.push(bookToPush);
    });

    booksToSet = JSON.parse(JSON.stringify(model));
}

function getBooks(callback) {
    var xhttp = new XMLHttpRequest();

    xhttp.open("GET", "http://localhost:3000/api/books");
    xhttp.setRequestHeader("Content-Type", "application/json");
    xhttp.send();

    xhttp.onreadystatechange = function() {
        if (this.readyState === 4 && this.status == 200) {
            var arr = JSON.parse(this.responseText);
            callback.apply(this, [arr]);
        }
    }
}

function deleteBookRequest(isbn) {
    var xhttp = new XMLHttpRequest();
    var url = `http://localhost:3000/api/book/${isbn}`;

    xhttp.open("DELETE", url);
    xhttp.onreadystatechange = function() {
        if (this.readyState === 4 && this.status == 200) {
            console.log(this.responseText);
        }
    }
    xhttp.send(null);
}

function updateBookRequest(isbn, bookToUpdate) {
    var xhttp = new XMLHttpRequest();
    var url = `http://localhost:3000/api/book/${isbn}`;
    var jsonObj = JSON.stringify(bookToUpdate);

    xhttp.open("PUT", url);
    xhttp.setRequestHeader('Content-type','application/json');
    xhttp.onreadystatechange = function() {
        if (this.readyState === 4 && this.status == 200) {
            console.log(this.responseText);
        }
    }
    xhttp.send(jsonObj);
}


export default class NewBookController extends ContainerController {
    constructor(element) {
        super(element);

        getBooks(bindBooks);

        this.model = this.setModel(booksToSet);

        let deleteBook = (event) => {
            var isbn = event.data;
            deleteBookRequest(isbn);

            var index = this.model.books.findIndex(x => x.isbn.value === isbn);
            if (index !== -1) {
                this.model.books.splice(index, 1);
            }
        }

        let editBook = (event) => {
            var isbn = event.data;

            var index = this.model.books.findIndex(x => x.isbn.value === isbn);
            if (index !== -1) {
                this.model.books.splice(index, 1);
            }

            let bookToUpdate = {
                bookName : this.model.books[index].bookName.value,
                author : this.model.books[index].author.value,
                isbn : this.model.books[index].isbn.value,
                pageNumber : this.model.books[index].pageNumber.value,
                category : this.model.books[index].category.value,
                fileSize : this.model.books[index].fileSize.value,
                publishingHouse : this.model.books[index].publishingHouse.value,
            }

            updateBookRequest(isbn, bookToUpdate);
        }

        this.on("deleteBook", deleteBook);
        this.on("updateBook", editBook);
    }
}