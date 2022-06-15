$(document).ready(function () {

    $(".sweet-delete").click(function (e) {
        e.preventDefault();
        
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                let url = $(this).attr("href");
                fetch(url)
                    .then(response => {
                        if (response.ok) {
                            window.location.reload()
                        }
                        else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Oops...',
                                text: "There are problem in process!!",
                               
                            })
                           
                        }
                    })
            }
        })
    })
    
    $(".remove-img").click(function () {
        console.log("hello")
    })

   
    
})
/*let removeImg = document.querySelector(".remove-img")
removeImg.addEventListener("click", function (e) {
    console.log("hello")
})*/


/*let input = document.querySelector(".input-image")
input.addEventListener('change', function (e) {
    let img = document.querySelector("important-image")
    img.setAttribute("src", `~/upload/slider/${}`)
})*/
