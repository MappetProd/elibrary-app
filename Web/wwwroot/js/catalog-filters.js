$(".multiple-select").chosen().change(function (e, params) {
    var _genres = $("#genre-filter").val();
    var _authors = $("#author-filter").val();
    var _publishers = $("#publisher-filter").val();

    var params = new URLSearchParams();
    var querystring = "GetBooksWithFilters?"
    if (_genres !== undefined && _genres !== null) {
        //params.append("genres", _genres)
        querystring += new URLSearchParams({ genres: _genres }).toString()
        querystring += "&"
    }
    if (_authors !== undefined && _authors !== null) {
        //params.append("authors", _authors)
        querystring += new URLSearchParams({ authors: _authors }).toString()
        querystring += "&"
    }
    if (_publishers !== undefined && _publishers !== null) {
        //params.append("publoshers", _publishers)
        querystring += new URLSearchParams({ publishers: _publishers }).toString()
        querystring += "&"
    }
    querystring.substring(0, querystring.length - 2)
    $('#catalog').load(querystring, function () {
        initCartActionButtons()
    })

    //fetch(querystring)
});