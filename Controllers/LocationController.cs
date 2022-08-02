using LocationApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LocationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly LocationsContext _context;

        string SuccessMessage = "The function works successfully.";
        string ErrorMessage = "No relevant record found.";
        string RegisteredLocation = "The location has already registered.";

        public LocationController(LocationsContext context)
        {
            _context = context;
        }

        //----------------GetAll----------------//

        //[HttpGet]
        //public List<Location> GetAll()
        //{
        //    return _context.Locations.ToList();
        //}

        [HttpGet("getAll")]
        public Response GetAll()
        {
            Response response = new Response();

            var linqLocation = from location in _context.Locations select location;

            List<Location> locations = new List<Location>();
            foreach (Location location in linqLocation)
                locations.Add(location);

            if (linqLocation.Any())
            {
                response.status = 1;
                response.message = SuccessMessage;
                response.result = linqLocation; //IEnumerable<Location>
                return response;
            }
            else
            {
                response.status = 0;
                response.message = ErrorMessage;
                return response;
            }
        }

        //----------------GetById----------------//

        //[HttpGet("_id")]
        //public Response GetById(int _id)
        //{
        //    Response response = new Response();
        //    var _result = _context.Locations.Find(_id);
        //    if (_result == null)
        //    {
        //        response.status = 0;
        //        response.message = ErrorMessage;
        //        return response;
        //    }

        //    else
        //    {
        //        response.status = 1;
        //        response.message = SuccessMessage;
        //        response.result = _result;
        //        return response;
        //    }
        //}

        [HttpGet("_id")]
        public Response GetById(int _id)
        {
            Response response = new Response();

            var linqLocation = from location in _context.Locations where location.Id == _id select location;

            if (!linqLocation.Any())
            {
                response.status = 0;
                response.message = ErrorMessage;
                return response;
            }

            else
            {
                response.status = 1;
                response.message = SuccessMessage;
                response.result = linqLocation; //IEnumerable<Location>
                return response;
            }
        }

        //----------------GetCoordinateByName----------------//

        [HttpGet("{_locName} name")]
        public Response getCoordinateByName(string _locName)
        {
            Response response = new Response();

            var xGetByName = from location in _context.Locations
                             where location.Locationname == _locName
                             select location.X;

            var yGetByName = from location in _context.Locations
                             where location.Locationname == _locName
                             select location.Y;


            List<double> coordinates = new List<double>();
            foreach (double x in xGetByName)
                coordinates.Add(x);
            
            foreach (double y in yGetByName)
                coordinates.Add(y);
            

            if (xGetByName.Any() && yGetByName.Any())
            {
                response.status = 1;
                response.message = SuccessMessage;
                response.result = coordinates;
                return response;
            }

            else
            {
                response.status = 0;
                response.message = ErrorMessage;
                return response;
            }
        }

        //----------------GetNameByCoordinate----------------//

        [HttpGet("{_x , _y} coordinate")]
        public Response getNameByCoordinate(double _x, double _y)
        {
            Response response = new Response();
            var nameGetByCoordinate = from location in _context.Locations
                                      where location.X == _x && location.Y == _y
                                      select location.Locationname;

            if (nameGetByCoordinate.Any())
            {
                response.status = 1;
                response.message = SuccessMessage;
                response.result = nameGetByCoordinate;
                return response;
            }

            else
            {
                response.status = 0;
                response.message = ErrorMessage;
                return response;
            }
        }


        //----------------getLocationByCoordinate----------------//

        [HttpGet("coordinate")]
        public Response getLocationByCoordinate(double _x, double _y)
        {
            Response response = new Response();
            var nameGetByCoordinate = from location in _context.Locations
                                      where location.X == _x && location.Y == _y
                                      select location;

            if (nameGetByCoordinate.Any())
            {
                response.status = 1;
                response.message = SuccessMessage;
                response.result = nameGetByCoordinate;
                return response;
            }

            else
            {
                response.status = 0;
                response.message = ErrorMessage;
                return response;
            }
        }

        //----------------GetByCoordinate----------------//

        [HttpGet("[Action]{_x , _y}")]
        public Response GetByCoordinate(double _x, double _y)
        {
            Response response = new Response();

            var queryGetByCoordinate = from location in _context.Locations
                                       where location.X == _x && location.Y == _y
                                       select location;

            if (queryGetByCoordinate.Any())
            {
                response.status = 1;
                response.message = SuccessMessage;
                response.result = queryGetByCoordinate;
                return response;
            }

            else
            {
                response.status = 0;
                response.message = ErrorMessage;
                return response;
            }
        }

        //----------------GetByName----------------//

        [HttpGet("{_locName}")]
        public Response GetByName(string _locName)
        {
            var response = new Response();

            var queryGetByName = from location in _context.Locations
                                 where location.Locationname == _locName
                                 select location;

            if (queryGetByName.Any())
            {

                response.result = queryGetByName;
                response.status = 1;
                response.message = SuccessMessage;
                return response;
            }

            else
            {
                response.status = 0;
                response.message = ErrorMessage;
                return response;
            }
        }


        //----------------DeleteById----------------//

        //[HttpDelete("_id")]
        //public Response DeleteById(int _id)
        //{
        //    Response response = new Response();
        //    if (_context.Locations.Find(_id) == null)
        //    {
        //        response.status = 0;
        //        response.message = ErrorMessage;
        //        return response;
        //    }

        //    else
        //    {
        //        var _result = _context.Locations.Find(_id);
        //        _context.Locations.Remove(_result);
        //        _context.SaveChanges();
        //        response.status = 1;
        //        response.message = SuccessMessage;
        //        response.result = _context.Locations.ToList();
        //        return response;
        //    }
        //}

        [HttpDelete("{_id}")]
        public Response DeleteById(int _id)
        {
            Response response = new Response();

            var linQLocation = from location in _context.Locations where location.Id == _id select location;

            if (!linQLocation.Any())
            {
                response.status = 0;
                response.message = ErrorMessage;
                return response;
            }

            else
            {
                _context.Locations.Remove(linQLocation.First());
                _context.SaveChanges();
                response.status = 1;
                response.message = SuccessMessage;
                return response;
            }
        }


        //----------------checkAndAdd----------------//

        //[HttpPost]
        //public Response AddLocation(Location _location)
        //{
        //    _context.Locations.Add(_location);
        //    _context.SaveChanges();
        //    Response response = new Response();

        //    response.status = 1;
        //    response.message = SuccessMessage;
        //    response.result = _context.Locations.Find(_location.Id);
        //    return response;
        //}


        //        •	Kayıt varsa aynısı eklemesin (isim)

        //[HttpPost("checkNameAndAdd")]
        //public Response checkNameAndAdd(Location _location)
        //{
        //    Response response = new Response();
        //    var queryGetByName = from location in _context.Locations
        //                         where location.Locationname == _location.Locationname
        //                         select location.Locationname;

        //    if (!queryGetByName.Any())
        //    {
        //        _context.Locations.Add(_location);
        //        _context.SaveChanges();

        //        response.status = 1;
        //        response.message = SuccessMessage;
        //        response.result = _context.Locations.Find(_location.Id);
        //        return response;
        //    }

        //    else
        //    {
        //        response.status = 0;
        //        response.message = "The name has already registered.";
        //        response.result = _context.Locations.Find(_location.Id);
        //        return response;
        //    }
        //}

        //        •	Kayıt varsa aynısı eklemesin (cordinate)

        //[HttpPost("checkCoordinateAndAdd")]
        //public Response checkCoordinateAndAdd(Location _location)
        //{
        //    Response response = new Response();
        //    var queryGetByCoordinate =  from location in _context.Locations
        //                                where location.X == _location.X && location.Y ==_location.Y
        //                                select location.Locationname;

        //    if (!queryGetByCoordinate.Any())
        //    {
        //        _context.Locations.Add(_location);
        //        _context.SaveChanges();

        //        response.status = 1;
        //        response.message = SuccessMessage;
        //        response.result = _context.Locations.Find(_location.Id);
        //        return response;
        //    }

        //    else
        //    {
        //        response.status = 0;
        //        response.message = "The location has already registered.";
        //        response.result = _context.Locations.Find(_location.Id);
        //        return response;
        //    }
        //}

        [HttpPost("checkAndAdd")]
        public Response checkAndAdd(Location _location)
        {
            Response response = new Response();

            var linQLocation = from location in _context.Locations 
                               where location.X == _location.X && location.Y == _location.Y 
                               && location.Locationname == _location.Locationname 
                               && location.Locationname.Length > 3
                               select location;
            if(linQLocation.Any())
            {
                response.status = 0;
                response.message = RegisteredLocation;
                return response;
            }
            else
            {
                _context.Locations.Add(_location);
                _context.SaveChanges();
                response.status = 1;
                response.message = SuccessMessage;
                response.result = _context.Locations.Find(_location.Id);
                return response;
            }
        }

        //----------------UpdateById----------------//

        //[HttpPut]
        //public Response UpdateById(Location _location)
        //{
        //    var _oldLocation = _context.Locations.Find(_location.Id);
        //    var response = new Response();

        //    if (_oldLocation == null)
        //    {
        //        response.status = 0;
        //        response.message = ErrorMessage;
        //        return response;
        //    }

        //    else
        //    {
        //        if (!string.IsNullOrEmpty(_location.Locationname))
        //        {
        //            _oldLocation.Locationname = _location.Locationname;
        //        }

        //        if (_location.X != -1)
        //        {
        //            _oldLocation.X = _location.X;
        //        }

        //        if (_location.Y != -1)
        //        {
        //            _oldLocation.Y = _location.Y;
        //        }

        //        _context.Locations.Update(_oldLocation);
        //        _context.SaveChanges();

        //        response.status = 1;
        //        response.message = SuccessMessage;
        //        response.result = _context.Locations.Take(_location.Id);
        //        return response;
        //    }
        //}

        [HttpPut("_location")]
        public Response UpdateById(Location _location)
        {
            Response response = new Response();

            var _oldLocation = from location in _context.Locations
                               where location.Id == _location.Id
                               select location;

            
            if (!_oldLocation.Any())
            {
                response.status = 0;
                response.message = ErrorMessage;
                return response;
            }

            else
            {
                if (!string.IsNullOrEmpty(_location.Locationname))
                {
                    _oldLocation.First().Locationname = _location.Locationname;
                }

                if (_location.X != -1)
                {
                    _oldLocation.First().X = _location.X;
                }

                if (_location.Y != -1)
                {
                    _oldLocation.First().Y = _location.Y;
                }

                _context.Locations.Update(_oldLocation.First());
                _context.SaveChanges();

                response.status = 1;
                response.message = SuccessMessage;
                response.result = _oldLocation.First();
                return response;
            }
        }
    }
}

