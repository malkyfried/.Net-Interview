function showPostDetails(id, title, linkPost) {
    fetch(`/Home/GetPostBody?id=${id}`)
        .then(response => response.text()) // Convert response to text
        .then(body => {
            document.getElementById('post-title').textContent = title;
            document.getElementById('post-body').innerHTML = body; // Use the body content
            document.getElementById('post-link').innerHTML = '<a href="' + linkPost + '">' + 'Read more>>' + '</a>';
        })
        .catch(error => {
            console.error('Error fetching post body:', error);
        });
}
