 $(function(){
 	


     // Show sideNav
     $('.button-collapse').sideNav();
    
     $(document).ready(function () {
         $('.collapsible').collapsible({
             accordion: false // A setting that changes the collapsible behavior to expandable instead of the default accordion style
         });
     });
     

   

     $('.slider').slider({ full_width: true });

     
         $('.parallax').parallax();
   

    $('.datepicker').pickadate({
    selectMonths: true, // Creates a dropdown to control month
    selectYears: 5 // Creates a dropdown of 15 years to control year
  		}); 


    $(document).ready(function() {
    $('select').material_select();
  });


 



 	});