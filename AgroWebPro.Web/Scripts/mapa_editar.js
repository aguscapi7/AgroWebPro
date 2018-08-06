
    var osmUrl = 'http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
        osmAttrib = '&copy; <a href="http://openstreetmap.org/copyright">OpenStreetMap</a> contributors',
        osm = L.tileLayer(osmUrl, { maxZoom: 29, attribution: osmAttrib });
    

    L.tileLayer("http://www.google.cn/maps/vt?lyrs=s@189&gl=cn&x={x}&y={y}&z={z}", {
        attribution: ''
    }).addTo(map);
    
    new LeafletToolbar.DrawToolbar({
        position: 'topleft'
    }).addTo(map);

    new LeafletToolbar.EditToolbar.Control({
        position: 'topleft'
    }).addTo(map, drawnItems);
    var searchControl = new L.esri.Controls.Geosearch().addTo(map,drawnItems);

    var results = new L.LayerGroup().addTo(map);

    searchControl.on('results', function (data) {
        results.clearLayers();
        for (var i = data.results.length - 1; i >= 0; i--) {
            results.addLayer(L.marker(data.results[i].latlng));
        }
    });

    setTimeout(function () { $('.pointer').fadeOut('slow'); }, 3400);

    $(".leaflet-draw-draw-polyline").addClass("hidden");
    $(".leaflet-draw-draw-marker").addClass("hidden");
    $(".leaflet-draw-draw-rectangle").addClass("hidden");
    $(".leaflet-draw-draw-circle").addClass("hidden");
    
    map.on('draw:created', function (evt) {
        var type = evt.layerType,
            layer = evt.layer;

        drawnItems.addLayer(layer);
    });

    L.control.layers({
        'osm': osm.addTo(map),
        "google": L.tileLayer('http://www.google.cn/maps/vt?lyrs=s@189&gl=cn&x={x}&y={y}&z={z}', {
            maxZoom: 29, attribution: 'google'
        })
    }, { 'drawlayer': drawnItems }, { position: 'topright', collapsed: false }).addTo(map);


    $(".leaflet-control-layers-expanded").addClass("hidden");

function CambiarTipoMapa(checkTipoMapa) {
    var tiposMapas = $(".leaflet-control-layers-selector");
    if ($(checkTipoMapa).is(":checked")) {
        $(tiposMapas[1]).click();
    }
    else {
        $(tiposMapas[0]).click();

    }
}

function DeserealizarXML(contenido) {
    clearMap();
    var xmlDoc = $.parseXML(contenido),
        $xml = $(xmlDoc),
        $coordenadas = $xml.find("coordenada");
    var coor = [];
    if ($coordenadas.length > 0) {
        map.setView([$($coordenadas[0].childNodes[0]).html(), $($coordenadas[0].childNodes[1]).html()], 18);
        for (var i = 0; i < $coordenadas.length; i++) {
            coor.push([$($coordenadas[i].childNodes[0]).html(), $($coordenadas[i].childNodes[1]).html()]);
        }
        var polygon = L.polygon(coor);
        polygon.addTo(L.featureGroup().addTo(map));
        drawnItems.addLayer(polygon);
    }

}

function clearMap() {
    for (i in map._layers) {
        if (map._layers[i]._path != undefined) {
            try {
                map.removeLayer(map._layers[i]);
            }
            catch (e) {
                console.log("problem with " + e + map._layers[i]);
            }
        }
    }
}