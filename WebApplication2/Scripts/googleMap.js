// Variables para renderizado de direcciones, autocompletado, mapa de google y marcador
var directionsRenderer;
var autoComplete;
var googleMap;
var marker;

$(document).ready(function () {
    $('#btnAddLocation').click(function () {
        if ($('#txtLocation').val().trim() == "") {
            //alert('Por favor, ingrese la ubicaci\u00F3n.');
            $("#rutaModal").modal('show');
            $("#rutaModalTitle").html("\u00A1Advertencia!");
            $("#rutaModalContent").html("Por favor, ingrese la ubicaci\u00F3n.");
            return;
        }

        // Remove previous marker, if any
        //Eliminar marcador anterior, si lo hubiera
        if (marker) {
            marker.setMap(null);
        }

        // Add location to source as well as destination drop down lists.
        // Agrega las ubicaciones en la lista de origen así como también en la de destino.
        $('#ddlDestination').append('<option value="' + $('#txtLocation').val() + '">' + $('#txtLocation').val() + '</option>');
        $('#ddlSource').append('<option value="' + $('#txtLocation').val() + '">' + $('#txtLocation').val() + '</option>');

        // Clear location text box and focus it.
        // Limpia el campo de texto de ubicaciones y se focaliza el cursor
        $('#txtLocation').val('');
        $('#txtLocation').focus();

        // Display success message.
        // Muestra el mensaje de finalización
        $('#successMessage').show();
        
        // Hide success message after 5 seconds.
        // Oculta el mensaje después de 5 segundos
        setTimeout(function () {
            $('#successMessage').hide();
        }, 5000);
    });


    $('#chkDestinationIsSameAsSource').change(function () {
        // Si el checkbox está seleccionado, deshabilita la segunda lista y
        // la predetermina con la misma ubicación de origen
        $('#ddlDestination').attr('disabled', $('#chkDestinationIsSameAsSource').is(':checked'));
        if ($('#chkDestinationIsSameAsSource').is(':checked')) {
            $('#ddlDestination').val($('#ddlSource').val());
        }
    });

    $('#ddlSource').change(function () {
        // Si se cambia el origen, el destino tambien. Todo esto si el checkbox está activo.
        if ($('#chkDestinationIsSameAsSource').is(':checked')) {
            $('#ddlDestination').val($('#ddlSource').val());
        }
    });

    $('#btnDisplayDirections').click(function () {
        // Si el origen no está seleccionado, lanza una advertencia
        if ($('#ddlSource').val() == '') {
            //alert('Please select Source.');
            $("#rutaModal").modal('show');
            $("#rutaModalTitle").html("\u00A1Advertencia!");
            $("#rutaModalContent").html("Por favor, ingrese el origen.");
            $('#ddlSource').focus();
            return;
        }

        // Si el destino no está seleccionado, lanza una advertencia
        if ($('#ddlDestination').val() == '') {
            //alert('Please select Destination.');
            $("#rutaModal").modal('show');
            $("#rutaModalTitle").html("\u00A1Advertencia!");
            $("#rutaModalContent").html("Por favor, ingrese el destino.");
            $('#ddlDestination').focus();
            return;
        }

        // Si el modo de viaje no está seleccionado, lanza una advertencia
        if ($('#ddlTravelMode').val() == '') {
            //alert('Please select Travel Mode.');
            $("#rutaModal").modal('show');
            $("#rutaModalTitle").html("\u00A1Advertencia!");
            $("#rutaModalContent").html("Por favor, ingrese el modo de viaje.");
            $('#ddlTravelMode').focus();
            return;
        }

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
        var wayPoints = [];
        $('#ddlSource > option').each(function () {
            if ($(this).val() != '' && $(this).val() != $('#ddlSource').val() && $(this).val() != $('#ddlDestination').val()) {
                var wayPoint = {
                    location: $(this).val(),
                    stopover: true
                };
                wayPoints.push(wayPoint);
            }
        });

        // Create directions request.
        // Crea la solicitud de las direcciones
        var directionsRequest = {
            origin: $('#ddlSource').val(),
            destination: $('#ddlDestination').val(),
            travelMode: google.maps.TravelMode[$('#ddlTravelMode').val()],
            waypoints: wayPoints,
            optimizeWaypoints: $('#chkOptimizePath').is(':checked')
        };

        // Send request to the directions service.
        // Envía las solicitudes hacia el servicio de direcciones
        directionsService.route(directionsRequest, function (response, status) {
            //Si encuentra los resultados, los muestra. Sino, indica que no se encontraron.
            if (status == google.maps.DirectionsStatus.OK) {
                directionsRenderer.setDirections(response);
            } else {
                //alert('We could not find any result for your search.');
                $("#rutaModal").modal('show');
                $("#rutaModalTitle").html("\u00A1Advertencia!");
                $("#rutaModalContent").html("No encontramos los resultados de su búsqueda.");
            }
        });
    });
});

function initializeMap() {
    // Specify auto complete text box.
    // Carga el autocompletado de ubicaciones en el textbox
    autoComplete = new google.maps.places.Autocomplete(document.getElementById("txtLocation"));

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
    document.getElementById('btnClearDirections').addEventListener('click', function () {
        if (confirm('\u00BFEst\u00E1 seguro de limpiar todas las ubicaciones?')) {
            // Clear directions, if any.
            // Limpia las direcciones, si las hubiera
            if (directionsRenderer) {
                directionsRenderer.setMap(null);
            }

            // Clear the form.
            // Limpia el formulario
            $('#panel').html('');
            $('#frmLocation').get(0).reset();
            $('#ddlSource').html('<option value="">-- Origen --</option>');
            $('#ddlDestination').html('<option value="">-- Destino --</option>');

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
    var mapOptions = {
        center: latLng,
        zoom: 18
    };

    //Se crea un nuevo objeto para cargarlo en el objeto div
    googleMap = new google.maps.Map(document.getElementById("googleMap"), mapOptions);

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
                    var address = results[0].formatted_address;
                    // Una vez obtenidos los datos, se crea un nuevo objeto para el marcador
                    marker = new google.maps.Marker({
                        position: e.latLng,
                        map: googleMap,
                        title: address
                    });

                    $('#txtLocation').val(address);
                }
            }
        });
    });
}

// Add window load event.
// Carga el evento para inicializar el mapa
google.maps.event.addDomListener(window, "load", initializeMap);
