// listen to the click event on the get-jokes button

//select the button, add the event listener

document.querySelector('.get-jokes').addEventListener('click', getJokes);

function getJokes(event)
{
	const userNumber = document.querySelector('input[type="number"]').value;
	//GET request
	fetch(`http://api.icndb.com/jokes/random/${userNumber}`)
	.then(function(response)
	{
		console.log(response);
		return response.json();
	})
	.then(function(data)
	{
		//for each joke in data.value array
		data.value.forEach(joke => 
		console.log(joke.joke));
		
	});
	console.log(userNumber);
	event.preventDefault();
}