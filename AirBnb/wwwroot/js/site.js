// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

mapboxgl.accessToken = 'pk.eyJ1IjoicW91eW0iLCJhIjoiY2thNm44MnV3MDhrdzJybDdyZ25taWZzdiJ9.TeswIM_AHz6PsntAVngmIA';
const bounds = [[5.159, 52.22], [4.691, 52.566]]

navigator.geolocation.getCurrentPosition(successLocation,
    errorLocation,
    { enableHighAccuracy: true }
)

function successLocation(positon) {
    console.log(positon);
    setupMap([positon.coords.longitude, positon.coords.latitude]);
}

function errorLocation() {
    setupMap([4.895168, 52.370216]);
}

function setupMap(center) {
    const map = new mapboxgl.Map({
        container: 'map',
        style: 'mapbox://styles/mapbox/outdoors-v11',
        zoom: 11,
        center: center,
        bounds: bounds
    });

    //map.addControl(new MapboxDirections({
    //    accessToken: mapboxgl.accessToken
    //}), 'top-left');

    map.addControl(new mapboxgl.NavigationControl());
    map.addControl(new mapboxgl.FullscreenControl());

    map.on('mousemove', (e) => {
        document.getElementById('info').innerHTML =
            // `e.point` is the x, y coordinates of the `mousemove` event
            // relative to the top-left corner of the map.
            JSON.stringify(e.point) +
            '<br />' +
            // `e.lngLat` is the longitude, latitude geographical position of the event.
            JSON.stringify(e.lngLat.wrap());
    });
}

