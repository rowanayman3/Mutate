const button = document.querySelector(".addtocart");
const done = document.querySelector(".done");
console.log(button);
let added = false;
button.addEventListener('click',()=>{
  if(added){
    done.style.transform = "translate(-110%) skew(-40deg)";
    added = false;
  }
  else{
    done.style.transform = "translate(0px)";
    added = true;
  }
    
});


jQuery(document).ready(($) => {
    $('.quantity').on('click', '.plus', function(e) {
        let $input = $(this).prev('input.qty');
        let val = parseInt($input.val());
        $input.val( val+1 ).change();
    });

    $('.quantity').on('click', '.minus', 
        function(e) {
        let $input = $(this).next('input.qty');
        var val = parseInt($input.val());
        if (val > 0) {
            $input.val( val-1 ).change();
        } 
    });
});