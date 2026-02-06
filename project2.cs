using System;

namespace SOLIDProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            //1
            ShapeFactory circleFactory = new CircleFactory();
            IShape circle = circleFactory.CreateShape();
            circle.Draw();
            
            ShapeFactory rectangleFactory = new RectangleFactory();
            IShape rectangle = rectangleFactory.CreateShape();
            rectangle.Draw();
            
            //2
            GeoLocation geoLocation = new GeoLocation();
            ICoordinatesService coordinatesService = new GeoLocationAdapter(geoLocation);
            var coordinates = coordinatesService.GetCoordinates();
            Console.WriteLine($"Latitude: {coordinates.Latitude}, Longitude: {coordinates.Longitude}");
            
            GeoLocation geoLocation2 = new GeoLocation("50.4501, 30.5234");
            ICoordinatesService coordinatesService2 = new GeoLocationAdapter(geoLocation2);
            var coordinates2 = coordinatesService2.GetCoordinates();
            Console.WriteLine($"Latitude: {coordinates2.Latitude}, Longitude: {coordinates2.Longitude}");
            
            Console.ReadLine();
        }
    }
    
    //1
    interface IShape
    {
        void Draw();
    }
    
    class Circle : IShape
    {
        public void Draw()
        {
            Console.WriteLine("Drawing a circle");
        }
    }
    
    class Rectangle : IShape
    {
        public void Draw()
        {
            Console.WriteLine("Drawing a rectangle");
        }
    }
    
    abstract class ShapeFactory
    {
        public abstract IShape CreateShape();
    }
    
    class CircleFactory : ShapeFactory
    {
        public override IShape CreateShape()
        {
            return new Circle();
        }
    }
    
    class RectangleFactory : ShapeFactory
    {
        public override IShape CreateShape()
        {
            return new Rectangle();
        }
    }
    
    //2
    interface ICoordinatesService
    {
        Coordinates GetCoordinates();
    }
    
    class Coordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        
        public Coordinates(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
    
    class GeoLocation
    {
        private string coordinatesString;
        
        public GeoLocation()
        {
            coordinatesString = "37.7749, -122.4194";
        }
        
        public GeoLocation(string coordinates)
        {
            coordinatesString = coordinates;
        }
        
        public string GetLocationString()
        {
            return coordinatesString;
        }
    }
    
    class GeoLocationAdapter : ICoordinatesService
    {
        private GeoLocation geoLocation;
        
        public GeoLocationAdapter(GeoLocation geoLocation)
        {
            this.geoLocation = geoLocation;
        }
        
        public Coordinates GetCoordinates()
        {
            string locationString = geoLocation.GetLocationString();
            
            string[] parts = locationString.Split(',');
            
            double latitude = double.Parse(parts[0].Trim());
            double longitude = double.Parse(parts[1].Trim());
            
            return new Coordinates(latitude, longitude);
        }
    }
}
