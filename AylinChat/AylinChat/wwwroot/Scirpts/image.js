
window.changeBackground = function () {
    var imageFolderPath = "/Image";
    var imageFiles = [];

    function getImageFiles() {
        var xhr = new XMLHttpRequest();
        xhr.open('GET', imageFolderPath, true);
        xhr.responseType = 'document';

        xhr.onload = function () {
            var files = xhr.response.getElementsByTagName('a');
            for (var i = 0; i < files.length; i++) {
                var fileName = files[i].href.split('/').pop();
                if (fileName.match(/\.(jpeg|jpg|gif|png)$/) != null) {
                    imageFiles.push(fileName);
                }
            }
            var randomIndex = Math.floor(Math.random() * imageFiles.length);
            var selectedImage = imageFiles[randomIndex];
            var imageURL = imageFolderPath + selectedImage;
            document.getElementById("background").style.backgroundImage = "url('" + imageURL + "')";
        };

        xhr.send();
    }

    getImageFiles();
}
