// Variables para renderizado de direcciones, autocompletado, mapa de google y marcador
var directionsRenderer;
var autoComplete;
var googleMap;
var marker;
var panelSumario = document.getElementById('panel-direcciones');

//$(document).ready(function () {

//$('#btnAgregarDireccion').click(function () {
//    $('#txtUbicacion2').val($('#txtUbicacionTD').text());
//});

//$("#tablaDireccion tbody tr").click(function () {
$('#btnRutaSugerida').click(function () {
    //var txtDireccion = $(this).find("td:eq(2)").text();
    //alert(total);
    //$('#txtUbicacion2').val(txtDireccion);
    //});

    //$('#btnAgregarUbicacion').click(function () {
    //        if ($('#txtUbicacion').val().trim() == "") {
    //            alert('Por favor, ingrese la ubicaci\u00F3n.');
    //            return;
    //        }

    // Remove previous marker, if any
    //Eliminar marcador anterior, si lo hubiera
    //if (marker) {
    //    marker.setMap(null);
    //}

    // Add location to source as well as destination drop down lists.
    // Agrega las ubicaciones en la lista de origen así como también en la de destino.
    //$('#listaDestino').append('<option value="' + $('#txtUbicacion').val() + '">' + $('#txtUbicacion').val() + '</option>');
    //$('#listaOrigen').append('<option value="' + $('#txtUbicacion2').val() + '">' + $('#txtUbicacion2').val() + '</option>');

    // Clear location text box and focus it.
    // Limpia el campo de texto de ubicaciones y se focaliza el cursor
    //$('#txtUbicacion2').val('');
    //$('#txtUbicacion2').focus();

    // Display success message.
    // Muestra el mensaje de finalización
    //$('#mensajeAgregado').html("\u00A1Agregado a la ruta!");
    // Hide success message after 5 seconds.
    // Oculta el mensaje después de 5 segundos
    //setTimeout(function () {
    //    $('#mensajeAgregado').html("");
    //}, 5000);
    //});


    //$('#checkDestinoIgualAOrigen').change(function () {
    // Si el checkbox está seleccionado, deshabilita la segunda lista y
    // la predetermina con la misma ubicación de origen
    //    $('#listaDestino').attr('disabled', $('#checkDestinoIgualAOrigen').is(':checked'));
    //    if ($('#checkDestinoIgualAOrigen').is(':checked')) {
    //        $('#listaDestino').val($('#listaOrigen').val());
    //    }
    //});

    //$('#listaOrigen').change(function () {
    // Si se cambia el origen, el destino tambien. Todo esto si el checkbox está activo.
    //    if ($('#checkDestinoIgualAOrigen').is(':checked')) {
    //        $('#listaDestino').val($('#listaOrigen').val());
    //    }
    //});

    //$('#btnMostrarDirecciones').click(function () {
    // Si el origen no está seleccionado, lanza una advertencia
    //if ($('#listaOrigen').val() == '') {
    //    alert('Por favor, ingrese el origen.');
    //    $('#listaOrigen').focus();
    //    return;
    //}

    // Si el destino no está seleccionado, lanza una advertencia
    //if ($('#listaDestino').val() == '') {
    //    alert('Por favor, ingrese el destino.');
    //    $('#listaDestino').focus();
    //    return;
    //}

    // Si el modo de viaje no está seleccionado, lanza una advertencia
    //if ($('#listaModoViaje').val() == '') {
    //    alert('Por favor, ingrese el modo de viaje.');
    //    $('#listaModoViaje').focus();
    //    return;
    //}

    // Clear previous directions, if any.
    // Limpia las direcciones previas, si lo hubiera
    if (directionsRenderer) {
        directionsRenderer.setMap(null);
    }
    $('#panel').html('');

    var directionsService = new google.maps.DirectionsService;

    // Se crea un nuevo objeto para renderizar el mapa
    directionsRenderer = new google.maps.DirectionsRenderer({
        map: googleMap,
        panel: document.getElementById("panel"),
        draggable: true
    });

    // Add way points to array.
    // Agrega los puntos de ruta en un arreglo
    //var wayPoints = [];
    //$('#listaOrigen > option').each(function () {
    //    if ($(this).val() != '' && $(this).val() != $('#listaOrigen').val() /*&& $(this).val() != $('#listaDestino').val()*/) {
    //        var wayPoint = {
    //            location: $(this).val(),
    //            stopover: true
    //        };
    //        //var punto1 = { location: 'Freire 240, Constitución, Chile', stopover: true }
    //        //var punto2 = { location: 'Infante 120, Constitución, Chile', stopover: true }
    //        wayPoints.push(wayPoint);
    //    }
    //});

    var waypts = [];
    var checkboxArray = document.getElementById('waypoints');
    for (var i = 0; i < checkboxArray.length; i++) {
        if (checkboxArray.options[i].selected) {
            waypts.push({
                location: checkboxArray[i].value,
                stopover: true
            });
        }
    }

    // Create directions request.
    // Crea la solicitud de las direcciones
    var directionsRequest = {
        origin: $('#txtUbicacion').val(),
        destination: $('#txtUbicacion').val(),
        travelMode: 'DRIVING', /*google.maps.TravelMode[$('#listaModoViaje').val()],*/
        waypoints: waypts,
        optimizeWaypoints: true /*$('#checkRutaOptima').is(':checked')*/
    };

    // Send request to the directions service.
    // Envía las solicitudes hacia el servicio de direcciones
    directionsService.route(directionsRequest, function (response, status) {
        //Si encuentra los resultados, los muestra. Sino, indica que no se encontraron.
        if (status == google.maps.DirectionsStatus.OK) {
            directionsRenderer.setDirections(response);

            var totalDistancia = 0;
            var totalDuracion = 0;
            var waypointsDuracion = waypts.length * 0.2;
            var legs = response.routes[0].legs;
            for (var i = 0; i < legs.length; ++i) {
                totalDistancia += legs[i].distance.value;
                totalDuracion += legs[i].duration.value;
            }
            //alert('Paradas: '+wayPoints.length);
            $('#totalDistancia').text((totalDistancia / 1000).toFixed(1));
            $('#totalDuracion').text(((totalDuracion / 3600) + waypointsDuracion).toFixed(1));
            $('#combustibleAprox').text(((totalDistancia / 1000) / 15).toFixed(1));
            $('#costoAprox').text((((totalDistancia / 1000) / 15) * 653).toFixed(1));
        } else {
            alert('No encontramos los resultados de su búsqueda.');
        }
    });
});
//});

function initializeMap() {
    var directionsService = new google.maps.DirectionsService;
    var directionsDisplay = new google.maps.DirectionsRenderer;
    // Specify auto complete text box.
    // Carga el autocompletado de ubicaciones en el textbox
    autoComplete = new google.maps.places.Autocomplete(document.getElementById("txtUbicacion2"));

    // Add auto complete listener.
    // Detecta y agrega el autocompletado
    autoComplete.addListener('place_changed', function () {
        // Display the place on the map.
        // Muestra el lugar en el mapa
        var place = autoComplete.getPlace();
        googleMap.setCenter(place.geometry.location);
        googleMap.setZoom(15);

        // Remove previous marker, if any.
        // Remueve marcador previo, si lo hubiera
        if (marker) {
            marker.setMap(null);
        }

        // Add a new marker.
        // Agrega un nuevo marcador
        marker = new google.maps.Marker({
            position: place.geometry.location,
            map: googleMap,
            title: place.formatted_address
        });
    });

    // Botón para limpiar el formulario y el mapa
    document.getElementById('btnLimpiarRuta').addEventListener('click', function () {
        if (confirm('\u00BFEst\u00E1 seguro de borrar la ruta?')) {
            // Clear directions, if any.
            // Limpia las direcciones, si las hubiera
            if (directionsRenderer) {
                directionsRenderer.setMap(null);
            }

            // Clear the form.
            // Limpia el formulario
            $('#panel').html('');
            $('#frmUbicacion').get(0).reset();
            $('#listaOrigen').html('<option value="">-- Origen --</option>');
            // $('#listaDestino').html('<option value="">-- Destino --</option>');

            panelSumario.innerHTML = '';
            // Remove previous marker, if any.
            // Remueve marcador previo, si lo hubiera
            if (marker) {
                marker.setMap(null);
            }
        }
    });

    // Variable que indica el punto central desde donde se debe iniciar el mapa
    var latLng = new google.maps.LatLng(-35.334538, -72.407888);

    // Se determina la variable anterior y se configura el zoom del mapa
    var mapaOpciones = {
        center: latLng,
        zoom: 15
    };

    //Se crea un nuevo objeto para cargarlo en el objeto div
    googleMap = new google.maps.Map(document.getElementById("googleMap"), mapaOpciones);

    google.maps.event.addListener(googleMap, 'click', function (e) {
        // Remove previous marker, if any.
        // Remueve marcador previo, si lo hubiera
        if (marker) {
            marker.setMap(null);
        }

        // Get address by lat/lng.
        // Obtiene la dirección por latitud y longitud
        var geocoder = new google.maps.Geocoder();
        geocoder.geocode({ 'latLng': e.latLng }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                if (results[0]) {
                    var direccion = results[0].formatted_address;
                    // Una vez obtenidos los datos, se crea un nuevo objeto para el marcador
                    marker = new google.maps.Marker({
                        position: e.latLng,
                        map: googleMap,
                        title: direccion
                    });

                    $('#txtUbicacion').val(direccion);
                }
            }
        });
    });
    document.getElementById('btnRutaSugerida').addEventListener('click', function () {
        calculateAndDisplayRoute(directionsService, directionsDisplay);
    });
}

// Add window load event.
// Carga el evento para inicializar el mapa
google.maps.event.addDomListener(window, "load", initializeMap);

function calculateAndDisplayRoute(directionsService, directionsDisplay) {
    var waypts = [];
    var checkboxArray = document.getElementById('waypoints');
    for (var i = 0; i < checkboxArray.length; i++) {
        if (checkboxArray.options[i].selected) {
            waypts.push({
                location: checkboxArray[i].value,
                stopover: true
            });
        }
    }

    //$('#listaOrigen > option').each(function () {
    //    if ($(this).val() != '' && $(this).val() != $('#listaOrigen').val() /*&& $(this).val() != $('#listaDestino').val()*/) {
    //        var waypt = {
    //            location: $(this).val(),
    //            stopover: true
    //        };
    //        //var punto1 = { location: 'Freire 240, Constitución, Chile', stopover: true }
    //        //var punto2 = { location: 'Infante 120, Constitución, Chile', stopover: true }
    //        waypts.push(waypt);
    //    }
    //});

    directionsService.route({
        origin: document.getElementById('txtUbicacion').value,
        destination: document.getElementById('txtUbicacion').value,
        waypoints: waypts,
        optimizeWaypoints: true,
        travelMode: 'DRIVING' /*google.maps.TravelMode[$('#listaModoViaje').val()]*/
    }, function (response, status) {
        if (status === 'OK') {
            directionsDisplay.setDirections(response);
            var route = response.routes[0];
            //var panelSumario = document.getElementById('directions-panel');
            panelSumario.innerHTML = '';
            // For each route, display summary information.
            for (var i = 0; i < route.legs.length; i++) {
                var tramo = i + 1;
                panelSumario.innerHTML += '<b>Tramo: ' + tramo +
                    '</b><br>';
                panelSumario.innerHTML += route.legs[i].start_address + ' hacia ';
                panelSumario.innerHTML += route.legs[i].end_address + '<br>';
                panelSumario.innerHTML += route.legs[i].distance.text + '<br><br>';
            }
        } else {
            window.alert('La solicitud falló debido a ' + status);
        }
    });
}