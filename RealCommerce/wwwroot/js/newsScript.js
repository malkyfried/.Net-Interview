
function showPostDetails(linkPost, title) {
    //document.getElementById('post-title').textContent = title;
    //document.getElementById('post-body').innerHTML = body;
    //document.getElementById('post-link').innerHTML = '<a href="' + linkPost + '">' + 'Read more>>' + '</a>';
     fetchPostBody(linkPost, title);
}

function fetchPostBody(linkPost, title) {
    // Make an AJAX request to the server to fetch the body of the post
    fetch('/Home/GetPostBody?url=' + encodeURIComponent(linkPost))
        .then(response => response.text())
        .then(body => {
            // Update the post details section with the fetched body
            document.getElementById('post-title').textContent = title;
            console.log(body);
            document.getElementById('post-body').innerHTML = body;
            document.getElementById('post-link').innerHTML = '<a href="' + linkPost + '">' + 'Read more>>' + '</a>';
        })
        .catch(error => {
            console.error('Error fetching post body:', error);
        });
}
