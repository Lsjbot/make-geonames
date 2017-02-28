#define DBGEOFLAG

using System;
using System.IO;
using DotNetWikiBot;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Threading;
using System.Web;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Data;

#if (DBGEOFLAG)
using System.Data.Entity.Spatial;
#endif

namespace make_geonames
{
    class Program
    {
        
        public static string geonamesfolder = @"C:\dotnwb3\Geonames\"; //will be modified according to computer name
        public static string extractdir = @"O:\dotnwb3\extract\";      //will be modified according to computer name

        public static string botname = "Lsjbot";
        public static string makelang = "sv";
        //public static string makecountry =    "AD,AE,AF,AG,AI,AL,AM,AO,AQ,AR,AS,AT,AU,AW,AX,AZ,BA,BB,BD,BE,BF,BG,BH,BI,BJ,BL,BM,BN,BO,BQ,BR,BS,BT,BV,BW,BY,BZ,CA,CC,CD,CF,CG,CH,CI,CK,CL,CM,CN,CO,CR,CU,CV,CW,CX,CY,CZ,DE,DJ,DK,DM,DO,DZ,EC,EE,EG,EH,ER,ES,ET,FI,FJ,FK,FM,FO,FR,GA,GB,GD,GE,GF,GG,GH,GI,GL,GM,GN,GP,GQ,GR,GS,GT,GU,GW,GY,HK,HM,HN,HR,HT,HU,ID,IE,IL,IM,IN,IO,IQ,IR,IS,IT,JE,JM,JO,JP,KE,KG,KH,KI,KM,KN,KP,KR,XK,KW,KY,KZ,LA,LB,LC,LI,LK,LR,LS,LT,LU,LV,LY,MA,MC,MD,ME,MF,MG,MH,MK,ML,MM,MN,MO,MP,MQ,MR,MS,MT,MU,MV,MW,MX,MY,MZ,NA,NC,NE,NF,NG,NI,NL,NO,NP,NR,NU,NZ,OM,PA,PE,PF,PG,PH,PK,PL,PM,PN,PR,PS,PT,PW,PY,QA,RE,RO,RS,RU,RW,SA,SB,SC,SD,SS,SE,SG,SH,SI,SJ,SK,SL,SM,SN,SO,SR,ST,SV,SX,SY,SZ,TC,TD,TF,TG,TH,TJ,TK,TL,TM,TN,TO,TR,TT,TV,TW,TZ,UA,UG,UM,US,UY,UZ,VA,VC,VE,VG,VI,VN,VU,WF,WS,YE,YT,ZA,ZM,ZW"; //Can be comma-separated list. Must be same number of components in the following three strings.
        //public static string makecountry =    "AG,MT,MK,SS,BH,BT,LU,AD,AE,AF,AI,AL,AM,AO,AQ,AR,AS,AT,AU,AW,AX,AZ,BA,BB,BD,BE,BF,BG,BI,BJ,BL,BM,BN,BO,BQ,BR,BS,BV,BW,BY,BZ,CA,CC,CD,CF,CG,CH,CI,CK,CL,CM,CN,CO,CR,CU,CV,CW,CX,CY,CZ,DE,DJ,DK,DM,DO,DZ,EC,EE,EG,EH,ER,ES,ET,FI,FJ,FK,FM,FO,FR,GA,GB,GD,GE,GF,GG,GH,GI,GL,GM,GN,GP,GQ,GR,GS,GT,GU,GW,GY,HK,HM,HN,HR,HT,HU,ID,IE,IL,IM,IN,IO,IQ,IR,IS,IT,JE,JM,JO,JP,KE,KG,KH,KI,KM,KN,KP,KR,XK,KW,KY,KZ,LA,LB,LC,LI,LK,LR,LS,LT,LU,LV,LY,MA,MC,MD,ME,MF,MG,MH,ML,MM,MN,MO,MP,MQ,MR,MS,MU,MV,MW,MX,MY,MZ,NA,NC,NE,NF,NG,NI,NL,NO,NP,NR,NU,NZ,OM,PA,PE,PF,PG,PH,PK,PL,PM,PN,PR,PS,PT,PW,PY,QA,RE,RO,RS,RU,RW,SA,SB,SC,SD,SE,SG,SH,SI,SJ,SK,SL,SM,SN,SO,SR,ST,SV,SX,SY,SZ,TC,TD,TF,TG,TH,TJ,TK,TL,TM,TN,TO,TR,TT,TV,TW,TZ,UA,UG,UM,US,UY,UZ,VA,VC,VE,VG,VI,VN,VU,WF,WS,YE,YT,ZA,ZM,ZW"; //Can be comma-separated list. Must be same number of components in the following three strings.
        public static string makecountry = "";//,AU,AW,AX,AZ";//"RU,UA,BY,RS,MK,KZ,KG,MN,BG,TJ";
        public static string makecountryname = "";//"Afghanistan,Albania,Antarctica,Armenia,Andorra,Burundi,Nicaragua,Bahamas,Macedonia,South Sudan,Bhutan,Luxembourg,Malta,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr,fr"; //Används av Makefork, samma engelska namnform som i namefork-filen
        //public static string makecountrywiki = "en";//"ru,ua,be,sr,mk,kk,ky,mn,bg,tg";
        //public static string makecountrywiki ="ca,ar,fa,en,en,sq,hy,pt,en,es,en,de,en,nl,fi,az,bs,en,bn,nl,fr,bg,ar,fr,fr,fr,en,ms,es,nl,pt,en,dz,en,en,be,en,en,ms,fr,fr,fr,de,fr,en,es,en,zh,es,es,es,pt,nl,en,el,cs,de,fr,da,en,es,ar,es,et,ar,ar,aa,es,am,fi,en,en,en,fo,fr,fr,en,en,ka,fr,en,en,en,kl,en,fr,fr,es,el,en,es,en,pt,en,zh,en,es,hr,ht,hu,id,en,he,en,en,en,ar,fa,is,it,en,en,ar,ja,en,ky,km,en,ar,en,ko,ko,sq,ar,en,kk,lo,ar,en,de,si,en,en,lt,lb,lv,ar,ar,fr,ro,sr,fr,fr,mh,mk,fr,my,mn,zh,fi,fr,ar,en,mt,en,dv,ny,es,ms,pt,en,fr,fr,en,en,es,nl,no,ne,na,ni,en,ar,es,es,fr,en,tl,ur,pl,fr,en,en,ar,pt,pa,es,ar,fr,ro,sr,ru,rw,ar,en,en,ar,en,sv,cm,en,sl,no,sk,en,it,fr,so,nl,pt,es,nl,ar,en,en,fr,fr,fr,th,tg,tk,te,tk,ar,to,tr,en,tv,zh,sw,uk,en,en,en,es,uz,la,en,es,en,en,vi,bi,wl,sm,ar,fr,zu,en,en"; //Används för iw
        public static int resume_at = -1; //-1: run from start; gnid to resume at.
        public static int stop_at = -1; //-1: run to end; gnid to stop at.
        public static string resume_at_fork = ""; //resume at for fork pages; empty string to run from start
        public static string resume_at_map = ""; //resume at for map pages; empty string to run from start NOT WORKING
        public static string testprefix = "";
        public static string kmlprefix = "Wikipedia:RobotKML/";
        //public static string nameforkdate = "20150803";
        public static char createclass = ' '; //create only articles with this feature class
        public static char createexceptclass = ' '; //create articles EXCEPT with this feature class
        public static string createfeature = ""; //create only articles with this feature code
        public static string createexceptfeature = "RK,RKS,AIRF,CSTL"; //create all articles EXCEPT with this feature code
        public static string createcategory = ""; //create only articles in this category
        public static string createexceptcategory = "lakes,streams"; //create all articles EXCEPT in this category
        public static int createunit = -1; //create only articles for place in admin.unit with gnid=createunit; <0 to make all
        public static int createexceptunit = -1; //create only articles for place NOT in admin.unit with gnid=createexceptunit; <0 to make all

        //======================================================
        // Flags setting the main task for a run.
        // Unpredictable results if multiple flags are true.
        //======================================================
        public static bool makearticles = false; //create wiki pages
        public static bool makespecificarticles = false; //create specific wiki pages, one by one
        public static bool remakearticleset = false; //remake a selected set of articles; overwrite must be true
        public static bool altnamesonly = false; //create namefork file
        public static bool makefork = false; //create fork pages on wiki
        public static bool checkdoubles = false; //create artname file
        public static bool checkwikidata = false; //create wikidata-XX files
        public static bool makeislands = false; //create islands-XX files
        public static bool makelakes = false; //create lakes-XX files
        public static bool makerivers = false; //create rivers-XX files
        public static bool makeranges = false; //create ranges-XX files
        public static bool verifygeonames = false; //doublecheck geonames against wiki
        public static bool verifywikidata = false; //doublecheck wikidata
        public static bool verifyislands = false; //verify island files, removing duplicates
        public static bool verifylakes = false; //verify island files, removing duplicates
        public static bool makealtitude = false;    //make altitude_XX files
        public static bool maketranslit = false;    //make translit_XX files
        public static bool makeworldmaponly = false; //create world map
        public static bool statisticsonly = false; //gather statistics without actually making anything; can be combined with other options
        public static bool savefeaturelink = false; //save a list of what feature codes link to
        public static bool savewikilinks = false; //save a list of what the bot wikilinks to
        public static bool saveadmlinks = false; //save a list of what the bot wikilinks to
        public static bool manualcheck = false; //check name matches manually, if true
        public static bool listnative = false; //list article names from native wiki of a country
        public static bool forkduplicates = false; //make namefork-duplicates file
        public static bool fixsizecats = false; //fix size categories
        public static bool testnasa = false; //test nasa data
        public static bool retrofitnasa = false; // retrofit existing articles with nasa data
        public static int resurrection = 0; //if >0, make only gnid in resurrected; if <0 skip resurrected; if 0, disregard.
        public static bool checkminutes = false; //check coordinates if rounded to whole minutes
        public static bool countrycenters = false;

        public static bool prefergeonamespop = true; //use population figures from GeoNames rather than wiki
        public static bool makedoubles = true;      //if suspected double, make article as a copy in doubleprefix folder
        public static bool overwrite = false;       //if article already exists, overwrite with a new version (if not human-edited)

        public static bool reallymake = true;  //if false, do dry run without actually loading from or saving to wiki; can be combined with other options
        public static bool pauseaftersave = false;  //if true, wait for keypress after saving each article

        public static bool firstround = true;

        public static int maxread = 100000000; //Set to a small number for a limited test run, or to 100000000 for a full run

        public class geonameclass //main class for GeoNames entries
        {
            public string Name = ""; //main name
            public string Name_ml = ""; //name in makelang language 
            public string asciiname = ""; //name in plain ascii
            public List<string> altnames = new List<string>(); //alternative names
            public double latitude = 0.0;
            public double longitude = 0.0;
            public char featureclass;
            public string featurecode;
            //public string countrycode = "XX"; //GeonameID of country in adm[0]!
            public List<int> altcountry = new List<int>();
            public List<int> children = new List<int>(); //index of children
            public List<int> features = new List<int>(); //index of features in adm-unit
            public int[] adm = new int[5]; // adm[0] = country, adm[1] = province, etc.
            public long population = 0; //pop according to geonames
            public long population_wd = 0; //pop according to wikipedia
            public string population_wd_iw = ""; //language from which pop is taken
            public int elevation = 0;
            public int elevation_vp = 0;
            public int prominence = 0;
            public double width = 0; //largest horizontal dimension in km
            public int island = 0; //if located on island, gnid of island
            public int inlake = 0; //if located in lake, gnid of lake
            public int inrange = 0; //if mountain part of range, gnid of range
            public List<int> atlakes = new List<int>(); //near the shore of lake(s) in list 
            public double area = 0.0; //area from wikipedia

            public int dem = 0;
            public string tz = "";
            public string moddate = "1901-01-01";

            public string articlename = ""; //article name, with disambiguation
            public string unfixedarticlename = ""; //article name before fixing oddities
            public string oldarticlename = ""; //article name in previous bot run
            public string artname2 = ""; //article name in other bot language, for interwiki linking
            public int wdid = -1; //wikidata id
            public bool roundminute = false; //coordinates rounded to whole minutes?
        }

        public class forkclass //class for entries in a fork page
        {
            public int geonameid = 0;
            public string featurecode = "";
            public string[] admname = new string[3];
            public double latitude = 0.0;
            public double longitude = 0.0;
            public string realname = "*";
            public int wdid = -1;    //wikidata id
            public string iso = "XX"; //country iso code
            public string featurename = "";
        }

        public class altnameclass //class for alternate names from GeoNames
        {
            public int altid = 0;
            public string altname = "";
            public int ilang = -1;
            public string wikilink = "";
            public bool official = false;
            public bool shortform = false;
            public bool colloquial = false;
            public bool historic = false;
        }

        public class countryclass //class for country data
        {
            public string Name = ""; //main name
            public string Name_ml = ""; //name in makelang language 
            public string asciiname = ""; //name in plain ascii
            public List<string> altnames = new List<string>(); //alternative names
            public string iso = "XX";
            public string iso3 = "XXX";
            public int isonumber = 0;
            public string fips = "XX";
            public string capital = "";
            public int capital_gnid = 0;
            public double area = 0.0;
            public long population = 0;
            public string continent = "EU";
            public string tld = ".xx";
            public string currencycode = "USD";
            public string currencyname = "Dollar";
            public string phone = "1-999";
            public string postalcode = "#####";
            public string nativewiki = "";
            public List<int> languages = new List<int>();
            public List<string> bordering = new List<string>();
#if (DBGEOFLAG)
            public shapeclass shape = null; //country shape(s); null means no shapefile. Shapes unreliable for Antarctica, Fiji, Norway, Russia
#endif
            public double clat = 9999; //lat, long of centroid of country shape
            public double clon = 9999;
        }

        public class langclass
        {
            public string iso3 = "";
            public string iso2 = "";
            public Dictionary<string, string> name = new Dictionary<string, string>(); //name of language in different languages. Iso -> name.
        }

        public class admclass //names of administrative entities by level, one admclass for each country
        {
            public string[] label = new string[5];
            public string[] det = new string[5];
            public int maxadm = 5;
        }

        public class tzclass //time zone class
        {
            public string offset = "0";
            public string summeroffset = "0";
            public string rawoffset = "0";
            public string tzname = "";
            public string tzsummer = "";
            public string tzfull = "";
            public string tzfullsummer = "";
        }

        public class islandclass //data for each island
        {
            public double area = 0;
            public double kmew = 0;
            public double kmns = 0;
            public List<int> onisland = new List<int>(); //list of GeoNames id of entities located on the island.
        }

        public class rangeclass //data for each MTS/HLLS
        {
            public double length = 0;
            public string orientation = "....";
            public double angle = 0; //polar angle of long axis (radians). 0 or pi = EW, pi/2 or 3pi/2 = NS etc.
            public double kmew = 0;
            public double kmns = 0;
            public int maxheight = 0; //highest point; gnid of peak if negative, height if positive
            public double hlat = 999; //latitude/longitude of highest point
            public double hlon = 999;
            public List<int> inrange = new List<int>(); //list of GeoNames id of mountains in the range.
        }

        public class lakeclass //data for each lake
        {
            public double area = 0;
            public double glwd_area = 0;
            public double kmew = 0;
            public double kmns = 0;
            public int higher = 0; //edge pixels higher than lake surface
            public int lower = 0; //edge pixels lower than lake surface
            public int overlaps_with = -1; //if two lakes overlap, gnid of the other one
            public int glwd_id = -1; //id number in GLWD lakes database, -1 if not found
            public List<int> inlake = new List<int>(); //list of GeoNames id of entities located in the lake (mainly islands).
            public List<int> atlake = new List<int>(); //list of GeoNames id of entities located around the lake.
        }

        public class riverclass //data for each river
        {
            public double area = 0; //area of watershed
            public double kmew = 0;
            public double kmns = 0;
            public double length = 0;
            public int tributary_of = -1; //if river joins another, gnid of the other
            public List<int> tributaries = new List<int>(); //list of rivers joining this one
            public List<int> inarea = new List<int>(); //list of GeoNames id of entities located in the catchment area of the river.
        }

        public class coordclass
        {
            public double lat = 9999;
            public double lon = 9999;
        }

        public class Disambigclass //class for disambiguation in article names
        {
            public bool existsalready = false;
            public bool country = false;
            public bool adm1 = false;
            public bool adm2 = false;
            public bool latlong = false;
            public bool fcode = false;
            public forkclass fork = new forkclass();
        }

        public class wdminiclass //minimal wikidata entry needed for verifying Geonames-links
        {
            public int gnid = 0;
            public double latitude = 9999.9;
            public double longitude = 9999.9;
            public List<int> instance_of = new List<int>();
            public double dist = 9999.9;
            public bool okdist = false;
            public bool okclass = false;
            public bool goodmatch = false;
        }

        public class existingclass //for existing geography articles on target wiki
        {
            public string articlename = "";
            public double latitude = 9999.9;
            public double longitude = 9999.9;
        }

        public class nasaclass
        {
            public int landcover = -1; //Landcover code 1-17 http://eospso.nasa.gov/sites/default/files/atbd/atbd_mod12.pdf
            public int popdensity = -1; //people per square km
            public int temp_average = -999; //average across months and day-night
            public int temp_max = -999; //temp of hottest month
            public int month_max = -999; //hottest month (1-12)
            public int temp_min = -999; //temp of coldest month
            public int month_min = -999; //coldest month
            public int temp_daynight = -999; //average difference between day and night
            public int rainfall = -999; //mm per year
            public int rain_max = -999; //rain wettest month
            public int rain_month_max = -999; //wettest month (1-12)
            public int rain_min = 99999; //rain driest month
            public int rain_month_min = -999; //driest month
            public double rainfall_double = 0; //mm per year
            public int koppen = -1;
            public int[] month_temp_day = new int[13] { -999, -999, -999, -999, -999, -999, -999, -999, -999, -999, -999, -999, -999 };
            public int[] month_temp_night = new int[13] { -999, -999, -999, -999, -999, -999, -999, -999, -999, -999, -999, -999, -999 };
            public int[] month_rain = new int[13] { -999, -999, -999, -999, -999, -999, -999, -999, -999, -999, -999, -999, -999 };
        }

        public class chinese_pop_class
        {
            public int adm1 = -1;
            public long pop = -1;
            public long malepop = -1;
            public long femalepop = -1;
            public long households = -1;
            public long pop014 = -1;
            public long pop1564 = -1;
            public long pop65 = -1;
        }

#if (DBGEOFLAG)
        public class shapeclass
        {
            public Dictionary<string, string> metadict = new Dictionary<string, string>();
            public List<DbGeography> shapes = new List<DbGeography>();
        }
#endif

        public class locatorclass //for locator maps
        {
            public string locatorname = ""; //country name in locator template on target wiki
            public string locatorimage = "";//map image name
            public string altlocator = "";
            public double latmin = -999;
            public double latmax = -999;
            public double lonmin = -999;
            public double lonmax = -999;
            public bool loaded = false;
            public string get_locator(double lat, double lon)
            {
                if (!loaded)
                {
                    string templatename = mp(63) + mp(72).Replace("{{", "") + " " + locatorname;
                    Console.WriteLine(templatename);
                    //string imagename = "";
                    Page ltp = new Page(makesite, templatename);
                    tryload(ltp, 2);
                    if (ltp.Exists())
                    {
                        locatorimage = get_pictureparam(ltp);
                        latmax = get_edgeparam(ltp, "top");
                        latmin = get_edgeparam(ltp, "bottom");
                        lonmax = get_edgeparam(ltp, "right");
                        lonmin = get_edgeparam(ltp, "left");
                    }
                    loaded = true;
                }

                if (String.IsNullOrEmpty(altlocator))
                {
                    if (String.IsNullOrEmpty(locatorname))
                    {
                        if (makelang == "sv")
                            return "Världen";
                        else
                            return "World";
                    }
                    else
                        return locatorname;
                }

                if (latmin < -99) //failed to get edges, probably complicated coordinates
                    return locatorname;

                if (lat < latmin)
                    return altlocator;
                if (lat > latmax)
                    return altlocator;
                if (lon < lonmin)
                    return altlocator;
                if (lon > lonmax)
                    return altlocator;
                return locatorname;
            }

        }

        public static Dictionary<int, geonameclass> gndict = new Dictionary<int, geonameclass>();
        public static Dictionary<int, countryclass> countrydict = new Dictionary<int, countryclass>(); //from geoname ID to country info
        public static Dictionary<string, int> countryid = new Dictionary<string, int>(); //from ISO code to geoname ID for countries
        public static Dictionary<string, string> countryml = new Dictionary<string, string>(); // from English name to makelang name
        public static Dictionary<string, string> countryiso = new Dictionary<string, string>(); // from English name to ISO
        public static Dictionary<string, locatorclass> locatordict = new Dictionary<string, locatorclass>(); // from English name to locator map name
        //public static Dictionary<string, string> locatorimage = new Dictionary<string, string>(); //from English name to locator map image
        public static Dictionary<string, List<int>> namefork = new Dictionary<string, List<int>>(); //names with list of corresponding geonameid(s)
        public static Dictionary<string, Dictionary<string, int>> adm1dict = new Dictionary<string, Dictionary<string, int>>();
        public static Dictionary<string, Dictionary<string, Dictionary<string, int>>> adm2dict = new Dictionary<string, Dictionary<string, Dictionary<string, int>>>();
        public static Dictionary<int, Dictionary<int, List<int>>> latlong = new Dictionary<int, Dictionary<int, List<int>>>(); //List of all places in same square degree
        public static Dictionary<int, Dictionary<int, List<int>>> exlatlong = new Dictionary<int, Dictionary<int, List<int>>>(); //List of all places in same square degree
        public static Dictionary<string, string> featuredict = new Dictionary<string, string>(); //From featurecode to feature name in makelang
        public static Dictionary<string, char> featureclassdict = new Dictionary<string, char>(); //From featurecode to feature class
        public static Dictionary<string, string> geoboxdict = new Dictionary<string, string>(); //From featurecode to geobox type
        public static Dictionary<string, string> geoboxtemplates = new Dictionary<string, string>(); //From geobox type to geobox template
        public static Dictionary<string, string> categorydict = new Dictionary<string, string>(); //from featurecode to category
        public static Dictionary<string, string> parentcategory = new Dictionary<string, string>(); //from category to parent category
        public static Dictionary<string, string> categoryml = new Dictionary<string, string>(); //from category to category name in makelang
        public static Dictionary<string, int> catstatdict = new Dictionary<string, int>(); //category statistics
        public static Dictionary<string, double> catnormdict = new Dictionary<string, double>(); //category statistics
        public static Dictionary<string, bool> featurepointdict = new Dictionary<string, bool>(); //true if feature is pointlike, false if extended
        public static Dictionary<string, bool> minutesensitivedict = new Dictionary<string, bool>(); //true if feature is sensitive to coordinate rounding errors
        public static List<string> noclimatelist = new List<string>(); //list of feature codes that should NOT have climate data in their articles
        public static Dictionary<int, islandclass> islanddict = new Dictionary<int, islandclass>();
        public static Dictionary<int, rangeclass> rangedict = new Dictionary<int, rangeclass>();
        public static Dictionary<int, lakeclass> lakedict = new Dictionary<int, lakeclass>();
        public static Dictionary<int, int> wdgniddict = new Dictionary<int, int>(); // from wdid to gnid
        //public static Dictionary<int, int> wdgnid = new Dictionary<int, int>(); //from wikidata id to geonames id; negative counts duplicates
        public static Dictionary<string, int> catwdclass = new Dictionary<string, int>(); //from category to appropriate wd top class
        public static Dictionary<string, List<string>> catwdinstance = new Dictionary<string, List<string>>(); //from category to list of appropriate wd instance_of
        public static Dictionary<string, string> admcap = new Dictionary<string, string>(); //names in makelang of capitals of various types of administrative units
        public static Dictionary<int, existingclass> existingdict = new Dictionary<int, existingclass>(); //already existing articles
        public static Dictionary<int, existingclass> ghostdict = new Dictionary<int, existingclass>(); //towns with no known population
        public static Dictionary<string, string> motherdict = new Dictionary<string, string>(); //for overseas possessions: from territory ISO to mother country ISO
        public static Dictionary<int, string> specialfeaturedict = new Dictionary<int, string>(); //for places that are exceptions to the usual feature labels
        public static Dictionary<string, List<chinese_pop_class>> chinese_pop_dict = new Dictionary<string, List<chinese_pop_class>>();
        public static Dictionary<int, chinese_pop_class> chinese_pop_dict2 = new Dictionary<int, chinese_pop_class>();
        public static Dictionary<int, string> iatadict = new Dictionary<int, string>();
        public static Dictionary<int, string> icaodict = new Dictionary<int, string>();
        public static List<int> resurrected = new List<int>(); //gnid of articles "resurrected" from ghosts with new population data; mostly used in China.

        public static Dictionary<int, List<altnameclass>> altdict = new Dictionary<int, List<altnameclass>>();
        public static List<string> geoboxlist = new List<string>(); //List of geobox types used
        public static Dictionary<int, langclass> langdict = new Dictionary<int, langclass>(); //main language table
        public static Dictionary<string, int> langtoint = new Dictionary<string, int>(); //from iso to integer code. Both iso2 and iso3 used as keys to the same int
        public static List<string> coordparams = new List<string>(); //possible template parameters for latitude/longitude
        public static Dictionary<int, string> artnamedict = new Dictionary<int, string>();
        public static Dictionary<int, string> oldartnamedict = new Dictionary<int, string>();
        public static Dictionary<string, tzclass> tzdict = new Dictionary<string, tzclass>(); //Time zone dictionary
        public static Dictionary<string, string> tznamedict = new Dictionary<string, string>(); //from timezone offset to timezone acronym (standard time)
        public static Dictionary<string, string> tzsummerdict = new Dictionary<string, string>(); //from timezone offset to timezone acronym (summer time)
        public static Dictionary<string, string> tzfulldict = new Dictionary<string, string>(); //from timezone offset to timezone full name (standard time)
        public static Dictionary<string, string> tzfullsummerdict = new Dictionary<string, string>(); //from timezone offset to timezone full name (summer time)
        public static Dictionary<string, int> propdict = new Dictionary<string, int>(); //from wikidata property name to property id
        public static string[] iwlang = { "en", "fr", "de", "es" };
        public static Dictionary<string, Site> iwsites = new Dictionary<string, Site>();
        public static long minimum_population = 100;
        public static double minimum_area = 0.1;
        public static int minimum_prominence = 50;
        public static DateTime wdtime = new DateTime();
        public static DateTime gnfiledate = new DateTime();
        public static string dumpdate = "";
        public static bool hasnotes = false;
        public static Dictionary<long, long> popvspop = new Dictionary<long, long>(); //comparing population for same place, wd vs gn
        public static Dictionary<double, double> areavsarea = new Dictionary<double, double>(); //comparing area for same place, wd vs gn
        public static int nwdtot = 0;
        public static Exception eglob;
        public static bool locatoringeobox = false;  //only works in Swedish!
        public static List<string> forktemplates = new List<string>();
        public static Dictionary<string, string> featurearticle = new Dictionary<string, string>();//from feature name to article name for feature
        public static Dictionary<string, string> alphabet_sv = new Dictionary<string, string>();//from English name to Swedish name of alphabets
        public static Dictionary<string, string> funny_quotes = new Dictionary<string, string>();
        public static List<string> donecountries = new List<string>();
        public static List<string> donecats = new List<string>();
        public static Dictionary<int, nasaclass> nasadict = new Dictionary<int, nasaclass>();
        public static Dictionary<string, int> climatemismatchdict = new Dictionary<string, int>();
        public static int pausetime = 5; //time between saves, modified depending on task
        public static List<string> nocapital = new List<string>(); //countries with no capital or capital filling country
        public static List<int> blacklist = new List<int>(); //list of geonames id NOT to create

#if (DBGEOFLAG)
        public static Dictionary<int, shapeclass> lakeshapedict = new Dictionary<int, shapeclass>();
        public static Dictionary<int, List<int>> countrylakedict = new Dictionary<int, List<int>>();
#endif
        public static Dictionary<int, int> wdid_buffer = new Dictionary<int, int>();
        public static int resume_at_wdid = -1;

        public static string mapfilecache = "NO CACHE";
        public static int[,] mapcache = new int[3603, 3603];

        public static string doubleprefix = "Användare/Lsjbot/Dubletter";

        public static Dictionary<string, admclass> admdict = new Dictionary<string, admclass>();
        public static Dictionary<string, string> admtodet = new Dictionary<string, string>(); //from base form to determinate form of adm labels

        public static Dictionary<string, List<string>> existing_adm1 = new Dictionary<string, List<string>>();

        public static Page pconflict = null;
        public static Page panomaly = null;
        public static bool conflictheadline = false;
        public static bool anomalyheadline = false;

        public static hbookclass featurehist = new hbookclass();

        public static int nedit = 0;
        public static DateTime oldtime = DateTime.Now;

        public static List<string> refnamelist = new List<string>();
        public static string reflist = "<references>";
        public static string password = "";
        public static string tabstring = "\t";
        public static char tabchar = tabstring[0];


        public static XmlDocument currentxml = new XmlDocument();
        //public static XmlNode currentnode;
        public static int wdid = -1;

        public static Dictionary<int, string> phrases = new Dictionary<int, string>();
        public static Site makesite;
        public static Site ensite;
        public static Site cmsite;
        //public static Site wdsite;
        public static Page pstats;

        public static int badelevation = -99999;

        public static CultureInfo culture = CultureInfo.CreateSpecificCulture("sv-SE");
        public static CultureInfo culture_en = CultureInfo.CreateSpecificCulture("en-US");
        public static NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
        public static NumberFormatInfo nfi_en = new CultureInfo("en-US", false).NumberFormat;
        public static NumberFormatInfo nfi_space = new CultureInfo("en-US", false).NumberFormat;

        public class rdfclass
        {
            public int obj = -1;
            public string objstring = "";
            public int prop = -1;
            public int objlink = -1;
            public string value = "";
        }

        public class wdtreeclass
        {
            public List<int> uplinks = new List<int>();
            public List<int> downlinks = new List<int>();
        }

        public static Dictionary<int, wdtreeclass> wdtree = new Dictionary<int, wdtreeclass>();

        public class transliterationclass
        {
            private string defaultlang = "ru";
            private string badreturn = "?";
            private string contextdependent = "*";
            private List<char> vowels = new List<char>();
            public List<char> badlist = new List<char>();

            private Dictionary<string, Dictionary<char, string>> tldict = new Dictionary<string, Dictionary<char, string>>();

            public void Addchar(char fromchar, string tochars)
            {
                Addchar(fromchar, tochars, defaultlang, false);
            }

            public void Addchar(char fromchar, string tochars, bool isvowel)
            {
                Addchar(fromchar, tochars, defaultlang, isvowel);
            }

            public void Addchar(char fromchar, string tochars, string lang)
            {
                Addchar(fromchar, tochars, lang, false);
            }

            public void Addchar(char fromchar, string tochars, string lang, bool isvowel)
            {
                if (!tldict.ContainsKey(lang))
                {
                    Dictionary<char, string> csdict = new Dictionary<char, string>();
                    tldict.Add(lang, csdict);
                }
                if (!tldict[lang].ContainsKey(fromchar))
                {
                    tldict[lang].Add(fromchar, tochars);
                    if (isvowel)
                        if (!vowels.Contains(fromchar))
                            vowels.Add(fromchar);
                }
            }

            private string Transchar(char fromchar, char contextbefore, char contextafter, string langparam)
            {
                if (Convert.ToInt32(fromchar) <= 0x0041) //punctuation etc.
                    return fromchar.ToString();

                string lang = langparam;
                if (!tldict.ContainsKey(lang)) //russian default language 
                    lang = defaultlang;
                if (!tldict[lang].ContainsKey(fromchar))
                    lang = defaultlang;
                if (!tldict[lang].ContainsKey(fromchar))
                {
                    if (is_latin(fromchar.ToString()))
                        return fromchar.ToString();
                    if (!badlist.Contains(fromchar))
                        badlist.Add(fromchar);
                    return badreturn;
                }

                if (tldict[lang][fromchar] != contextdependent)
                    return tldict[lang][fromchar];
                else //context-dependent:
                {
                    List<char> tszlist = new List<char> { 'С', 'с', 'Т', 'т', 'З', 'з' };
                    List<char> jlist = new List<char> { 'Ж', 'ж', 'Љ', 'љ', 'Ш', 'ш', 'Щ', 'щ', 'Й', 'й', 'Ч', 'ч' };

                    if (fromchar == 'Е')
                    {
                        if ((contextbefore == '#') || (vowels.Contains(contextbefore)))
                            return "Je";
                        else
                            return "E";
                    }
                    else if (fromchar == 'е')
                    {
                        if ((contextbefore == '#') || (vowels.Contains(contextbefore)))
                            return "je";
                        else
                            return "e";
                    }
                    else if (fromchar == 'Ё')
                    {
                        if (tszlist.Contains(contextbefore))
                            return "Io";
                        else if (jlist.Contains(contextbefore))
                            return "O";
                        else
                            return "Jo";
                    }
                    else if ((fromchar == 'ë') || (fromchar == 'ё'))
                    {
                        if (tszlist.Contains(contextbefore))
                            return "io";
                        else if (jlist.Contains(contextbefore))
                            return "o";
                        else
                            return "jo";
                    }
                    else if (fromchar == 'Ю')
                    {
                        if (tszlist.Contains(contextbefore))
                            return "Iu";
                        else if (jlist.Contains(contextbefore))
                            return "U";
                        else
                            return "Ju";
                    }
                    else if (fromchar == 'ю')
                    {
                        if (tszlist.Contains(contextbefore))
                            return "iu";
                        else if (jlist.Contains(contextbefore))
                            return "u";
                        else
                            return "ju";
                    }
                    else if (fromchar == 'Я')
                    {
                        if (tszlist.Contains(contextbefore))
                            return "Ia";
                        else if (jlist.Contains(contextbefore))
                            return "A";
                        else
                            return "Ja";
                    }
                    else if (fromchar == 'я')
                    {
                        if (tszlist.Contains(contextbefore))
                            return "ia";
                        else if (jlist.Contains(contextbefore))
                            return "a";
                        else
                            return "ja";
                    }
                    else
                    {
                        if (!badlist.Contains(fromchar))
                            badlist.Add(fromchar);

                        return badreturn;
                    }

                }
            }

            public string Transliterate(string name, string lang)
            {
                char[] letters = name.ToCharArray();
                string result = "";
                for (int ic = 0; ic < letters.Length; ic++)
                {
                    char contextbefore = '#';
                    if (ic > 0)
                        contextbefore = letters[ic - 1];
                    char contextafter = '#';
                    if (ic < letters.Length - 1)
                        contextafter = letters[ic + 1];
                    result += Transchar(letters[ic], contextbefore, contextafter, lang);
                }

                return result;
            }
        }

        public static transliterationclass cyrillic = new transliterationclass();

        public static void make_translit()
        {
            int icountry = countryid[makecountry];
            using (StreamWriter sw = new StreamWriter("translit-" + makecountry + ".txt"))
            {
                foreach (int gnid in altdict.Keys)
                {
                    bool found = false;
                    string langtouse = countrydict[icountry].nativewiki;
                    foreach (altnameclass ac in altdict[gnid])
                    {
                        //if (countrydict[icountry].languages.Contains(ac.ilang))
                        {
                            if (langdict.ContainsKey(ac.ilang))
                            {
                                string langname = langdict[ac.ilang].iso2;
                                if (langname != langtouse)
                                    continue;
                                string alphabet = get_alphabet(ac.altname);
                                //Console.WriteLine(ac.altname + ": " + alphabet);
                                if (alphabet == "cyrillic")
                                {
                                    //Console.WriteLine("Transliteration: " + cyrillic.Transliterate(ac.altname, "ru"));
                                    sw.WriteLine(gnid.ToString() + tabstring + ac.altname + tabstring + cyrillic.Transliterate(ac.altname, langname));
                                    found = true;
                                }
                            }
                        }
                    }
                    if ((!found) && ((makecountry == "UA") || (makecountry == "BY") || (makecountry == "KZ") || (makecountry == "KG") || (makecountry == "TJ")))
                    {
                        langtouse = "ru";
                        foreach (altnameclass ac in altdict[gnid])
                        {
                            //if (countrydict[icountry].languages.Contains(ac.ilang))
                            {
                                if (langdict.ContainsKey(ac.ilang))
                                {
                                    string langname = langdict[ac.ilang].iso2;
                                    if (langname != langtouse)
                                        continue;
                                    string alphabet = get_alphabet(ac.altname);
                                    //Console.WriteLine(ac.altname + ": " + alphabet);
                                    if (alphabet == "cyrillic")
                                    {
                                        //Console.WriteLine("Transliteration: " + cyrillic.Transliterate(ac.altname, "ru"));
                                        sw.WriteLine(gnid.ToString() + tabstring + ac.altname + tabstring + cyrillic.Transliterate(ac.altname, langname));
                                        found = true;
                                    }
                                }
                            }
                        }
                    }
                }
                sw.Write("Badlist:");
                foreach (char c in cyrillic.badlist)
                    sw.Write(c);
                sw.WriteLine();
            }
            altdict.Clear();
        }

        public static void fill_cyrillic()
        {
            //Swedish transliteration!
            cyrillic.Addchar('А', "A", true);
            cyrillic.Addchar('а', "a", true);
            cyrillic.Addchar('Б', "B");
            cyrillic.Addchar('б', "b");
            cyrillic.Addchar('В', "V");
            cyrillic.Addchar('в', "v");
            cyrillic.Addchar('Г', "H", "uk");
            cyrillic.Addchar('г', "h", "uk");
            cyrillic.Addchar('Г', "H", "be");
            cyrillic.Addchar('г', "h", "be");
            cyrillic.Addchar('Г', "G");
            cyrillic.Addchar('г', "g");
            cyrillic.Addchar('Ѓ', "Ǵ");
            cyrillic.Addchar('ѓ', "ǵ");
            cyrillic.Addchar('Ґ', "G");
            cyrillic.Addchar('ґ', "g");
            cyrillic.Addchar('Д', "D");
            cyrillic.Addchar('д', "d");
            cyrillic.Addchar('Ђ', "D");
            cyrillic.Addchar('ђ', "đ");
            cyrillic.Addchar('Ђ', "Dj", "sr");
            cyrillic.Addchar('ђ', "dj", "sr");
            cyrillic.Addchar('Е', "*", true);
            cyrillic.Addchar('е', "*", true);
            cyrillic.Addchar('Е', "E", "uk", true);
            cyrillic.Addchar('е', "e", "uk", true);
            cyrillic.Addchar('Е', "E", "bg", true);
            cyrillic.Addchar('е', "e", "bg", true);
            cyrillic.Addchar('Ё', "*", true);
            cyrillic.Addchar('ë', "*", true);
            cyrillic.Addchar('ё', "*", true);
            cyrillic.Addchar('Є', "Je", true);//є
            cyrillic.Addchar('є', "je", true);
            cyrillic.Addchar('Ж', "Zj");
            cyrillic.Addchar('ж', "zj");
            cyrillic.Addchar('Ж', "Ž", "sr");
            cyrillic.Addchar('ж', "Ž".ToLower(), "sr");
            cyrillic.Addchar('Ж', "Ž", "mk");
            cyrillic.Addchar('ж', "Ž".ToLower(), "mk");
            cyrillic.Addchar('З', "Z");
            cyrillic.Addchar('з', "z");
            cyrillic.Addchar('Ѕ', "Dz", "mk");
            cyrillic.Addchar('ѕ', "dz", "mk");
            cyrillic.Addchar('И', "Y", "uk", true);
            cyrillic.Addchar('и', "y", "uk", true);
            cyrillic.Addchar('И', "Y", "be", true);
            cyrillic.Addchar('и', "y", "be", true);
            cyrillic.Addchar('И', "I", true);
            cyrillic.Addchar('и', "i", true);
            cyrillic.Addchar('Й', "J");
            cyrillic.Addchar('й', "j");
            cyrillic.Addchar('І', "I", true);
            cyrillic.Addchar('і', "і", true);
            cyrillic.Addchar('Ї', "Ji", true);
            cyrillic.Addchar('ї', "ji", true);
            cyrillic.Addchar('J', "J");
            cyrillic.Addchar('j', "j");
            cyrillic.Addchar('К', "K");//seemingly identical
            cyrillic.Addchar('K', "K");//but different unicodes
            cyrillic.Addchar('к', "k");
            cyrillic.Addchar('Ќ', "Ḱ");
            cyrillic.Addchar('ќ', "ḱ");
            cyrillic.Addchar('Л', "L");
            cyrillic.Addchar('л', "l");
            cyrillic.Addchar('Љ', "Lj");
            cyrillic.Addchar('љ', "lj");
            cyrillic.Addchar('М', "M");
            cyrillic.Addchar('м', "m");
            cyrillic.Addchar('Н', "N");
            cyrillic.Addchar('н', "n");
            cyrillic.Addchar('Њ', "Nj");
            cyrillic.Addchar('њ', "nj");
            cyrillic.Addchar('О', "O", true);
            cyrillic.Addchar('о', "o", true);
            cyrillic.Addchar('o', "o", true);
            cyrillic.Addchar('П', "P");
            cyrillic.Addchar('п', "p");
            cyrillic.Addchar('Р', "R");
            cyrillic.Addchar('р', "r");
            cyrillic.Addchar('С', "S");//seemingly identical
            cyrillic.Addchar('C', "S");//but different unicodes
            cyrillic.Addchar('с', "s");
            cyrillic.Addchar('Т', "T");
            cyrillic.Addchar('т', "t");
            cyrillic.Addchar('Ћ', "Ć");
            cyrillic.Addchar('ћ', "ć");
            cyrillic.Addchar('У', "U", true);
            cyrillic.Addchar('у', "u", true);
            cyrillic.Addchar('Ў', "Ŭ", true);
            cyrillic.Addchar('ў', "ŭ", true);
            cyrillic.Addchar('Ф', "F");
            cyrillic.Addchar('ф', "f");
            cyrillic.Addchar('Х', "H", "sr");
            cyrillic.Addchar('х', "h", "sr");
            cyrillic.Addchar('Х', "H", "mk");
            cyrillic.Addchar('х', "h", "mk");
            cyrillic.Addchar('Х', "Ch");
            cyrillic.Addchar('х', "ch");
            cyrillic.Addchar('Ц', "Ts");
            cyrillic.Addchar('ц', "ts");
            cyrillic.Addchar('Ц', "C", "sr");
            cyrillic.Addchar('ц', "c", "sr");
            cyrillic.Addchar('Ц', "C", "mk");
            cyrillic.Addchar('ц', "c", "mk");
            cyrillic.Addchar('Ч', "Tj");
            cyrillic.Addchar('ч', "tj");
            cyrillic.Addchar('Ч', "Č", "sr");
            cyrillic.Addchar('ч', "Č".ToLower(), "sr");
            cyrillic.Addchar('Ч', "Č", "mk");
            cyrillic.Addchar('ч', "Č".ToLower(), "mk");
            cyrillic.Addchar('Џ', "Dž");
            cyrillic.Addchar('џ', "dž");
            cyrillic.Addchar('Ш', "Sj");
            cyrillic.Addchar('ш', "sj");
            cyrillic.Addchar('Ш', "Š", "sr");
            cyrillic.Addchar('ш', "Š".ToLower(), "sr");
            cyrillic.Addchar('Щ', "Sjt", "bg");
            cyrillic.Addchar('щ', "sjt", "bg");
            cyrillic.Addchar('Щ', "Sjtj");
            cyrillic.Addchar('щ', "sjtj");
            cyrillic.Addchar('Ъ', "", true);
            cyrillic.Addchar('ъ', "", true);
            cyrillic.Addchar('Ъ', "", true);
            cyrillic.Addchar('ъ', "", true);
            cyrillic.Addchar('Ы', "Y", true);
            cyrillic.Addchar('ы', "y", true);
            cyrillic.Addchar('Ь', "", true);
            cyrillic.Addchar('ь', "", true);
            cyrillic.Addchar('Ѣ', "", true);
            cyrillic.Addchar('ѣ', "", true);
            cyrillic.Addchar('Э', "E", true);
            cyrillic.Addchar('э', "e", true);
            cyrillic.Addchar('Ю', "*", true);
            cyrillic.Addchar('ю', "*", true);
            cyrillic.Addchar('Я', "*", true);
            cyrillic.Addchar('я', "*", true);
            cyrillic.Addchar('Ө', "Ḟ");
            cyrillic.Addchar('ө', "ḟ");
            cyrillic.Addchar('Ѵ', "Ẏ");
            cyrillic.Addchar('ѵ', "ẏ");
            cyrillic.Addchar('Ѫ', "A", true);
            cyrillic.Addchar('ѫ', "a", true);//“”
            cyrillic.Addchar('“', "“");
            cyrillic.Addchar('”', "”");
            cyrillic.Addchar('«', "«");
            cyrillic.Addchar('»', "»");
            cyrillic.Addchar('’', "’");
            cyrillic.Addchar('„', "„");
            cyrillic.Addchar('´', "");//
            cyrillic.Addchar('Ғ', "Gh", "kk");
            cyrillic.Addchar('ғ', "gh");
            cyrillic.Addchar('Ə', "Ä", "kk", true);
            cyrillic.Addchar('ə', "ä", "kk", true);
            cyrillic.Addchar('İ', "I", "kk", true);
            cyrillic.Addchar('і', "i", "kk", true);
            cyrillic.Addchar('Қ', "Q", "kk");
            cyrillic.Addchar('қ', "q", "kk");
            cyrillic.Addchar('қ', "q");
            cyrillic.Addchar('Ң', "Ng", "kk");
            cyrillic.Addchar('ң', "ng", "kk");
            cyrillic.Addchar('Ө', "Ö", "kk", true);
            cyrillic.Addchar('ө', "ö", "kk", true);
            cyrillic.Addchar('Ү', "Ü", "kk", true);
            cyrillic.Addchar('ү', "ü", "kk", true);
            cyrillic.Addchar('Ұ', "U", "kk", true);
            cyrillic.Addchar('ұ', "u", "kk", true);
            cyrillic.Addchar('Һ', "H", "kk");
            cyrillic.Addchar('һ', "h", "kk");
            cyrillic.Addchar('Ң', "Ng", "ky");
            cyrillic.Addchar('ң', "ng", "ky");
            cyrillic.Addchar('Ө', "Ö", "ky", true);
            cyrillic.Addchar('ө', "ö", "ky", true);
            cyrillic.Addchar('Ү', "Ü", "ky", true);
            cyrillic.Addchar('ү', "ü", "ky", true);
            cyrillic.Addchar('γ', "ü", true);
            cyrillic.Addchar('Ғ', "Gh", "tg");
            cyrillic.Addchar('ғ', "gh", "tg");
            cyrillic.Addchar('Ӣ', "Y", "tg", true);
            cyrillic.Addchar('ӣ', "y", "tg", true);
            cyrillic.Addchar('Қ', "Q", "tg");
            cyrillic.Addchar('қ', "q", "tg");
            cyrillic.Addchar('Ӯ', "Ö", "tg", true);
            cyrillic.Addchar('ӯ', "ö", "tg", true);
            cyrillic.Addchar('Ҳ', "H", "tg");
            cyrillic.Addchar('ҳ', "h", "tg");
            cyrillic.Addchar('Ҷ', "Dzj", "tg");
            cyrillic.Addchar('ҷ', "dzj", "tg");
            cyrillic.Addchar('ж', "j", "mn");
            cyrillic.Addchar('Ж', "J", "mn");
            cyrillic.Addchar('З', "Dz", "mn");
            cyrillic.Addchar('з', "dz", "mn");
            cyrillic.Addchar('Ы', "Ij", "mn");
            cyrillic.Addchar('ы', "ij", "mn");
            cyrillic.Addchar('Ө', "Ö", "mn", true);
            cyrillic.Addchar('ө', "ö", "mn", true);
            cyrillic.Addchar('Ү', "Ü", "mn", true);
            cyrillic.Addchar('ү', "ü", "mn", true);//
            cyrillic.Addchar('ј', "j");
            cyrillic.Addchar('Ј', "J");
            cyrillic.Addchar('ј', "j");
            cyrillic.Addchar('ј', "j");
            cyrillic.Addchar('ј', "j");

        }

        public static int LevenshteinDistance(string src, string dest)
        {
            //From http://www.codeproject.com/Articles/36869/Fuzzy-Search
            //License CPOL (http://www.codeproject.com/info/cpol10.aspx)

            int[,] d = new int[src.Length + 1, dest.Length + 1];
            int i, j, cost;
            char[] str1 = src.ToCharArray();
            char[] str2 = dest.ToCharArray();

            for (i = 0; i <= str1.Length; i++)
            {
                d[i, 0] = i;
            }
            for (j = 0; j <= str2.Length; j++)
            {
                d[0, j] = j;
            }
            for (i = 1; i <= str1.Length; i++)
            {
                for (j = 1; j <= str2.Length; j++)
                {

                    if (str1[i - 1] == str2[j - 1])
                        cost = 0;
                    else
                        cost = 1;

                    d[i, j] =
                        Math.Min(
                            d[i - 1, j] + 1,              // Deletion
                            Math.Min(
                                d[i, j - 1] + 1,          // Insertion
                                d[i - 1, j - 1] + cost)); // Substitution

                    if ((i > 1) && (j > 1) && (str1[i - 1] ==
                        str2[j - 2]) && (str1[i - 2] == str2[j - 1]))
                    {
                        d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + cost);
                    }
                }
            }

            return d[str1.Length, str2.Length];
        }

        public static void test_levenshtein()
        {
            while (true)
            {
                Console.Write("string 1:");
                string s1 = Console.ReadLine();
                Console.Write("string 2:");
                string s2 = Console.ReadLine();
                int dist = LevenshteinDistance(s1, s2);
                Console.WriteLine("Distance = " + dist.ToString());

            }
        }

        public class hbookclass
        {
            private SortedDictionary<string, int> shist = new SortedDictionary<string, int>();
            private SortedDictionary<int, int> ihist = new SortedDictionary<int, int>();
            private SortedDictionary<double, int> dhist = new SortedDictionary<double, int>();

            private const int MAXBINS = 202;
            private double[] binlimits = new double[MAXBINS];
            private double binmax = 100;
            private double binmin = 0;
            private double binwid = 0;
            private int nbins = MAXBINS - 2;

            public void Add(string key)
            {
                if (!shist.ContainsKey(key))
                    shist.Add(key, 1);
                else
                    shist[key]++;
            }

            public void Add(char key)
            {

                if (!shist.ContainsKey(key.ToString()))
                    shist.Add(key.ToString(), 1);
                else
                    shist[key.ToString()]++;
            }

            public void Add(int key)
            {
                if (!ihist.ContainsKey(key))
                    ihist.Add(key, 1);
                else
                    ihist[key]++;
            }

            private int valuetobin(double key)
            {
                int bin = 0;
                if (key > binmin)
                {
                    if (key > binmax)
                        bin = nbins + 1;
                    else
                    {
                        bin = (int)((key - binmin) / binwid) + 1;
                    }
                }
                return bin;
            }

            private double bintomin(int bin)
            {
                if (bin == 0)
                    return binmin;
                if (bin > nbins)
                    return binmax;
                return binmin + (bin - 1) * binwid;
            }

            private double bintomax(int bin)
            {
                if (bin == 0)
                    return binmin;
                if (bin > nbins)
                    return binmax;
                return binmin + bin * binwid;
            }

            public void Add(double key)
            {
                int bin = valuetobin(key);
                if (!ihist.ContainsKey(bin))
                    ihist.Add(bin, 1);
                else
                    ihist[bin]++;
            }

            public void SetBins(double min, double max, int nb)
            {
                if (nbins > MAXBINS - 2)
                {
                    Console.WriteLine("Too many bins. Max " + (MAXBINS - 2).ToString());
                    return;
                }
                else
                {
                    binmax = max;
                    binmin = min;
                    nbins = nb;
                    binwid = (max - min) / nbins;
                    binlimits[0] = binmin;
                    for (int i = 1; i <= nbins; i++)
                    {
                        binlimits[i] = binmin + i * binwid;
                    }

                    for (int i = 0; i <= nbins + 1; i++)
                        if (!ihist.ContainsKey(i))
                            ihist.Add(i, 0);
                }
            }

            public void PrintIHist()
            {
                int total = 0;
                foreach (int key in ihist.Keys)
                {
                    Console.WriteLine(key + ": " + ihist[key].ToString());
                    total += ihist[key];
                }
                Console.WriteLine("----Total : " + total.ToString());
            }

            public void PrintDHist()
            {
                int total = 0;
                foreach (int key in ihist.Keys)
                {
                    Console.WriteLine(bintomin(key).ToString() + " -- " + bintomax(key).ToString() + ": " + ihist[key].ToString());
                    total += ihist[key];
                }
                Console.WriteLine("----Total : " + total.ToString());
            }

            public void PrintSHist()
            {
                int total = 0;
                foreach (string key in shist.Keys)
                {
                    Console.WriteLine(key + ": " + shist[key].ToString());
                    total += shist[key];
                }
                Console.WriteLine("----Total : " + total.ToString());
            }
        }


        public static hbookclass fchist = new hbookclass();
        public static hbookclass fcbad = new hbookclass();
        public static hbookclass fcathist = new hbookclass();
        public static hbookclass fclasshist = new hbookclass();
        public static hbookclass evarhist = new hbookclass();
        public static hbookclass slope1hist = new hbookclass();
        public static hbookclass slope5hist = new hbookclass();
        public static hbookclass slopermshist = new hbookclass();
        public static hbookclass ndirhist = new hbookclass();
        public static hbookclass nsameterrhist = new hbookclass();
        public static hbookclass terrainhist = new hbookclass();
        public static hbookclass terraintexthist = new hbookclass();
        public static hbookclass elevdiffhist = new hbookclass();
        public static hbookclass nwdhist = new hbookclass();
        public static hbookclass foverrephist = new hbookclass();


        public class statclass
        {
            public List<int> sizelist = new List<int>();
            public int nart = 0;
            public int nredirect = 0;
            public int ncat = 0;
            public int ndonecat = 0;
            public int nbot = 0;
            public int ntalk = 0;
            public int nskip = 0;
            public int milestone = 0;
            public int milestone_interval = 1000;
            public bool skipmilestone = false;
            public int ntowait = 0;
            public int nwaited = 0;

            public void ClearStat()
            {
                sizelist.Clear();
                nart = 0;
                nredirect = 0;
                ncat = 0;
                nbot = 0;
                ntalk = 0;
                ndonecat = 0;
                nskip = 0;
            }

            public int ArticleCount(Site countsite)
            {


                //string xmlSrc = countsite.PostDataAndGetResultHTM(countsite.site + "/w/api.php", "action=query&format=xml&meta=siteinfo&siprop=statistics");
                string xmlSrc = countsite.PostDataAndGetResult(countsite.address + "/w/api.php", "action=query&format=xml&meta=siteinfo&siprop=statistics");

                //Console.WriteLine(xmlSrc);


                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlSrc);
                string ts = doc.GetElementsByTagName("statistics")[0].Attributes.GetNamedItem("articles").Value;

                Console.WriteLine("ts = " + ts);

                return Convert.ToInt32(ts);

            }

            public void SetMilestone(int mint, Site countsite)
            {
                milestone_interval = mint;

                int ac = ArticleCount(countsite);

                milestone = ((ac / milestone_interval) + 1) * milestone_interval;

                Console.WriteLine("Articlecount = " + ac.ToString() + ", milestone = " + milestone.ToString());

                if ((milestone - ac) > 100)
                    ntowait = (milestone - ac) / 2;
                else
                    ntowait = 0;

                nwaited = 0;

            }

            public void Addskip()
            {
                nskip++;
            }

            public void Adddonecat()
            {
                ndonecat++;
            }

            public void Add(string title, string text, bool savemilestone)
            {
                if (title.Contains(mp(1)))
                    ncat++;
                else if (title.Contains(botname))
                    nbot++;
                else if (text.Contains(mp(2)))
                    nredirect++;
                else if (title.Contains(mp(38)))
                    ntalk++;
                else if (!title.Contains(":"))
                {
                    nart++;
                    sizelist.Add(text.Length);

                    nwaited++;
                    if (nwaited >= ntowait)
                    {
                        int ac = ArticleCount(makesite);

                        if (ac >= milestone)
                        {
                            Console.WriteLine("Milestone reached: ac = " + ac.ToString());
                            SetMilestone(milestone_interval, makesite);
                            if (savemilestone)
                            {
                                if (pstats == null)
                                {
                                    pstats = new Page(makesite, mp(13) + botname + "/Statistik");
                                    pstats.Load();
                                }
                                pstats.text += "\n\nMilstolpe: artikel #" + ac.ToString() + " är [[" + title + "]]\n\n";
                                trysave(pstats, 3, mp(302,null));
                            }
                        }

                        Console.WriteLine("Articlecount = " + ac.ToString() + ", milestone = " + milestone.ToString());

                        if ((milestone - ac) > 10)
                            ntowait = (milestone - ac) / 2;
                        else if (!skipmilestone || ((milestone - ac) > 1))
                            ntowait = 0;
                        else
                        {
                            while (ac < milestone)
                            {
                                Console.WriteLine("Waiting for milestone...");
                                Thread.Sleep(60000);//milliseconds
                                ac = ArticleCount(makesite);
                            }
                            ntowait = 0;
                        }

                        nwaited = 0;
                    }
                }
            }

            public string GetStat()
            {
                string s = "* Antal artiklar: " + (nart + nskip).ToString() + "\n";

                //int sum = 0;

                SortedDictionary<int, int> hist = new SortedDictionary<int, int>();

                int isum = 0;
                int mean = 0;
                foreach (int i in sizelist)
                {
                    isum += i;
                    if (hist.ContainsKey(i))
                        hist[i]++;
                    else
                        hist.Add(i, 1);
                }

                if (nart > 0)
                    mean = isum / nart;
                else
                {
                    return s;
                }

                int icum = 0;
                int median = 0;
                foreach (int i in hist.Keys)
                {
                    icum += hist[i];
                    if (icum >= (nart / 2))
                    {
                        median = i;
                        break;
                    }
                }

                s += "** Medellängd: " + mean.ToString() + " bytes\n";
                s += "** Medianlängd: " + median.ToString() + " bytes\n";

                if (nskip == 0)
                    s += "* Antal kategorier: " + ncat.ToString() + "\n";
                else
                    s += "* Antal kategorier: " + ndonecat.ToString() + "\n";
                s += "* Antal omdirigeringar: " + nredirect.ToString() + "\n";
                s += "* Antal diskussionssidor: " + ntalk.ToString() + "\n";
                s += "* Antal anomalier: " + nbot.ToString() + "\n";
                s += "\n";

                return s;
            }

        }

        public static statclass stats = new statclass();

        public static string getgnidname(int gnid)
        {
            if (!gndict.ContainsKey(gnid))
                return null;
            else
                return gndict[gnid].Name_ml;

        }

        public static string getartname(int gnid)
        {
            if (!gndict.ContainsKey(gnid))
                return null;
            else
                return gndict[gnid].articlename.Replace("*", "");

        }

        public static string fixcase(string ss)
        {
            string s = String.Copy(ss);
            for (int i = 1; i < s.Length; i++)
            {
                if ((s[i - 1] != ' ') && (s[i - 1] != '.'))
                {
                    s = s.Remove(i, 1).Insert(i, Char.ToLower(s[i]).ToString());
                }
            }
            return s;
        }


        public static void read_phrases()
        {
            using (StreamReader sr = new StreamReader(geonamesfolder + "phraselist.txt"))
            {

                String headline = "";
                headline = sr.ReadLine();

                int icol = 0;
                string[] langs = headline.Split(tabchar);
                for (icol = 0; icol < langs.Length; icol++)
                {
                    if (langs[icol] == makelang)
                    {
                        break;
                    }
                }

                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();
                    //Console.WriteLine(line);

                    string[] words = line.Split(tabchar);
                    if (words.Length < icol + 1)
                        continue;
                    //for (int jj = 1; jj < words.Length; jj++)
                    //{
                    //    words[jj] = words[jj].Trim();
                    //}
                    int ip = tryconvert(words[0]);
                    phrases.Add(ip, words[icol]);
                }
            }

        }

        public static string mp(int np)
        {
            return mp(np, null);
        }

        public static string mp(int np, string[] param)
        {
            if (phrases.Count == 0)
                read_phrases();

            int ip = 0;
            string sret = phrases[np];
            if (param != null)
                foreach (string s in param)
                {
                    ip++;
                    sret = sret.Replace("#" + ip.ToString(), s);
                }

            return sret;
        }

        public static string ReplaceOne(string textparam, string oldt, string newt, int position) //Replace ONE occurrence of oldt in textparam, the first one after position
        {
            string text = textparam;
            int oldpos = text.IndexOf(oldt, position);
            if (oldpos < 0)
                return text;
            text = text.Remove(oldpos, oldt.Length);
            text = text.Insert(oldpos, newt);

            return text;
        }

        public static List<int> IndexOfAll(string text, string find) //Return a list with ALL occurrences of find in text
        {
            int start = 0;
            int pos = 0;
            List<int> rl = new List<int>();
            do
            {
                pos = text.IndexOf(find, start);
                if (pos >= 0)
                {
                    start = pos + find.Length;
                    rl.Add(pos);
                }
            }
            while (pos >= 0);

            return rl;
        }

        public static void make_redirect(string frompage, string topage)
        {
            make_redirect(frompage, topage, "", -1);
        }

        public static void make_redirect(string frompage, string topage, string cat, int ilang)
        {
            if (String.IsNullOrEmpty(frompage))
                return;
            if (String.IsNullOrEmpty(topage))
                return;

            Page pred = new Page(makesite, frompage);
            if (tryload(pred, 1))
            {
                if (!pred.Exists())
                {
                    pred.text = "#" + mp(2) + " [[" + topage + "]]\n";

                    if (makelang == "sv")
                    {
                        if (langdict.ContainsKey(ilang))
                            pred.text += "{{Omdirigering på annat språk|" + langdict[ilang].iso3 + "}}\n";
                        if (!is_latin(remove_disambig(pred.title)))
                        {
                            string alph_sv = get_alphabet_sv(get_alphabet(remove_disambig(pred.title)));
                            Console.WriteLine(alph_sv);
                            if (!alph_sv.Contains("okänd"))
                                pred.text += "{{Sidnamn annan skrift|" + alph_sv + "}}\n";
                            else
                            {
                                Console.WriteLine(pred.title);
                                Console.WriteLine(remove_disambig(pred.title));
                                Console.WriteLine(alph_sv);
                                Console.ReadLine();
                            }

                        }

                    }
                    if (!String.IsNullOrEmpty(cat))
                        pred.AddToCategory(cat);
                    trysave(pred, 2,mp(60) + " " + countryml[makecountryname] + " " + mp(2).ToLower());
                }

            }

        }

        public static void make_redirect_override(Page pred, string topage, string cat, int ilang)
        {

            pred.text = "#" + mp(2) + " [[" + topage + "]]\n";

            if (makelang == "sv")
            {
                if (langdict.ContainsKey(ilang))
                    pred.text += "{{Omdirigering på annat språk|" + langdict[ilang].iso3 + "}}\n";
                if (!is_latin(remove_disambig(pred.title)))
                {
                    string alph_sv = get_alphabet_sv(get_alphabet(remove_disambig(pred.title)));
                    if (!alph_sv.Contains("okänd"))
                        pred.text += "{{Sidnamn annan skrift|" + alph_sv + "}}\n";
                    else
                    {
                        Console.WriteLine(pred.title);
                        Console.WriteLine(remove_disambig(pred.title));
                        Console.WriteLine(alph_sv);
                        Console.ReadLine();
                    }
                }

            }
            if (!String.IsNullOrEmpty(cat))
                pred.AddToCategory(cat);

            trysave(pred, 2,mp(303) + " " + countryml[makecountryname] + " " + mp(2).ToLower());

        }

        public static void romanian_redirect(string topage)
        {
            string frompage = topage;
            Dictionary<char, char> romchars = new Dictionary<char, char>();
            romchars.Add('ş', 'ș');
            romchars.Add('ţ', 'ț');

            foreach (char c in romchars.Keys)
            {
                frompage = frompage.Replace(c, romchars[c]);
            }

            if (frompage != topage)
                make_redirect(frompage, topage, "", -1);

            frompage = topage;
            foreach (char c in romchars.Keys)
            {
                frompage = frompage.Replace(romchars[c], c);
            }

            if (frompage != topage)
                make_redirect(frompage, topage, "", -1);

        }

        public static string initialcap(string orig)
        {
            if (String.IsNullOrEmpty(orig))
                return "";

            int initialpos = 0;
            if (orig.IndexOf('|') > 0)
                initialpos = orig.IndexOf('|') + 1;
            else if (orig.IndexOf("[[") >= 0)
                initialpos = orig.IndexOf("[[") + 2;
            string s = orig.Substring(initialpos, 1);
            s = s.ToUpper();
            string final = orig;
            final = final.Remove(initialpos, 1).Insert(initialpos, s);
            //s += orig.Remove(0, 1);
            return final;
        }

        public static bool tryload(Page p, int iattempt)
        {
            int itry = 1;

            if (!reallymake)
            {
                Console.WriteLine("NOT loading " + p.title);
                return true;
            }

            if (String.IsNullOrEmpty(p.title))
                return false;

            while (true)
            {

                try
                {
                    p.Load();
                    return true;
                }
                catch (WebException e)
                {
                    string message = e.Message;
                    Console.Error.WriteLine("tl we " + message);
                    itry++;
                    if (itry > iattempt)
                        return false;
                    else
                        Thread.Sleep(600000);//milliseconds
                }
                catch (NullReferenceException e)
                {
                    string message = e.Message;
                    Console.Error.WriteLine("tl ne " + message);
                    itry++;
                    if (itry > iattempt)
                        return false;
                    else
                        Thread.Sleep(6000);//milliseconds
                }
            }

        }

        public static bool trysave(Page p, int iattempt)
        {
            return trysave(p, iattempt, makesite.defaultEditComment);
        }

        public static bool trysave(Page p, int iattempt,string editcomment)
        {
            int itry = 1;

            if (!reallymake)
                return true;

            while (true)
            {

                try
                {
                    //Bot.editComment = mp(60);
                    p.Save(editcomment, false);
                    stats.Add(p.title, p.text, (makearticles || makefork));
                    DateTime newtime = DateTime.Now;
                    while (newtime < oldtime)
                        newtime = DateTime.Now;
                    oldtime = newtime.AddSeconds(pausetime);

                    if (pauseaftersave)
                    {
                        Console.WriteLine("<ret>");
                        Console.ReadKey();
                    }
                    return true;
                }
                catch (WebException e)
                {
                    string message = e.Message;
                    Console.Error.WriteLine("ts we " + message);
                    itry++;
                    if (itry > iattempt)
                        return false;
                    else
                        Thread.Sleep(600000);//milliseconds
                }
                catch (WikiBotException e)
                {
                    string message = e.Message;
                    Console.Error.WriteLine("ts wbe " + message);
                    if (message.Contains("Bad title"))
                        return false;
                    itry++;
                    if (itry > iattempt)
                        return false;
                    else
                        Thread.Sleep(600000);//milliseconds
                }
                catch (IOException e)
                {
                    string message = e.Message;
                    Console.Error.WriteLine("ts ioe " + message);
                    itry++;
                    if (itry > iattempt)
                        return false;
                    else
                        Thread.Sleep(600000);//milliseconds
                }
            }

        }

        public static int tryconvert(string word)
        {
            int i = -1;

            if (word.Length == 0)
                return i;

            try
            {
                i = Convert.ToInt32(word);
            }
            catch (OverflowException)
            {
                Console.WriteLine("i Outside the range of the Int32 type: " + word);
            }
            catch (FormatException)
            {
                //if ( !String.IsNullOrEmpty(word))
                //    Console.WriteLine("i Not in a recognizable format: " + word);
            }

            return i;

        }

        public static long tryconvertlong(string word)
        {
            long i = -1;

            if (word.Length == 0)
                return i;

            try
            {
                i = Convert.ToInt64(word);
            }
            catch (OverflowException)
            {
                Console.WriteLine("i Outside the range of the Int64 type: " + word);
            }
            catch (FormatException)
            {
                //Console.WriteLine("i Not in a recognizable long format: " + word);
            }

            return i;

        }

        public static double tryconvertdouble(string word)
        {
            double i = -1;

            if (word.Length == 0)
                return i;

            try
            {
                i = Convert.ToDouble(word);
            }
            catch (OverflowException)
            {
                Console.WriteLine("i Outside the range of the Double type: " + word);
            }
            catch (FormatException)
            {
                try
                {
                    i = Convert.ToDouble(word.Replace(".", ","));
                }
                catch (FormatException)
                {
                    //Console.WriteLine("i Not in a recognizable double format: " + word.Replace(".", ","));
                }
                //Console.WriteLine("i Not in a recognizable double format: " + word);
            }

            return i;

        }

        public static void fill_propdict()
        {
            propdict.Add("country", 17);
            propdict.Add("capital", 36);
            propdict.Add("commonscat", 373);
            propdict.Add("coat of arms", 94);
            propdict.Add("locatormap", 242);
            propdict.Add("flag", 41);
            propdict.Add("timezone", 421);
            propdict.Add("kids", 150);
            propdict.Add("parent", 131);
            propdict.Add("iso", 300);
            propdict.Add("borders", 47);
            propdict.Add("coordinates", 625);
            propdict.Add("inception", 571);
            propdict.Add("head of government", 6);
            propdict.Add("gnid", 1566);
            propdict.Add("follows", 155);
            propdict.Add("category dead", 1465);
            propdict.Add("category born", 1464);
            propdict.Add("category from", 1792);
            propdict.Add("image", 18);
            propdict.Add("banner", 948);
            //propdict.Add("sister city",190);
            propdict.Add("postal code", 281);
            propdict.Add("position", 625);
            propdict.Add("population", 1082);
            propdict.Add("instance", 31);
            propdict.Add("subclass", 279);
            propdict.Add("nexttowater", 206);

            //propdict.Add("",);

        }

        public static void read_blacklist()
        {
            Page pblack = new Page(makesite, mp(13) + botname + "/svartlista");
            tryload(pblack, 2);
            foreach (string s in pblack.text.Split('\n'))
            {
                if (tryconvert(s) > 0)
                    blacklist.Add(tryconvert(s));
            }
        }

        public static void read_adm1()
        {
            int n = 0;



            using (StreamReader sr = new StreamReader(geonamesfolder + "admin1CodesASCII.txt"))
            {
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();

                    if (line[0] == '#')
                        continue;

                    //if (n > 250)
                    //    Console.WriteLine(line);

                    string[] words = line.Split(tabchar);

                    //foreach (string s in words)
                    //    Console.WriteLine(s);

                    //Console.WriteLine(words[0] + "|" + words[1]);

                    //    public static Dictionary<string,Dictionary<string,int>> adm1dict = new Dictionary<string,Dictionary<string,int>>();


                    int geonameid = -1;

                    geonameid = tryconvert(words[3]);

                    string[] ww = words[0].Split('.');
                    if (adm1dict.ContainsKey(ww[0]))
                        adm1dict[ww[0]].Add(ww[1], geonameid);
                    else
                    {
                        Dictionary<string, int> dd = new Dictionary<string, int>();
                        dd.Add(ww[1], geonameid);
                        adm1dict.Add(ww[0], dd);
                    }

                    if (ww[0] == makecountry)
                    {
                        Console.WriteLine("adm1:" + words[0] + ":" + geonameid.ToString());
                    }


                    n++;
                    if ((n % 1000) == 0)
                    {
                        Console.WriteLine("n (adm1)   = " + n.ToString());

                    }

                }

                Console.WriteLine("n    (adm1)= " + n.ToString());
                //Console.WriteLine("<cr>");
                //Console.ReadLine();

            }
        }

        public static void read_adm2()
        {
            int n = 0;

            using (StreamReader sr = new StreamReader(geonamesfolder + "admin2Codes.txt"))
            {
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();

                    if (line[0] == '#')
                        continue;

                    //if (n > 250)
                    //    Console.WriteLine(line);

                    string[] words = line.Split(tabchar);

                    //foreach (string s in words)
                    //    Console.WriteLine(s);

                    //Console.WriteLine(words[0] + "|" + words[1]);

                    //    public static Dictionary<string,Dictionary<string,int>> adm1dict = new Dictionary<string,Dictionary<string,int>>();

                    countryclass country = new countryclass();

                    int geonameid = -1;

                    geonameid = tryconvert(words[3]);

                    string[] ww = words[0].Split('.');
                    if (!adm2dict.ContainsKey(ww[0]))
                    {
                        Dictionary<string, Dictionary<string, int>> dd = new Dictionary<string, Dictionary<string, int>>();
                        adm2dict.Add(ww[0], dd);
                    }

                    if (adm2dict[ww[0]].ContainsKey(ww[1]))
                        adm2dict[ww[0]][ww[1]].Add(ww[2], geonameid);
                    else
                    {
                        Dictionary<string, int> ddd = new Dictionary<string, int>();
                        ddd.Add(ww[2], geonameid);
                        adm2dict[ww[0]].Add(ww[1], ddd);
                    }


                    n++;
                    if ((n % 10000) == 0)
                    {
                        Console.WriteLine("n (adm2)   = " + n.ToString());

                    }

                }

                Console.WriteLine("n    (adm2)= " + n.ToString());

            }
        }

        public static void read_timezone()
        {
            int n = 0;

            tzdict.Clear();
            tznamedict.Clear();
            tzsummerdict.Clear();
            tzfulldict.Clear();
            tzfullsummerdict.Clear();

            string filename = "timezonenames.txt";

            //first look if timezone name file exists for specific country...
            filename = "timezonenames-" + makecountry + ".txt";
            //Console.WriteLine(filename);

            //... then for continent...
            if (!File.Exists(geonamesfolder + filename))
                if ((!String.IsNullOrEmpty(makecountry)) && countryid.ContainsKey(makecountry))
                    filename = "timezonenames-" + countrydict[countryid[makecountry]].continent + ".txt";

            //...otherwise default names.
            if (!File.Exists(geonamesfolder + filename))
                filename = "timezonenames.txt";

            Console.WriteLine(filename);
            //Console.ReadLine();

            using (StreamReader sr = new StreamReader(geonamesfolder + filename))
            {
                //String line = sr.ReadLine();
                //line = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();
                    //Console.WriteLine(line);
                    n++;
                    string[] words = line.Split(tabchar);

                    if (!String.IsNullOrEmpty(words[1]))
                        tznamedict.Add(words[0], words[1]);
                    if (!String.IsNullOrEmpty(words[2]))
                        tzsummerdict.Add(words[0], words[2]);
                    if (!String.IsNullOrEmpty(words[3]))
                        tzfulldict.Add(words[0], words[3]);
                    if ((words.Length > 4) && (!String.IsNullOrEmpty(words[4])))
                        tzfullsummerdict.Add(words[0], words[4]);
                }
            }

            n = 0;
            Console.WriteLine("tznamedict.Count = " + tznamedict.Count);
            //Console.ReadLine();

            if (savewikilinks)
            {
                Page pt = new Page(makesite, mp(13) + botname + "/timezonelinks");
                pt.text = "Timezone links used by Lsjbot\n\n";
                foreach (string tz in tznamedict.Keys)
                    pt.text += "* UTC" + tz + " [[" + tzfulldict[tz] + "|" + tznamedict[tz] + "]]\n";
                foreach (string tz in tzsummerdict.Keys)
                    pt.text += "* UTC" + tz + " [[" + tzfullsummerdict[tz] + "|" + tzsummerdict[tz] + "]]\n";
                trysave(pt, 1,"Bot saving timezonelinks");
            }

            using (StreamReader sr = new StreamReader(geonamesfolder + "timeZones.txt"))
            {
                String line = sr.ReadLine();
                line = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();


                    //if (n > 250)
                    //    Console.WriteLine(line);

                    string[] words = line.Split(tabchar);

                    //foreach (string s in words)
                    //    Console.WriteLine(s);

                    //Console.WriteLine(words[0] + "|" + words[1]);

                    //    public static Dictionary<string,Dictionary<string,int>> adm1dict = new Dictionary<string,Dictionary<string,int>>();

                    tzclass tzz = new tzclass();

                    for (int ii = 2; ii < 5; ii++)
                    {
                        if (!words[ii].Contains("+") && !words[ii].Contains("-"))
                            words[ii] = "+" + words[ii];
                        words[ii] = words[ii].Replace(".0", "");
                        words[ii] = words[ii].Replace(".5", ":30");
                        words[ii] = words[ii].Replace(".75", ":45");

                    }

                    tzz.offset = words[2];
                    tzz.summeroffset = words[3];
                    tzz.rawoffset = words[4];


                    if (tznamedict.ContainsKey(tzz.offset))
                        tzz.tzname = tznamedict[tzz.offset];
                    else
                        Console.WriteLine("No tzname for |" + tzz.offset + "|");
                    if (tzfulldict.ContainsKey(tzz.offset))
                        tzz.tzfull = tzfulldict[tzz.offset];

                    if ((tzz.summeroffset != tzz.offset) && (tzsummerdict.ContainsKey(tzz.summeroffset)))
                    {
                        tzz.tzsummer = tzsummerdict[tzz.summeroffset];
                        tzz.tzfullsummer = tzfullsummerdict[tzz.summeroffset];
                    }

                    tzdict.Add(words[1], tzz);

                    n++;
                    if ((n % 1000) == 0)
                    {
                        Console.WriteLine("n (timezone)   = " + n.ToString());

                    }

                }

                Console.WriteLine("n    (timezone)= " + n.ToString());
                //Console.ReadLine();

            }
        }

        public static void addnamefork(int geonameid, string Name)
        {
            string nn = Name.Trim();
            if (String.IsNullOrEmpty(nn))
                return;

            //Skip numbers and acronyms:
            if (tryconvert(Name) > 0)
                return;
            if ((Name.Length <= 3) && (Name == Name.ToUpper()))
                return;


            if (namefork.ContainsKey(Name))
            {
                if (!namefork[Name].Contains(geonameid))
                    namefork[Name].Add(geonameid);
            }
            else
            {
                List<int> ll = new List<int>();
                ll.Add(geonameid);
                namefork.Add(Name, ll);
            }
        }

        public static void read_existing_coord()
        {
            int n = 0;
            using (StreamReader sr = new StreamReader(geonamesfolder + "coord-" + makelang + ".txt"))
            {
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();
                    //Console.WriteLine(line);
                    n++;
                    string[] words = line.Split(tabchar);

                    if (words.Length < 3)
                        continue;

                    existingclass ex = new existingclass();

                    ex.articlename = words[0];
                    ex.latitude = tryconvertdouble(words[1]);
                    ex.longitude = tryconvertdouble(words[2]);
                    existingdict.Add(n, ex);
                    addexistinglatlong(ex.latitude, ex.longitude, n);
                }
            }
        }

        public static void read_existing_adm1()
        {
            //public static Dictionary<string, List<string>> existing_adm1 = new Dictionary<string, List<string>>();
            PageList pl = new PageList(makesite);
            pl.FillAllFromCategory("Kategori:Regionala politiska indelningar");

            int n = 0;
            foreach (Page p in pl)
            {
                string tit = remove_disambig(p.title.Replace("Kategori:", ""));

                string country = "";
                if (tit.Contains("s "))
                    country = tit.Substring(0, tit.IndexOf("s "));
                else
                    continue;

                PageList pl1 = new PageList(makesite);
                pl1.FillAllFromCategory(p.title);
                foreach (Page p1 in pl1)
                {
                    string tit1 = p1.title.Replace("Kategori:", "");
                    if (!existing_adm1.ContainsKey(country))
                    {
                        List<string> ll = new List<string>();
                        existing_adm1.Add(country, ll);
                        Console.WriteLine("country = " + country);
                    }
                    existing_adm1[country].Add(tit1);
                    n++;
                }
            }
            Console.WriteLine("n(adm1) = " + n.ToString());

        }

        public static void addexistinglatlong(double lat, double lon, int exid)
        {
            int ilat = Convert.ToInt32(Math.Truncate(lat));
            int ilong = Convert.ToInt32(Math.Truncate(lon));

            if (!exlatlong.ContainsKey(ilat))
            {
                Dictionary<int, List<int>> dd = new Dictionary<int, List<int>>();
                exlatlong.Add(ilat, dd);
            }
            if (!exlatlong[ilat].ContainsKey(ilong))
            {
                List<int> ll = new List<int>();
                exlatlong[ilat].Add(ilong, ll);
            }
            if (!exlatlong[ilat][ilong].Contains(exid))
                exlatlong[ilat][ilong].Add(exid);
        }

        public static List<int> getexisting(double lat, double lon, double radius)
        {
            List<int> ll = new List<int>();
            double kmdeg = 40000 / 360; //km per degree at equator
            double r2 = radius * radius / (kmdeg * kmdeg);
            double scale = Math.Cos(lat * 3.1416 / 180); //latitude-dependent longitude scale

            int ilat = Convert.ToInt32(Math.Truncate(lat));
            int ilong = Convert.ToInt32(Math.Truncate(lon));

            int cells = Convert.ToInt32(radius / kmdeg + 1);
            for (int u = -cells; u < (cells + 1); u++)
                for (int v = -cells; v < (cells + 1); v++)
                {
                    if (exlatlong.ContainsKey(ilat + u))
                        if (exlatlong[ilat + u].ContainsKey(ilong + v))
                            foreach (int gnn in exlatlong[ilat + u][ilong + v])
                            {
                                if (!existingdict.ContainsKey(gnn))
                                    continue;
                                //if ((existingdict[gnn].latitude == lat) && (existingdict[gnn].longitude == lon))
                                //    continue;
                                double dlat = existingdict[gnn].latitude - lat;
                                double dlon = (existingdict[gnn].longitude - lon) * scale;
                                if ((dlat * dlat + dlon * dlon) < r2)
                                    ll.Add(gnn);
                            }
                }
            return ll;
        }

        public static void addlatlong(double lat, double lon, int gnid)
        {
            int ilat = Convert.ToInt32(Math.Truncate(lat));
            int ilong = Convert.ToInt32(Math.Truncate(lon));

            if (!latlong.ContainsKey(ilat))
            {
                Dictionary<int, List<int>> dd = new Dictionary<int, List<int>>();
                latlong.Add(ilat, dd);
            }
            if (!latlong[ilat].ContainsKey(ilong))
            {
                List<int> ll = new List<int>();
                latlong[ilat].Add(ilong, ll);
            }
            if (!latlong[ilat][ilong].Contains(gnid))
                latlong[ilat][ilong].Add(gnid);
        }

        public static bool getghostneighbors(double lat, double lon, double radius)
        {
            double kmdeg = 40000 / 360; //km per degree at equator
            double r2 = radius * radius / (kmdeg * kmdeg);
            double scale = Math.Cos(lat * 3.1416 / 180); //latitude-dependent longitude scale

            foreach (int gnn in ghostdict.Keys)
            {
                double dlat = ghostdict[gnn].latitude - lat;
                double dlon = (ghostdict[gnn].longitude - lon) * scale;
                if ((dlat * dlat + dlon * dlon) < r2)
                    return true;
            }

            return false;
        }

        public static List<int> getneighbors(double lat, double lon, double radius)
        {
            List<int> ll = new List<int>();
            double kmdeg = 40000 / 360; //km per degree at equator
            double r2 = radius * radius / (kmdeg * kmdeg);
            double scale = Math.Cos(lat * 3.1416 / 180); //latitude-dependent longitude scale

            int ilat = Convert.ToInt32(Math.Truncate(lat));
            int ilong = Convert.ToInt32(Math.Truncate(lon));

            int cells = Convert.ToInt32(radius / kmdeg + 1);
            for (int u = -cells; u < (cells + 1); u++)
                for (int v = -cells; v < (cells + 1); v++)
                {
                    if (latlong.ContainsKey(ilat + u))
                        if (latlong[ilat + u].ContainsKey(ilong + v))
                            foreach (int gnn in latlong[ilat + u][ilong + v])
                            {
                                if (!gndict.ContainsKey(gnn))
                                    continue;
                                if ((gndict[gnn].latitude == lat) && (gndict[gnn].longitude == lon))
                                    continue;
                                double dlat = gndict[gnn].latitude - lat;
                                double dlon = (gndict[gnn].longitude - lon) * scale;
                                if ((dlat * dlat + dlon * dlon) < r2)
                                    ll.Add(gnn);
                            }
                }
            return ll;
        }

        public static List<int> getneighbors(int gnid, double radius) //radius in km!
        {
            List<int> ll = new List<int>();

            if (!gndict.ContainsKey(gnid))
                return ll;
            double lat = gndict[gnid].latitude;
            double lon = gndict[gnid].longitude;
            ll = getneighbors(lat, lon, radius);
            return ll;
        }

        public static void read_geoboxes()
        {
            int n = 0;

            foreach (string geotype in geoboxlist)
            {
                string filename = geonamesfolder + "geobox-" + geotype + "-" + makelang + ".txt";

                using (StreamReader sr = new StreamReader(filename))
                {
                    geoboxtemplates.Add(geotype, sr.ReadToEnd());
                    n++;
                }
            }

            Console.WriteLine("Read " + n.ToString() + " geoboxes.");

        }


        public static void read_categories()
        {
            int n = 0;

            string filename = geonamesfolder + "categories.txt";

            using (StreamReader sr = new StreamReader(filename))
            {
                String headline = "";
                headline = sr.ReadLine();

                int icol = 0;
                string[] langs = headline.Split(tabchar);
                for (icol = 0; icol < langs.Length; icol++)
                {
                    if (langs[icol] == makelang)
                    {
                        break;
                    }
                }

                //public static Dictionary<string, string> parentcategory = new Dictionary<string, string>(); //from category to parent category
                //public static Dictionary<string, string> categoryml = new Dictionary<string, string>(); //from category to category name in makelang


                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();

                    string[] words = line.Split(tabchar);

                    if (words.Length < icol + 1)
                        continue;

                    parentcategory.Add(words[0], words[1]);
                    categoryml.Add(words[0], words[icol]);

                    n++;
                    if ((n % 100) == 0)
                    {
                        Console.WriteLine("n (categories)   = " + n.ToString());
                        if (n >= 100000000)
                            break;
                    }

                }

                Console.WriteLine("n    (categories)= " + n.ToString());

            }

        }

        public static void read_catstat()
        {
            int n = 0;
            double nctot = 0;

            string filename = geonamesfolder + "catstat.txt";

            using (StreamReader sr = new StreamReader(filename))
            {

                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();

                    string[] words = line.Split(':');

                    if (words.Length < 2)
                        continue;

                    int nc = tryconvert(words[1].Trim());
                    catstatdict.Add(words[0], nc);
                    nctot += nc;

                    n++;
                    if ((n % 100) == 0)
                    {
                        Console.WriteLine("n (categories)   = " + n.ToString());
                        if (n >= 100000000)
                            break;
                    }

                }

                Console.WriteLine("n    (categories)= " + n.ToString());

            }


            foreach (string s in catstatdict.Keys)
                catnormdict.Add(s, catstatdict[s] / nctot);


        }


        public static void read_featurecodes()
        {
            int n = 0;
            int nbad = 0;

            string filename = geonamesfolder + "featureCodes.txt";
            string lf = "";

            using (StreamReader sr = new StreamReader(filename))
            {
                String headline = "";
                headline = sr.ReadLine();

                int icol = 0;
                string[] langs = headline.Split(tabchar);
                for (icol = 0; icol < langs.Length; icol++)
                {
                    if (langs[icol] == makelang)
                    {
                        break;
                    }
                }

                string oldfc0 = "X";

                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();

                    string[] words = line.Split(tabchar);

                    string[] fc = words[0].Split('.');

                    if (words[1] == "0")
                    {
                        nbad++;
                        continue;
                    }

                    //Console.WriteLine(fc[1]);
                    featuredict.Add(fc[1], words[icol]);

                    char fchar = fc[0].ToCharArray()[0];
                    featureclassdict.Add(fc[1], fchar);

                    string geotype = words[2];
                    if (String.IsNullOrEmpty(geotype))
                        geotype = "alla";
                    geoboxdict.Add(fc[1], geotype);
                    if (!geoboxlist.Contains(geotype))
                        geoboxlist.Add(geotype);

                    string catname = words[3];
                    if (String.IsNullOrEmpty(catname))
                        catname = "landforms";
                    categorydict.Add(fc[1], catname);

                    featurepointdict.Add(fc[1], ((words[4] != "0")&&(words[4] != "3")));
                    minutesensitivedict.Add(fc[1], (words[4] == "2"));
                    if (words[4] == "3")
                        noclimatelist.Add(fc[1]);

                    if (savefeaturelink)
                    {
                        if ((!fc[1].Contains("ADM")) && (!fc[1].Contains("PPLA")))
                        {
                            if (fc[0] != oldfc0)
                            {
                                oldfc0 = fc[0];
                                lf += "\n\n== " + fc[0] + " ==\n\n";
                            }
                            lf += "* " + words[0] + ": " + linkfeature(fc[1], -1);
                            if (words.Length > 8)
                                lf += " (" + words[8] + ")";
                            lf += "\n";
                        }
                    }

                    n++;
                    if ((n % 100) == 0)
                    {
                        Console.WriteLine("n (featurecodes)   = " + n.ToString());
                        if (n >= 100000000)
                            break;
                    }

                }

                Console.WriteLine("n    (featurecodes)= " + n.ToString());
                Console.WriteLine("nbad (featurecodes)= " + nbad.ToString());

                if (savefeaturelink)
                {
                    Console.WriteLine(lf);

                    Page plf = new Page(makesite, mp(13) + botname + "/linkfeatures");
                    plf.text = lf;
                    trysave(plf, 1,"Bot saving linkfeatures");
                    Console.ReadLine();
                }
            }

            read_specialfeatures();
        }

        public static void read_specialfeatures()
        {
            int n = 0;

            string filename = geonamesfolder + "specialfeatures-" + makelang + ".txt";
            if (!File.Exists(filename))
                return;

            using (StreamReader sr = new StreamReader(filename))
            {

                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();

                    string[] words = line.Split(tabchar);

                    if (words.Length < 2)
                        continue;

                    int gnid = tryconvert(words[0]);
                    if (gnid < 0)
                        continue;

                    if (!specialfeaturedict.ContainsKey(gnid))
                        specialfeaturedict.Add(gnid, words[1]);

                    if (words.Length >= 3)
                    {
                        if (!admtodet.ContainsKey(words[1]))
                            admtodet.Add(words[1], words[2]);
                    }

                    n++;
                    if ((n % 100) == 0)
                    {
                        Console.WriteLine("n (specialfeatures)   = " + n.ToString());
                        if (n >= 100000000)
                            break;
                    }

                }

                Console.WriteLine("n    (specialfeatures)= " + n.ToString());

            }

        }



        public static void make_altitude_files()
        {
            elevdiffhist.SetBins(-1000.0, 1000.0, 200);

            using (StreamWriter sw = new StreamWriter("altitude-" + makecountry + ".txt"))
            {

                int ngnid = gndict.Count;

                foreach (int gnid in gndict.Keys)
                {
                    Console.WriteLine("=====" + makecountry + "======== " + ngnid.ToString() + " remaining. ===========");
                    ngnid--;
                    if ((ngnid % 1000) == 0)
                    {
                        Console.WriteLine("Garbage collection:");
                        GC.Collect();
                    }

                    if ((resume_at > 0) && (resume_at != gnid))
                    {
                        stats.Addskip();
                        continue;
                    }
                    else
                        resume_at = -1;


                    int altitude = get_altitude(gnid);

                    Console.WriteLine(gndict[gnid].Name + ", " + gndict[gnid].featureclass + "." + gndict[gnid].featurecode + ": " + altitude.ToString());

                    if (gndict[gnid].elevation > 0)
                        elevdiffhist.Add(1.0 * (gndict[gnid].elevation - altitude));

                    if (altitude != 0)
                        sw.WriteLine(gnid.ToString() + tabstring + altitude.ToString());

                }
            }
            elevdiffhist.PrintDHist();
            //Console.ReadLine();

        }



        public static void read_chinese_pop()
        {
            Console.WriteLine("read_chinese_pop");
            string filepath = geonamesfolder + @"\China population\";
            string filekeyname = filepath + "filekey.txt";
            Dictionary<int, int> filekeys = new Dictionary<int, int>();
            int nkeys = 0;
            using (StreamReader sr = new StreamReader(filekeyname))
            {
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();
                    string[] words = line.Split(tabchar);
                    int fn = tryconvert(words[0]);
                    if (fn > 0)
                    {
                        int gnid = tryconvert(words[2]);
                        if (gnid > 0)
                        {
                            nkeys++;
                            filekeys.Add(fn, gnid);
                        }
                    }
                }
            }
            Console.WriteLine("nkeys = " + nkeys);

            //public class chinese_pop_class
            //{
            //public int adm1 = -1;
            //public long pop = -1;
            //public long malepop = -1;
            //public long femalepop = -1;
            //public long households = -1;
            //public long pop014 = -1;
            //public long pop1564 = -1;
            //public long pop65 = -1;
            //}

            foreach (int fn in filekeys.Keys)
            {
                string filename = filepath + "China" + fn.ToString() + ".txt";
                Console.WriteLine(filename);
                if (!File.Exists(filename))
                    continue;

                int npop = 0;
                using (StreamReader sr = new StreamReader(filename))
                {
                    bool started = false;
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        string[] words = line.Split(tabchar);
                        if (!started) //skip preamble in file
                        {
                            if (words[0] == "start")
                                started = true;
                            continue;
                        }
                        chinese_pop_class cc = new chinese_pop_class();
                        cc.adm1 = filekeys[fn];
                        if (words.Length <= 1)
                            continue;
                        cc.pop = tryconvertlong(words[1]);
                        if (cc.pop < 0)
                            continue;
                        if (words.Length > 9)
                        {
                            cc.malepop = tryconvertlong(words[2]);
                            cc.femalepop = tryconvertlong(words[3]);
                            cc.households = tryconvertlong(words[4]);
                            cc.pop014 = tryconvertlong(words[7]);
                            cc.pop1564 = tryconvertlong(words[8]);
                            cc.pop65 = tryconvertlong(words[9]);
                        }
                        if (!chinese_pop_dict.ContainsKey(words[0]))
                        {
                            List<chinese_pop_class> cl = new List<chinese_pop_class>();
                            chinese_pop_dict.Add(words[0], cl);
                        }
                        chinese_pop_dict[words[0]].Add(cc);
                        npop++;
                    }
                }
                Console.WriteLine("npop = " + npop);
            }

        }

        public static void chinese_special()
        {
            read_chinese_pop();

            int nfdouble = 0;
            foreach (int gnid in gndict.Keys)
            {
                if ((gndict[gnid].featureclass == 'A') || (gndict[gnid].featureclass == 'P'))
                {
                    int nfcc = 0;
                    foreach (string an in gndict[gnid].altnames)
                    {
                        if (chinese_pop_dict.ContainsKey(an))
                        {
                            foreach (chinese_pop_class cc in chinese_pop_dict[an])
                            {
                                if (cc.adm1 == gndict[gnid].adm[1])
                                {
                                    nfcc++;
                                    if (nfcc == 1)
                                        chinese_pop_dict2.Add(gnid, cc);
                                    else if (nfcc == 2)
                                    {
                                        //Console.WriteLine("pop1 = " + cc.pop);
                                        //Console.WriteLine("pop2 = " + chinese_pop_dict2[gnid].pop);
                                        chinese_pop_dict2.Remove(gnid);
                                        nfdouble++;
                                    }
                                }
                            }
                            //if ( nfcc > 0 )
                            //    Console.WriteLine("nfcc = " + nfcc);
                        }
                    }

                }
            }

            Console.WriteLine("chinese pop found: " + chinese_pop_dict2.Count);
            Console.WriteLine("nfdouble = " + nfdouble);
            //Console.ReadLine();
        }

        public static void read_geonames(string countrycode)
        {

            int n = 0;
            int nbad = 0;
            Console.WriteLine("read_geonames " + countrycode);

            string filename = geonamesfolder;
            if (countrycode == "")
                filename += "allCountries.txt";
            else
                filename += "Countries//" + countrycode + ".txt";

            gnfiledate = File.GetLastWriteTime(@filename);
            dumpdate = gnfiledate.ToString("yyyy-MM-dd");

            //for checkminutes:
            int ntot = 0;
            int nclosex = 0;
            int nclosey = 0;
            int ncloseboth = 0;
            Dictionary<string, int> ntotcountry = new Dictionary<string, int>();
            Dictionary<string, int> ncbcountry = new Dictionary<string, int>();
            Dictionary<string, int> ntotfcode = new Dictionary<string, int>();
            Dictionary<string, int> ncbfcode = new Dictionary<string, int>();
            double minutedist = 0.01;
            Page pmin = new Page(makesite, "Användare:Lsjbot/Minutavrundade");


            using (StreamReader sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();

                    if (line[0] == '#')
                        continue;

                    //if (n > 250)
                    //    Console.WriteLine(line);

                    string[] words = line.Split(tabchar);

                    bool badone = false;

                    //foreach (string s in words)
                    //    Console.WriteLine(s);

                    //Console.WriteLine(words[0] + "|" + words[1]);

                    int geonameid = -1;

                    geonameclass gn = new geonameclass();

                    words[1] = initialcap(words[1]);

                    gn.Name = words[1];
                    gn.Name_ml = words[1];
                    gn.articlename = "XXX";
                    geonameid = tryconvert(words[0]);
                    if (geonameid <= 0)
                        continue;
                    gn.asciiname = words[2];
                    foreach (string ll in words[3].Split(','))
                        gn.altnames.Add(initialcap(ll));
                    gn.latitude = tryconvertdouble(words[4]);
                    gn.longitude = tryconvertdouble(words[5]);

                    if (words[6].Length > 0)
                        gn.featureclass = words[6][0];
                    gn.featurecode = words[7];

                    if (!featuredict.ContainsKey(gn.featurecode))
                        badone = true;

                    for (int ii = 0; ii < 4; ii++)
                        gn.adm[ii] = -1;
                    if (countryid.ContainsKey(words[8]))
                        gn.adm[0] = countryid[words[8]];
                    if (!altnamesonly)
                        foreach (string ll in words[9].Split(','))
                        {
                            if ((ll != words[8]) && (countryid.ContainsKey(ll)))
                                gn.altcountry.Add(countryid[ll]);
                        }
                    if (adm1dict.ContainsKey(words[8]))
                    {
                        if (adm1dict[words[8]].ContainsKey(words[10]))
                            gn.adm[1] = adm1dict[words[8]][words[10]];
                        else if (adm1dict[words[8]].ContainsKey("0" + words[10]))
                            gn.adm[1] = adm1dict[words[8]]["0" + words[10]];
                    }
                    if (adm2dict.ContainsKey(words[8]))
                        if (adm2dict[words[8]].ContainsKey(words[10]))
                            if (adm2dict[words[8]][words[10]].ContainsKey(words[11]))
                                gn.adm[2] = adm2dict[words[8]][words[10]][words[11]];
                    gn.adm[3] = tryconvert(words[12]);
                    gn.adm[4] = tryconvert(words[13]);
                    for (int ii = 1; ii < 4; ii++)
                        if (gn.adm[ii] == geonameid)
                            gn.adm[ii] = -1;

                    gn.population = tryconvertlong(words[14]);
                    gn.elevation = tryconvert(words[15]);
                    gn.dem = tryconvert(words[16]);
                    if ((gn.elevation <= 0) && (gn.dem > 0))
                        gn.elevation = gn.dem;
                    gn.tz = words[17];
                    gn.moddate = words[18];

                    //if ((gn.featureclass == 'P') && (gn.population <= 0) && (!checkwikidata))
                    //    badone = true;

                    bool skipnoheight = false;
                    if (skipnoheight)
                    {
                        if ((gn.featureclass == 'T') && (gn.elevation <= 0) && (gn.dem <= 0))
                        {
                            switch (gn.featurecode)
                            {
                                case "HLL":
                                    badone = true;
                                    break;
                                case "HLLS":
                                    badone = true;
                                    break;
                                case "MT":
                                    badone = true;
                                    break;
                                case "MTS":
                                    badone = true;
                                    break;
                                case "MESA":
                                    badone = true;
                                    break;
                                case "MND":
                                    badone = true;
                                    break;
                                case "PK":
                                    badone = true;
                                    break;
                                case "PKS":
                                    badone = true;
                                    break;
                                case "RDGE":
                                    badone = true;
                                    break;
                                default:
                                    break;
                            }

                        }
                    }

                    if (gn.featurecode == "PPLC") //Capital
                    {
                        if (countrydict.ContainsKey(gn.adm[0]))
                        {
                            countrydict[gn.adm[0]].capital_gnid = geonameid;
                        }
                    }

                    if (statisticsonly)
                    {
                        //Console.WriteLine(gn.featurecode);
                        fchist.Add(gn.featurecode);
                        if (badone)
                            fcbad.Add(gn.featurecode);
                        fclasshist.Add(gn.featureclass);
                        if (!badone && categorydict.ContainsKey(gn.featurecode))
                            fcathist.Add(categorydict[gn.featurecode]);
                    }

                    //    public static Dictionary<string, List<int>> namefork = new Dictionary<string, List<int>>(); //names with list of corresponding geonameid(s)

                    if (!badone)
                    {
                        //if (altnamesonly)
                        //{
                        //    addnamefork(geonameid, gn.Name);
                        //    addnamefork(geonameid, gn.asciiname);
                        //    foreach (string nm in gn.altnames)
                        //        addnamefork(geonameid, nm);
                        //}

                        if (!altnamesonly)
                            addlatlong(gn.latitude, gn.longitude, geonameid);


                        double dlatmin = 60 * gn.latitude;
                        double dlonmin = 60 * gn.longitude;
                        double latdiff = Math.Abs(dlatmin - Convert.ToInt32(dlatmin));
                        double londiff = Math.Abs(dlonmin - Convert.ToInt32(dlonmin));
                        if (checkminutes)
                        {
                            ntot++;
                            if (!ntotcountry.ContainsKey(words[8]))
                            {
                                ntotcountry.Add(words[8], 0);
                                ncbcountry.Add(words[8], 0);
                            }
                            if (!ntotfcode.ContainsKey(words[7]))
                            {
                                ntotfcode.Add(words[7], 0);
                                ncbfcode.Add(words[7], 0);
                            }

                            ntotcountry[words[8]]++;
                            ntotfcode[words[7]]++;

                            if (latdiff < minutedist)
                                nclosey++;
                        }
                        if (londiff < minutedist)
                        {
                            nclosex++;
                            if (latdiff < minutedist)
                            {
                                if (checkminutes)
                                {
                                    ncloseboth++;
                                    ncbcountry[words[8]]++;
                                    ncbfcode[words[7]]++;
                                }
                                gn.roundminute = true;
                            }
                        }


                        if ((geonameid > 0) && (!badone))// && (!statisticsonly))
                        {
                            gndict.Add(geonameid, gn);
                        }


                    }
                    else
                        nbad++;

                    n++;
                    if ((n % 10000) == 0)
                    {
                        Console.WriteLine("n    (geonames)   = " + n.ToString());
                        Console.WriteLine("nbad (geonames)   = " + nbad.ToString());
                        if (n >= maxread)
                            break;
                    }

                }

                Console.WriteLine("n    (geonames) = " + n.ToString());
                Console.WriteLine("nbad (geonames) = " + nbad.ToString());


            }

            if (checkminutes)
            {
                Console.WriteLine("nclosex = " + nclosex);
                Console.WriteLine("nclosey = " + nclosey);
                Console.WriteLine("nclosebot = " + ncloseboth);
                Console.WriteLine("ntot = " + ntot);
                double ratio = ncloseboth;
                ratio = ratio / ntot;
                Console.WriteLine("ncloseboth/ntot (%) = " + 100 * ratio);

                pmin.text = "== Länder ==\n";
                foreach (string cc in ntotcountry.Keys)
                {
                    ratio = ncbcountry[cc];
                    ratio = ratio / ntotcountry[cc];
                    Console.WriteLine(cc + ": ncloseboth/ntot (%) = " + 100 * ratio);
                    pmin.text += "*" + cc + ": Andel minutavrundade (%) = " + (100 * ratio).ToString("N1") + "\n";
                }
                pmin.text += "\n== Platstyper ==\n";
                foreach (string cc in ntotfcode.Keys)
                {
                    ratio = ncbfcode[cc];
                    ratio = ratio / ntotfcode[cc];
                    if (ntotfcode[cc] > 100)
                        Console.WriteLine(cc + ": ncloseboth/ntot (%) = " + 100 * ratio);
                    pmin.text += "*" + cc + ": Andel minutavrundade (%) = " + (100 * ratio).ToString("N1") + "\n";
                }
                pmin.text += "\n== Vanligaste platstyperna ==\n";
                foreach (string cc in ntotfcode.Keys)
                {
                    if (ntotfcode[cc] < 10000)
                        continue;
                    ratio = ncbfcode[cc];
                    ratio = ratio / ntotfcode[cc];
                    Console.WriteLine(cc + ": ncloseboth/ntot (%) = " + 100 * ratio);
                    pmin.text += "*" + linkfeature(cc, -1) + ": Andel minutavrundade (%) = " + (100 * ratio).ToString("N1") + "\n";
                }
                trysave(pmin, 1,mp(302,null));
                Console.ReadLine();

            }


            if ((!verifywikidata) && (!checkwikidata))
                read_wd_files(countrycode); //files for individual countries, with pop and area

            if (checkwikidata)
                read_good_wd_file(); //file for all countries, with id match only

            if (makecountry == "CN")
                chinese_special();

            //clear away villages without population:
            if ((!verifywikidata) && (!checkwikidata))
            {
                List<int> ghosts = new List<int>();
                foreach (int gnid in gndict.Keys)
                {
                    if ((gndict[gnid].featureclass == 'P') && (gndict[gnid].population <= minimum_population) && (gndict[gnid].population_wd <= minimum_population))
                    {
                        if ((makecountry == "CN") && (chinese_pop_dict2.ContainsKey(gnid)))
                        {
                            //public class forkclass //class for entries in a fork page
                            //{
                            //    public int geonameid = 0;
                            //    public string featurecode = "";
                            //    public string[] admname = new string[3];
                            //    public double latitude = 0.0;
                            //    public double longitude = 0.0;
                            //    public string realname = "*"; 
                            //    public int wdid = -1;    //wikidata id
                            //    public string iso = "XX"; //country iso code
                            //    public string featurename = "";
                            //}

                            //public class Disambigclass //class for disambiguation in article names
                            //{
                            //    public bool existsalready = false;
                            //    public bool country = false;
                            //    public bool adm1 = false;
                            //    public bool adm2 = false;
                            //    public bool latlong = false;
                            //    public bool fcode = false;
                            //    public forkclass fork = new forkclass();
                            //}

                            Disambigclass da = new Disambigclass();
                            da.country = true;
                            da.adm1 = true;
                            da.adm2 = true;
                            da.latlong = true;
                            da.fcode = true;
                            da.fork.geonameid = gnid;
                            da.fork.featurecode = gndict[gnid].featurecode;
                            string countryname = countrydict[gndict[gnid].adm[0]].Name;
                            string countrynameml = countryname;
                            if (countryml.ContainsKey(countryname))
                                countrynameml = countryml[countryname];

                            da.fork.admname[0] = countrynameml;
                            if (gndict.ContainsKey(gndict[gnid].adm[1]))
                                da.fork.admname[1] = gndict[gndict[gnid].adm[1]].Name_ml;
                            else
                                da.fork.admname[1] = "";
                            if (gndict.ContainsKey(gndict[gnid].adm[2]))
                                da.fork.admname[2] = gndict[gndict[gnid].adm[2]].Name_ml;
                            else
                                da.fork.admname[2] = "";
                            da.fork.latitude = gndict[gnid].latitude;
                            da.fork.longitude = gndict[gnid].longitude;
                            da.fork.iso = makecountry;

                            gndict[gnid].articlename = gndict[gnid].Name_ml + " " + make_disambig(da, gnid);
                            gndict[gnid].population = chinese_pop_dict2[gnid].pop;
                            resurrected.Add(gnid);
                        }
                        else
                            ghosts.Add(gnid);
                    }
                    else if ((gndict[gnid].featurecode == "PPLQ") && (gndict[gnid].population > 0))
                        ghosts.Add(gnid);
                }

                foreach (int gnid in ghosts)
                {
                    existingclass gh = new existingclass();
                    gh.articlename = gndict[gnid].Name_ml;
                    gh.latitude = gndict[gnid].latitude;
                    gh.longitude = gndict[gnid].longitude;
                    if (!ghostdict.ContainsKey(gnid))
                        ghostdict.Add(gnid, gh);

                    gndict.Remove(gnid);
                }

                if (resurrected.Count > 0)
                {
                    using (StreamWriter sw = new StreamWriter("resurrected-" + makelang + ".txt"))
                    {

                        foreach (int gnid in resurrected)
                        {
                            sw.WriteLine(gnid.ToString() + tabstring + gndict[gnid].articlename);
                        }
                    }
                    Console.WriteLine(resurrected.Count + " resurrected.");
                    //Console.ReadLine();
                }
            }

            read_islands(countrycode); //which place is on which island?

            read_lakes(countrycode); //which place is in or near which lake?

            read_ranges(countrycode); //which mountain is in which mountain range?

            read_altitudes(countrycode);

            //get proper country names
            foreach (int gnid in countrydict.Keys)
                if (gndict.ContainsKey(gnid))
                    gndict[gnid].Name_ml = countrydict[gnid].Name_ml;

            Console.WriteLine("read_geonames done");

        }

        public static bool is_latin(string name)
        {
            return (get_alphabet(name) == "latin");
        }

        public static string get_alphabet(string name)
        {
            char[] letters = remove_disambig(name).ToCharArray();
            int n = 0;
            int sum = 0;
            //int nlatin = 0;
            Dictionary<string, int> alphdir = new Dictionary<string, int>();
            foreach (char c in letters)
            {
                int uc = Convert.ToInt32(c);
                sum += uc;
                string alphabet = "none";
                if (uc <= 0x0040) alphabet = "none";
                //else if ((uc >= 0x0030) && (uc <= 0x0039)) alphabet = "number";
                //else if ((uc >= 0x0020) && (uc <= 0x0040)) alphabet = "punctuation";
                else if ((uc >= 0x0041) && (uc <= 0x007F)) alphabet = "latin";
                else if ((uc >= 0x00A0) && (uc <= 0x00FF)) alphabet = "latin";
                else if ((uc >= 0x0100) && (uc <= 0x017F)) alphabet = "latin";
                else if ((uc >= 0x0180) && (uc <= 0x024F)) alphabet = "latin";
                else if ((uc >= 0x0250) && (uc <= 0x02AF)) alphabet = "phonetic";
                else if ((uc >= 0x02B0) && (uc <= 0x02FF)) alphabet = "spacing modifier letters";
                else if ((uc >= 0x0300) && (uc <= 0x036F)) alphabet = "combining diacritical marks";
                else if ((uc >= 0x0370) && (uc <= 0x03FF)) alphabet = "greek and coptic";
                else if ((uc >= 0x0400) && (uc <= 0x04FF)) alphabet = "cyrillic";
                else if ((uc >= 0x0500) && (uc <= 0x052F)) alphabet = "cyrillic";
                else if ((uc >= 0x0530) && (uc <= 0x058F)) alphabet = "armenian";
                else if ((uc >= 0x0590) && (uc <= 0x05FF)) alphabet = "hebrew";
                else if ((uc >= 0x0600) && (uc <= 0x06FF)) alphabet = "arabic";
                else if ((uc >= 0x0700) && (uc <= 0x074F)) alphabet = "syriac";
                else if ((uc >= 0x0780) && (uc <= 0x07BF)) alphabet = "thaana";
                else if ((uc >= 0x0900) && (uc <= 0x097F)) alphabet = "devanagari";
                else if ((uc >= 0x0980) && (uc <= 0x09FF)) alphabet = "bengali";
                else if ((uc >= 0x0A00) && (uc <= 0x0A7F)) alphabet = "gurmukhi";
                else if ((uc >= 0x0A80) && (uc <= 0x0AFF)) alphabet = "gujarati";
                else if ((uc >= 0x0B00) && (uc <= 0x0B7F)) alphabet = "oriya";
                else if ((uc >= 0x0B80) && (uc <= 0x0BFF)) alphabet = "tamil";
                else if ((uc >= 0x0C00) && (uc <= 0x0C7F)) alphabet = "telugu";
                else if ((uc >= 0x0C80) && (uc <= 0x0CFF)) alphabet = "kannada";
                else if ((uc >= 0x0D00) && (uc <= 0x0D7F)) alphabet = "malayalam";
                else if ((uc >= 0x0D80) && (uc <= 0x0DFF)) alphabet = "sinhala";
                else if ((uc >= 0x0E00) && (uc <= 0x0E7F)) alphabet = "thai";
                else if ((uc >= 0x0E80) && (uc <= 0x0EFF)) alphabet = "lao";
                else if ((uc >= 0x0F00) && (uc <= 0x0FFF)) alphabet = "tibetan";
                else if ((uc >= 0x1000) && (uc <= 0x109F)) alphabet = "myanmar";
                else if ((uc >= 0x10A0) && (uc <= 0x10FF)) alphabet = "georgian";
                else if ((uc >= 0x1100) && (uc <= 0x11FF)) alphabet = "korean";
                else if ((uc >= 0x1200) && (uc <= 0x137F)) alphabet = "ethiopic";
                else if ((uc >= 0x13A0) && (uc <= 0x13FF)) alphabet = "cherokee";
                else if ((uc >= 0x1400) && (uc <= 0x167F)) alphabet = "unified canadian aboriginal syllabics";
                else if ((uc >= 0x1680) && (uc <= 0x169F)) alphabet = "ogham";
                else if ((uc >= 0x16A0) && (uc <= 0x16FF)) alphabet = "runic";
                else if ((uc >= 0x1700) && (uc <= 0x171F)) alphabet = "tagalog";
                else if ((uc >= 0x1720) && (uc <= 0x173F)) alphabet = "hanunoo";
                else if ((uc >= 0x1740) && (uc <= 0x175F)) alphabet = "buhid";
                else if ((uc >= 0x1760) && (uc <= 0x177F)) alphabet = "tagbanwa";
                else if ((uc >= 0x1780) && (uc <= 0x17FF)) alphabet = "khmer";
                else if ((uc >= 0x1800) && (uc <= 0x18AF)) alphabet = "mongolian";
                else if ((uc >= 0x1900) && (uc <= 0x194F)) alphabet = "limbu";
                else if ((uc >= 0x1950) && (uc <= 0x197F)) alphabet = "tai le";
                else if ((uc >= 0x19E0) && (uc <= 0x19FF)) alphabet = "khmer";
                else if ((uc >= 0x1D00) && (uc <= 0x1D7F)) alphabet = "phonetic";
                else if ((uc >= 0x1E00) && (uc <= 0x1EFF)) alphabet = "latin";
                else if ((uc >= 0x1F00) && (uc <= 0x1FFF)) alphabet = "greek and coptic";
                else if ((uc >= 0x2000) && (uc <= 0x206F)) alphabet = "none";
                else if ((uc >= 0x2070) && (uc <= 0x209F)) alphabet = "none";
                else if ((uc >= 0x20A0) && (uc <= 0x20CF)) alphabet = "none";
                else if ((uc >= 0x20D0) && (uc <= 0x20FF)) alphabet = "combining diacritical marks for symbols";
                else if ((uc >= 0x2100) && (uc <= 0x214F)) alphabet = "letterlike symbols";
                else if ((uc >= 0x2150) && (uc <= 0x218F)) alphabet = "none";
                else if ((uc >= 0x2190) && (uc <= 0x21FF)) alphabet = "none";
                else if ((uc >= 0x2200) && (uc <= 0x22FF)) alphabet = "none";
                else if ((uc >= 0x2300) && (uc <= 0x23FF)) alphabet = "none";
                else if ((uc >= 0x2400) && (uc <= 0x243F)) alphabet = "none";
                else if ((uc >= 0x2440) && (uc <= 0x245F)) alphabet = "optical character recognition";
                else if ((uc >= 0x2460) && (uc <= 0x24FF)) alphabet = "enclosed alphanumerics";
                else if ((uc >= 0x2500) && (uc <= 0x257F)) alphabet = "none";
                else if ((uc >= 0x2580) && (uc <= 0x259F)) alphabet = "none";
                else if ((uc >= 0x25A0) && (uc <= 0x25FF)) alphabet = "none";
                else if ((uc >= 0x2600) && (uc <= 0x26FF)) alphabet = "none";
                else if ((uc >= 0x2700) && (uc <= 0x27BF)) alphabet = "none";
                else if ((uc >= 0x27C0) && (uc <= 0x27EF)) alphabet = "none";
                else if ((uc >= 0x27F0) && (uc <= 0x27FF)) alphabet = "none";
                else if ((uc >= 0x2800) && (uc <= 0x28FF)) alphabet = "braille";
                else if ((uc >= 0x2900) && (uc <= 0x297F)) alphabet = "none";
                else if ((uc >= 0x2980) && (uc <= 0x29FF)) alphabet = "none";
                else if ((uc >= 0x2A00) && (uc <= 0x2AFF)) alphabet = "none";
                else if ((uc >= 0x2B00) && (uc <= 0x2BFF)) alphabet = "none";
                else if ((uc >= 0x2E80) && (uc <= 0x2EFF)) alphabet = "chinese/japanese";
                else if ((uc >= 0x2F00) && (uc <= 0x2FDF)) alphabet = "chinese/japanese";
                else if ((uc >= 0x2FF0) && (uc <= 0x2FFF)) alphabet = "none";
                else if ((uc >= 0x3000) && (uc <= 0x303F)) alphabet = "chinese/japanese";
                else if ((uc >= 0x3040) && (uc <= 0x309F)) alphabet = "chinese/japanese";
                else if ((uc >= 0x30A0) && (uc <= 0x30FF)) alphabet = "chinese/japanese";
                else if ((uc >= 0x3100) && (uc <= 0x312F)) alphabet = "bopomofo";
                else if ((uc >= 0x3130) && (uc <= 0x318F)) alphabet = "korean";
                else if ((uc >= 0x3190) && (uc <= 0x319F)) alphabet = "chinese/japanese";
                else if ((uc >= 0x31A0) && (uc <= 0x31BF)) alphabet = "bopomofo";
                else if ((uc >= 0x31F0) && (uc <= 0x31FF)) alphabet = "chinese/japanese";
                else if ((uc >= 0x3200) && (uc <= 0x32FF)) alphabet = "chinese/japanese";
                else if ((uc >= 0x3300) && (uc <= 0x33FF)) alphabet = "chinese/japanese";
                else if ((uc >= 0x3400) && (uc <= 0x4DBF)) alphabet = "chinese/japanese";
                else if ((uc >= 0x4DC0) && (uc <= 0x4DFF)) alphabet = "none";
                else if ((uc >= 0x4E00) && (uc <= 0x9FFF)) alphabet = "chinese/japanese";
                else if ((uc >= 0xA000) && (uc <= 0xA48F)) alphabet = "chinese/japanese";
                else if ((uc >= 0xA490) && (uc <= 0xA4CF)) alphabet = "chinese/japanese";
                else if ((uc >= 0xAC00) && (uc <= 0xD7AF)) alphabet = "korean";
                else if ((uc >= 0xD800) && (uc <= 0xDB7F)) alphabet = "high surrogates";
                else if ((uc >= 0xDB80) && (uc <= 0xDBFF)) alphabet = "high private use surrogates";
                else if ((uc >= 0xDC00) && (uc <= 0xDFFF)) alphabet = "low surrogates";
                else if ((uc >= 0xE000) && (uc <= 0xF8FF)) alphabet = "private use area";
                else if ((uc >= 0xF900) && (uc <= 0xFAFF)) alphabet = "chinese/japanese";
                else if ((uc >= 0xFB00) && (uc <= 0xFB4F)) alphabet = "alphabetic presentation forms";
                else if ((uc >= 0xFB50) && (uc <= 0xFDFF)) alphabet = "arabic";
                else if ((uc >= 0xFE00) && (uc <= 0xFE0F)) alphabet = "variation selectors";
                else if ((uc >= 0xFE20) && (uc <= 0xFE2F)) alphabet = "combining half marks";
                else if ((uc >= 0xFE30) && (uc <= 0xFE4F)) alphabet = "chinese/japanese";
                else if ((uc >= 0xFE50) && (uc <= 0xFE6F)) alphabet = "small form variants";
                else if ((uc >= 0xFE70) && (uc <= 0xFEFF)) alphabet = "arabic";
                else if ((uc >= 0xFF00) && (uc <= 0xFFEF)) alphabet = "halfwidth and fullwidth forms";
                else if ((uc >= 0xFFF0) && (uc <= 0xFFFF)) alphabet = "specials";
                else if ((uc >= 0x10000) && (uc <= 0x1007F)) alphabet = "linear b";
                else if ((uc >= 0x10080) && (uc <= 0x100FF)) alphabet = "linear b";
                else if ((uc >= 0x10100) && (uc <= 0x1013F)) alphabet = "aegean numbers";
                else if ((uc >= 0x10300) && (uc <= 0x1032F)) alphabet = "old italic";
                else if ((uc >= 0x10330) && (uc <= 0x1034F)) alphabet = "gothic";
                else if ((uc >= 0x10380) && (uc <= 0x1039F)) alphabet = "ugaritic";
                else if ((uc >= 0x10400) && (uc <= 0x1044F)) alphabet = "deseret";
                else if ((uc >= 0x10450) && (uc <= 0x1047F)) alphabet = "shavian";
                else if ((uc >= 0x10480) && (uc <= 0x104AF)) alphabet = "osmanya";
                else if ((uc >= 0x10800) && (uc <= 0x1083F)) alphabet = "cypriot syllabary";
                else if ((uc >= 0x1D000) && (uc <= 0x1D0FF)) alphabet = "byzantine musical symbols";
                else if ((uc >= 0x1D100) && (uc <= 0x1D1FF)) alphabet = "musical symbols";
                else if ((uc >= 0x1D300) && (uc <= 0x1D35F)) alphabet = "tai xuan jing symbols";
                else if ((uc >= 0x1D400) && (uc <= 0x1D7FF)) alphabet = "none";
                else if ((uc >= 0x20000) && (uc <= 0x2A6DF)) alphabet = "chinese/japanese";
                else if ((uc >= 0x2F800) && (uc <= 0x2FA1F)) alphabet = "chinese/japanese";
                else if ((uc >= 0xE0000) && (uc <= 0xE007F)) alphabet = "none";

                bool ucprint = false;
                if (alphabet != "none")
                {
                    n++;
                    if (!alphdir.ContainsKey(alphabet))
                        alphdir.Add(alphabet, 0);
                    alphdir[alphabet]++;
                }
                else if (uc != 0x0020)
                {
                    //Console.Write("c=" + c.ToString() + ", uc=0x" + uc.ToString("x5") + "|");
                    //ucprint = true;
                }
                if (ucprint)
                    Console.WriteLine();
            }

            int nmax = 0;
            string alphmax = "none";
            foreach (string alph in alphdir.Keys)
            {
                //Console.WriteLine("ga:" + alph + " " + alphdir[alph].ToString());
                if (alphdir[alph] > nmax)
                {
                    nmax = alphdir[alph];
                    alphmax = alph;
                }
            }

            if (letters.Length > 2 * n) //mostly non-alphabetic
                return "none";
            else if (nmax > n / 2) //mostly same alphabet
                return alphmax;
            else
                return "mixed"; //mixed alphabets
        }

        public static string get_alphabet_sv(string alph_en)
        {
            Console.WriteLine("gas:" + alph_en);
            if (alphabet_sv.Count == 0)
            {
                alphabet_sv.Add("bopomofo", "zhuyin");
                alphabet_sv.Add("halfwidth and fullwidth forms", "");
                alphabet_sv.Add("syriac", "syriska alfabetet");
                alphabet_sv.Add("thaana", "tāna");
                alphabet_sv.Add("lao", "laotisk skrift");
                alphabet_sv.Add("khmer", "khmerisk skrift");
                alphabet_sv.Add("gurmukhi", "gurmukhi");
                alphabet_sv.Add("myanmar", "burmesisk skrift");
                alphabet_sv.Add("tibetan", "tibetansk skrift");
                alphabet_sv.Add("sinhala", "singalesisk skrift");
                alphabet_sv.Add("ethiopic", "etiopisk skrift");
                alphabet_sv.Add("oriya", "oriya-skrift");
                alphabet_sv.Add("kannada", "kannada");
                alphabet_sv.Add("malayalam", "malayalam");
                alphabet_sv.Add("telugu", "teluguskrift");
                alphabet_sv.Add("tamil", "tamilska alfabetet");
                alphabet_sv.Add("gujarati", "gujarati");
                alphabet_sv.Add("bengali", "bengalisk skrift");
                alphabet_sv.Add("armenian", "armeniska alfabetet");
                alphabet_sv.Add("georgian", "georgiska alfabetet");
                alphabet_sv.Add("devanagari", "devanāgarī");
                alphabet_sv.Add("korean", "hangul");
                alphabet_sv.Add("hebrew", "hebreiska alfabetet");
                alphabet_sv.Add("greek and coptic", "grekiska alfabetet");
                alphabet_sv.Add("chinese/japanese", "kinesiska tecken");
                alphabet_sv.Add("thai", "thailändska alfabetet");
                alphabet_sv.Add("cyrillic", "kyrilliska alfabetet");
                alphabet_sv.Add("arabic", "arabiska alfabetet");
                alphabet_sv.Add("latin", "latinska alfabetet");
            }

            if (alphabet_sv.ContainsKey(alph_en))
                return alphabet_sv[alph_en];
            else
                return "okänd skrift";
        }

        public static void add_nameforks()
        {
            int ntot = gndict.Count;

            Console.WriteLine("Add_nameforks: " + ntot.ToString() + " to do.");

            foreach (int gnid in gndict.Keys)
            {
                addnamefork(gnid, gndict[gnid].Name);
                addnamefork(gnid, gndict[gnid].Name_ml);
                addnamefork(gnid, gndict[gnid].asciiname);

                if (altdict.ContainsKey(gnid))
                {
                    foreach (altnameclass ac in altdict[gnid])
                    {
                        addnamefork(gnid, ac.altname);
                    }
                }
                //else
                //{
                //    foreach (string nm in gndict[gnid].altnames)
                //        addnamefork(gnid, nm);
                //}

                //if (gndict[gnid].wdid > 0)
                //{
                //    XmlDocument cx = get_wd_xml(wdid);
                //    if (cx != null)
                //    {
                //        Dictionary<string, string> rd = get_wd_sitelinks(cx);
                //        foreach (string sw in rd.Keys)
                //            addnamefork(gnid, rd[sw]);
                //    }
                //}

                ntot--;
                if ((ntot % 1000) == 0)
                    Console.WriteLine("=== " + ntot.ToString() + " left ===");
            }
        }

        public static void read_island_file(string wdcountry)
        {
            string filename = geonamesfolder + @"islands\islands-" + wdcountry + ".txt";
            islanddict.Clear();
            try
            {
                int nislands = 0;
                using (StreamReader sr = new StreamReader(filename))
                {
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        string[] words = line.Split(tabchar);
                        //sw.Write(gnid.ToString() + tabstring + area.ToString() + tabstring + kmew.ToString() + tabstring + kmns.ToString());
                        //foreach (int oi in onisland)
                        //    sw.Write(tabstring + oi.ToString());
                        //sw.WriteLine();

                        if (words.Length < 4)
                            continue;

                        int gnid = tryconvert(words[0]);
                        if (!gndict.ContainsKey(gnid))
                            continue;

                        double area = tryconvertdouble(words[1]);
                        if ((area > 0) && (gndict[gnid].area <= 0))
                            gndict[gnid].area = area;

                        islandclass isl = new islandclass();
                        isl.area = area;

                        double scale = Math.Cos(gndict[gnid].latitude * 3.1416 / 180);
                        double pixkmx = scale * 40000 / (360 * 1200);
                        double pixkmy = 40000.0 / (360.0 * 1200.0);

                        isl.kmew = tryconvertdouble(words[2]) + pixkmx;
                        isl.kmns = tryconvertdouble(words[3]) + pixkmy;
                        for (int i = 4; i < words.Length; i++)
                        {
                            int oi = tryconvert(words[i]);
                            if (gndict.ContainsKey(oi))
                            {
                                isl.onisland.Add(oi);
                                //if (gndict[oi].island <= 0)
                                //{
                                //    gndict[oi].island = gnid;
                                //}
                                //else //on two islands - error
                                //{
                                //    otherisland = gndict[oi].island;
                                //    gndict[oi].island = 0;
                                //}
                            }
                        }
                        //if (islanddict.ContainsKey(otherisland))
                        //    islanddict.Remove(otherisland);
                        //else
                        islanddict.Add(gnid, isl);
                        nislands++;
                    }
                }

                Console.WriteLine("# islands = " + nislands.ToString());

                Dictionary<int, int> oindex = new Dictionary<int, int>();
                //List<int> badlist = new List<int>();
                Dictionary<int, List<int>> badlist = new Dictionary<int, List<int>>();

                //identify stuff that's listed as on two islands:
                foreach (int gnid in islanddict.Keys)
                {
                    //first add island itself as "on" itself...
                    if (oindex.ContainsKey(gnid))
                    {
                        if (!badlist.ContainsKey(oindex[gnid]))
                        {
                            List<int> bl = new List<int>();
                            badlist.Add(oindex[gnid], bl);
                        }
                        if (!badlist[oindex[gnid]].Contains(gnid))
                            badlist[oindex[gnid]].Add(gnid);
                    }
                    else
                        oindex.Add(gnid, gnid);
                    //... then add everything else on the island.
                    foreach (int oi in islanddict[gnid].onisland)
                    {
                        if (oindex.ContainsKey(oi))
                        {
                            if (!badlist.ContainsKey(oindex[oi]))
                            {
                                List<int> bl = new List<int>();
                                badlist.Add(oindex[oi], bl);
                            }
                            if (!badlist[oindex[oi]].Contains(gnid))
                                badlist[oindex[oi]].Add(gnid);
                        }
                        else
                            oindex.Add(oi, gnid);

                    }
                }

                if (verifyislands) //Go through and find best island for stuff with double location,
                {                  //then make new island file.
                    int nbad = 0;

                    foreach (int badi in badlist.Keys)
                    {
                        long bestdist = seed_center_dist(badi);
                        long best2dist = 99999999;
                        int best = badi;
                        foreach (int badg in badlist[badi])
                        {
                            long scdist = seed_center_dist(badg);
                            if (scdist < bestdist)
                            {
                                best2dist = bestdist;
                                bestdist = scdist;
                                best = badg;
                            }
                        }

                        if (bestdist * 3 > best2dist) //require 3 times better to "promote" one of the islands
                            best = -1;

                        if (badi != best)
                        {
                            islanddict.Remove(badi);
                            nbad++;
                        }
                        foreach (int badg in badlist[badi])
                            if (badg != best)
                            {
                                islanddict.Remove(badg);
                                nbad++;
                            }
                    }

                    Console.WriteLine("# islands = " + nislands.ToString());
                    Console.WriteLine("# bad islands = " + nbad.ToString());
                    using (StreamWriter sw = new StreamWriter("islands-" + makecountry + ".txt"))
                    {

                        foreach (int gnid in islanddict.Keys)
                        {
                            sw.Write(gnid.ToString() + tabstring + islanddict[gnid].area.ToString() + tabstring + islanddict[gnid].kmew.ToString() + tabstring + islanddict[gnid].kmns.ToString());
                            foreach (int oi in islanddict[gnid].onisland)
                                sw.Write(tabstring + oi.ToString());
                            sw.WriteLine();
                        }
                    }
                }
                else //just remove the duplicate islands
                {
                    foreach (int badi in badlist.Keys)
                    {
                        islanddict.Remove(badi);
                        foreach (int badg in badlist[badi])
                            islanddict.Remove(badg);
                    }
                }

                foreach (int gnid in islanddict.Keys)
                {
                    foreach (int oi in islanddict[gnid].onisland)
                    {
                        gndict[oi].island = gnid;
                    }
                }


            }
            catch (IOException e)
            {
                string message = e.Message;
                Console.Error.WriteLine(message);
            }
        }

        public static void read_range_file(string wdcountry)
        {
            string filename = geonamesfolder + @"ranges\ranges-" + wdcountry + ".txt";
            rangedict.Clear();
            try
            {
                int nranges = 0;
                using (StreamReader sr = new StreamReader(filename))
                {
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        string[] words = line.Split(tabchar);
                        //sw.Write(gnid.ToString() + tabstring + area.ToString() + tabstring + kmew.ToString() + tabstring + kmns.ToString());
                        //foreach (int oi in onrange)
                        //    sw.Write(tabstring + oi.ToString());
                        //sw.WriteLine();

                        if (words.Length < 6)
                            continue;

                        int gnid = tryconvert(words[0]);
                        if (!gndict.ContainsKey(gnid))
                            continue;

                        double length = tryconvertdouble(words[1]);
                        rangeclass isl = new rangeclass();
                        isl.length = length;

                        double scale = Math.Cos(gndict[gnid].latitude * 3.1416 / 180);
                        double pixkmx = scale * 40000 / (360 * 1200);
                        //double pixkmy = 40000.0 / (360.0 * 1200.0);

                        //public class rangeclass //data for each MTS/HLLS
                        //{
                        //    public double length = 0;
                        //    public string orientation = "....";
                        //    public double angle = 0; //polar angle of long axis (radians). 0 or pi = EW, pi/2 or 3pi/2 = NS etc.
                        //    public double kmew = 0;
                        //    public double kmns = 0;
                        //    public int maxheight = 0; //highest point; gnid of peak if negative, height if positive
                        //    public double hlat = 999; //latitude/longitude of highest point
                        //    public double hlon = 999;
                        //    public List<int> inrange = new List<int>(); //list of GeoNames id of mountains in the range.
                        //}


                        isl.kmew = tryconvertdouble(words[2]);
                        isl.kmns = tryconvertdouble(words[3]);
                        isl.angle = tryconvertdouble(words[4]);
                        isl.maxheight = tryconvert(words[5]);
                        isl.hlat = tryconvertdouble(words[6]);
                        isl.hlon = tryconvertdouble(words[7]);

                        for (int i = 8; i < words.Length; i++)
                        {
                            int oi = tryconvert(words[i]);
                            if (gndict.ContainsKey(oi))
                            {
                                isl.inrange.Add(oi);
                            }
                        }
                        //if (rangedict.ContainsKey(otherrange))
                        //    rangedict.Remove(otherrange);
                        //else
                        rangedict.Add(gnid, isl);
                        nranges++;
                    }
                }

                Console.WriteLine("# ranges = " + nranges.ToString());


                foreach (int gnid in rangedict.Keys)
                {
                    foreach (int oi in rangedict[gnid].inrange)
                    {
                        gndict[oi].inrange = gnid;
                    }
                }


            }
            catch (IOException e)
            {
                string message = e.Message;
                Console.Error.WriteLine(message);
            }
        }

        public static void read_islands(string countrycode)
        {
            if (countrycode == "")
            {
                foreach (int gnid in countrydict.Keys)
                    read_island_file(countrydict[gnid].iso);
            }
            else
                read_island_file(countrycode);
        }

        public static void read_ranges(string countrycode)
        {
            if (countrycode == "")
            {
                foreach (int gnid in countrydict.Keys)
                    read_range_file(countrydict[gnid].iso);
            }
            else
                read_range_file(countrycode);
        }


        public static void read_lake_file(string wdcountry)
        {
            string filename = geonamesfolder + @"lakes\lakes-" + wdcountry + ".txt";
            lakedict.Clear();
            try
            {
                int nlakes = 0;
                using (StreamReader sr = new StreamReader(filename))
                {
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        string[] words = line.Split(tabchar);
                        //sw.Write(gnid.ToString() + tabstring + area.ToString() + tabstring + kmew.ToString() + tabstring + kmns.ToString());
                        //foreach (int oi in onisland)
                        //    sw.Write(tabstring + oi.ToString());
                        //sw.WriteLine();

                        if (words.Length < 6)
                            continue;

                        int gnid = tryconvert(words[0]);
                        if (!gndict.ContainsKey(gnid))
                            continue;

                        double area = tryconvertdouble(words[1]);
                        if ((area > 0) && (gndict[gnid].area <= 0))
                            gndict[gnid].area = area;

                        lakeclass lake = new lakeclass();
                        lake.area = area;

                        double scale = Math.Cos(gndict[gnid].latitude * 3.1416 / 180);
                        double pixkmx = scale * 40000 / (360 * 1200);
                        double pixkmy = 40000.0 / (360.0 * 1200.0);

                        lake.kmew = tryconvertdouble(words[2]);
                        lake.kmns = tryconvertdouble(words[3]);
                        if (verifylakes)
                        {
                            lake.kmew += pixkmx;
                            lake.kmns += pixkmy;
                        }

                        lake.higher = tryconvert(words[4]);
                        lake.lower = tryconvert(words[5]);

                        if (words.Length < 9)
                            continue;
                        lake.overlaps_with = tryconvert(words[6]);

                        lake.glwd_id = tryconvert(words[7]);
                        lake.glwd_area = tryconvertdouble(words[8]);

                        int iw = 8;
                        while ((iw < words.Length) && (words[iw] != "around"))
                        {
                            int ii = tryconvert(words[iw]);
                            if (gndict.ContainsKey(ii))
                                lake.inlake.Add(ii);
                            iw++;
                        }
                        while (iw < words.Length)
                        {
                            int ii = tryconvert(words[iw]);
                            if (gndict.ContainsKey(ii))
                                lake.atlake.Add(ii);
                            iw++;
                        }

                        lakedict.Add(gnid, lake);
                        nlakes++;
                    }
                }

                Console.WriteLine("# lakes = " + nlakes.ToString());

                Dictionary<int, int> oindex = new Dictionary<int, int>();
                //List<int> badlist = new List<int>();
                Dictionary<int, List<int>> badlist = new Dictionary<int, List<int>>();

                //identify stuff that's listed as on two lakes:
                foreach (int gnid in lakedict.Keys)
                {
                    //first add lake itself as "in" itself...
                    if (oindex.ContainsKey(gnid))
                    {
                        if (!badlist.ContainsKey(oindex[gnid]))
                        {
                            List<int> bl = new List<int>();
                            badlist.Add(oindex[gnid], bl);
                        }
                        if (!badlist[oindex[gnid]].Contains(gnid))
                            badlist[oindex[gnid]].Add(gnid);
                    }
                    else
                        oindex.Add(gnid, gnid);
                    //... then add everything else in the lake.
                    foreach (int oi in lakedict[gnid].inlake)
                    {
                        if (oindex.ContainsKey(oi))
                        {
                            if (!badlist.ContainsKey(oindex[oi]))
                            {
                                List<int> bl = new List<int>();
                                badlist.Add(oindex[oi], bl);
                            }
                            if (!badlist[oindex[oi]].Contains(gnid))
                                badlist[oindex[oi]].Add(gnid);
                        }
                        else
                            oindex.Add(oi, gnid);

                    }
                }

                if (verifylakes) //Go through and find best lake for stuff with double location,
                {                  //then make new lake file.
                    int nbad = 0;

                    foreach (int badi in badlist.Keys)
                    {
                        long bestdist = seed_center_dist(badi);
                        long best2dist = 99999999;
                        int best = badi;
                        foreach (int badg in badlist[badi])
                        {
                            long scdist = seed_center_dist(badg);
                            if (scdist < bestdist)
                            {
                                best2dist = bestdist;
                                bestdist = scdist;
                                best = badg;
                            }
                        }

                        if (bestdist * 3 > best2dist) //require 3 times better to "promote" one of the lakes
                            best = -1;

                        if (badi != best)
                        {
                            lakedict.Remove(badi);
                            nbad++;
                        }
                        foreach (int badg in badlist[badi])
                            if (badg != best)
                            {
                                lakedict.Remove(badg);
                                nbad++;
                            }
                    }

                    Console.WriteLine("# lakes = " + nlakes.ToString());
                    Console.WriteLine("# bad lakes = " + nbad.ToString());
                    using (StreamWriter sw = new StreamWriter("lakes-" + makecountry + ".txt"))
                    {

                        foreach (int gnid in lakedict.Keys)
                        {
                            Console.WriteLine(gndict[gnid].Name + "; " + lakedict[gnid].area.ToString() + "; " + lakedict[gnid].kmew.ToString() + "; " + lakedict[gnid].kmns.ToString() + "; " + lakedict[gnid].inlake.Count.ToString() + "; " + lakedict[gnid].atlake.Count.ToString());
                            sw.Write(gnid.ToString() + tabstring + lakedict[gnid].area.ToString() + tabstring + lakedict[gnid].kmew.ToString() + tabstring + lakedict[gnid].kmns.ToString() + tabstring + "in");
                            foreach (int il in lakedict[gnid].inlake)
                            {
                                sw.Write(tabstring + il.ToString());
                                //Console.WriteLine(gndict[il].Name + " in lake");
                            }
                            sw.Write(tabstring + "around");
                            foreach (int al in lakedict[gnid].atlake)
                            {
                                sw.Write(tabstring + al.ToString());
                                //Console.WriteLine(gndict[al].Name + " around lake");
                            }
                            sw.WriteLine();
                        }
                    }
                }
                else //just remove the duplicate lakes
                {
                    foreach (int badi in badlist.Keys)
                    {
                        lakedict.Remove(badi);
                        foreach (int badg in badlist[badi])
                            lakedict.Remove(badg);
                    }
                }


                foreach (int gnid in lakedict.Keys)
                {
                    foreach (int ii in lakedict[gnid].inlake)
                    {
                        gndict[ii].inlake = gnid;
                    }
                    foreach (int ai in lakedict[gnid].atlake)
                    {
                        gndict[ai].atlakes.Add(gnid);
                    }
                }


            }
            catch (IOException e)
            {
                string message = e.Message;
                Console.Error.WriteLine(message);
            }
        }

        public static void read_lakes(string countrycode)
        {
            if (countrycode == "")
            {
                foreach (int gnid in countrydict.Keys)
                    read_lake_file(countrydict[gnid].iso);
            }
            else
                read_lake_file(countrycode);
        }

        public static void read_altitude_file(string wdcountry)
        {
            string filename = geonamesfolder + @"altitudes\altitude-" + wdcountry + ".txt";
            try
            {
                int nalt = 0;
                using (StreamReader sr = new StreamReader(filename))
                {
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        string[] words = line.Split(tabchar);

                        if (words.Length < 2)
                            continue;

                        int gnid = tryconvert(words[0]);
                        if (!gndict.ContainsKey(gnid))
                            continue;

                        int altitude = tryconvert(words[1]);

                        if (altitude < 9000)
                        {
                            gndict[gnid].elevation_vp = altitude;
                            if (gndict[gnid].elevation <= 0)
                                gndict[gnid].elevation = altitude;
                        }
                        nalt++;
                    }
                }

                Console.WriteLine("# altitudes = " + nalt.ToString());



            }
            catch (IOException e)
            {
                string message = e.Message;
                Console.Error.WriteLine(message);
            }
        }

        public static void read_altitudes(string countrycode)
        {
            if (countrycode == "")
            {
                foreach (int gnid in countrydict.Keys)
                    read_altitude_file(countrydict[gnid].iso);
            }
            else
                read_altitude_file(countrycode);
        }

        public static void read_altnames()
        {
            int n = 0;
            int nbad = 0;
            Console.WriteLine("read_altnames");
            string filename = geonamesfolder + "alternateNames.txt";

            using (StreamReader sr = new StreamReader(filename))
            {

                //public class altnameclass
                //{
                //    public int altid = 0;
                //    public string altname = "";
                //    public int ilang = 0;
                //    public string wikilink = "";
                //    public bool official = false;
                //    public bool shortform = false;
                //    public bool colloquial = false;
                //    public bool historic = false;
                //}

                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();

                    bool goodname = false;

                    string[] words = line.Split(tabchar);

                    altnameclass an = new altnameclass();

                    an.altid = tryconvert(words[0]);

                    int gnid = tryconvert(words[1]);

                    if (!checkdoubles && !gndict.ContainsKey(gnid))
                        continue;

                    int country = -1;
                    if (gndict.ContainsKey(gnid))
                        country = gndict[gnid].adm[0];

                    //if (gnid == 3039154)
                    //    Console.WriteLine(line);

                    if (words[2] == "link")
                    {
                        an.wikilink = words[3];
                        goodname = true;
                    }
                    else if (words[2] == "iata")
                    {
                        if (!iatadict.ContainsKey(gnid))
                            iatadict.Add(gnid, words[3]);
                    }
                    else if (words[2] == "icao")
                    {
                        if (!icaodict.ContainsKey(gnid))
                            icaodict.Add(gnid, words[3]);
                    }
                    else
                    {
                        an.altname = initialcap(words[3]);

                        if (langtoint.ContainsKey(words[2]))
                            an.ilang = langtoint[words[2]];
                        else if (langtoint.ContainsKey(words[2].Split('-')[0]))
                            an.ilang = langtoint[words[2].Split('-')[0]];
                        else if (words[2].Length > 3)
                        {
                            an.ilang = -1;
                            continue;
                        }
                        //Console.WriteLine("lang:" + an.ilang.ToString() + ", " + words[2]);


                        if (words[2] == makelang)
                        {
                            if (gndict.ContainsKey(gnid))
                                if (!gndict[gnid].articlename.Contains("*"))
                                    gndict[gnid].Name_ml = words[3];
                        }

                        if (countrydict.ContainsKey(country))
                            if (countrydict[country].languages.Contains(an.ilang))
                                goodname = true;

                        string script = get_alphabet(an.altname);

                        if ((an.ilang < 0) && (script == "latin"))
                            goodname = true;

                        if (((makecountry == "CN") || (makecountry == "JP") || (makecountry == "TW")) && (script == "chinese/japanese"))
                            goodname = true;

                    }




                    if ((words.Length > 4) && (words[4] == "1"))
                        an.official = true;
                    if ((words.Length > 5) && (words[5] == "1"))
                        an.shortform = true;
                    if ((words.Length > 6) && (words[6] == "1"))
                        an.colloquial = true;
                    if ((words.Length > 7) && (words[7] == "1"))
                        an.historic = true;

                    //if (an.official || an.shortform || an.colloquial || an.historic)
                    //    goodname = true;

                    //if (langdict.ContainsKey(an.ilang))
                    //    Console.Write(langdict[an.ilang].iso2 + ":");
                    //else
                    //    Console.Write("--:");
                    //Console.WriteLine(an.altname + ": is_latin = " + is_latin(an.altname).ToString()+", goodname = "+goodname.ToString());

                    if (goodname)
                    {
                        if (!altdict.ContainsKey(gnid))
                        {
                            List<altnameclass> anl = new List<altnameclass>();
                            altdict.Add(gnid, anl);
                        }
                        altdict[gnid].Add(an);
                        if (!String.IsNullOrEmpty(an.altname))
                            n++;
                    }
                    else
                        nbad++;


                    if ((n % 1000000) == 0)
                    {
                        Console.WriteLine("n (altnames)   = " + n.ToString());
                        if (n >= 1000000000)
                            break;
                    }

                }

                Console.WriteLine("n    (altnames)= " + n.ToString());
                Console.WriteLine("nbad (altnames)= " + nbad.ToString());

                if (statisticsonly)
                {
                    hbookclass althist = new hbookclass();
                    hbookclass altuniquehist = new hbookclass();

                    foreach (int gnid in altdict.Keys)
                    {
                        althist.Add(altdict[gnid].Count);
                        List<string> al = new List<string>();
                        foreach (altnameclass ac in altdict[gnid])
                        {
                            if (!al.Contains(ac.altname))
                                al.Add(ac.altname);
                        }
                        altuniquehist.Add(al.Count);
                        if (al.Count > 100)
                        {
                            Console.Write(al.Count.ToString() + ": ");
                            foreach (string s in al)
                                Console.Write(s + " | ");
                            Console.WriteLine();
                        }
                    }

                    althist.PrintIHist();
                    altuniquehist.PrintIHist();

                    Console.ReadLine();
                }
            }

            if (makelang == "sv")
            {
                if (makecountry == "")
                {
                    read_translit("BG");
                    read_translit("BY");
                    read_translit("KZ");
                    read_translit("MK");
                    read_translit("RS");
                    read_translit("RU");
                    read_translit("UA");
                    read_translit("MN");
                    read_translit("KG");
                    read_translit("TJ");
                    read_translit("RS");

                }
                else
                    read_translit(makecountry);
            }

            read_handtranslated();

        }

        public static void read_handtranslated()
        {

            string filename = geonamesfolder + @"handtranslated-" + makelang + ".txt";
            int n = 0;
            if (File.Exists(filename))
            {
                Console.WriteLine("read_handtranslated " + filename);
                using (StreamReader sr = new StreamReader(filename))
                {

                    //public class altnameclass
                    //{
                    //    public int altid = 0;
                    //    public string altname = "";
                    //    public int ilang = 0;
                    //    public string wikilink = "";
                    //    public bool official = false;
                    //    public bool shortform = false;
                    //    public bool colloquial = false;
                    //    public bool historic = false;
                    //}

                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();

                        //bool goodname = false;

                        string[] words = line.Split(tabchar);

                        if (words.Length < 2)
                            continue;

                        altnameclass an = new altnameclass();

                        an.altid = -1;

                        int gnid = tryconvert(words[0]);

                        if (!checkdoubles && !gndict.ContainsKey(gnid))
                            continue;

                        an.altname = remove_disambig(words[1].Replace("*", ""));

                        an.ilang = langtoint[makelang];

                        if (gndict.ContainsKey(gnid))
                        {
                            gndict[gnid].Name_ml = an.altname;
                            if (words[1].Contains("*"))
                                gndict[gnid].articlename = words[1];
                        }

                        //else
                        {
                            if (!artnamedict.ContainsKey(gnid))
                                artnamedict.Add(gnid, words[1]);
                            else if (!artnamedict[gnid].Contains("*"))
                                artnamedict[gnid] = words[1];

                        }




                        if (!altdict.ContainsKey(gnid))
                        {
                            List<altnameclass> anl = new List<altnameclass>();
                            altdict.Add(gnid, anl);
                        }
                        altdict[gnid].Add(an);
                        if (!String.IsNullOrEmpty(an.altname))
                            n++;

                    }
                }
                Console.WriteLine("Names found in handtranslated: " + n.ToString());
            }
            else
                Console.WriteLine("File not found! " + filename);
        }

        public static void read_translit(string tlcountry)
        {

            string filename = geonamesfolder + @"translit\translit-" + tlcountry + ".txt";
            int n = 0;
            if (File.Exists(filename))
            {
                Console.WriteLine("read_translit " + filename);
                using (StreamReader sr = new StreamReader(filename))
                {

                    //public class altnameclass
                    //{
                    //    public int altid = 0;
                    //    public string altname = "";
                    //    public int ilang = 0;
                    //    public string wikilink = "";
                    //    public bool official = false;
                    //    public bool shortform = false;
                    //    public bool colloquial = false;
                    //    public bool historic = false;
                    //}

                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();

                        //bool goodname = false;

                        string[] words = line.Split(tabchar);

                        if (words.Length < 3)
                            continue;

                        altnameclass an = new altnameclass();

                        an.altid = -1;

                        int gnid = tryconvert(words[0]);

                        if (!checkdoubles && !gndict.ContainsKey(gnid))
                            continue;

                        an.altname = words[2];

                        an.ilang = langtoint["sv"];

                        if (makelang == "sv")
                        {
                            if (gndict.ContainsKey(gnid))
                            {
                                if (gndict[gnid].Name_ml == gndict[gnid].Name)
                                    gndict[gnid].Name_ml = an.altname;
                            }

                        }

                        if (!altdict.ContainsKey(gnid))
                        {
                            List<altnameclass> anl = new List<altnameclass>();
                            altdict.Add(gnid, anl);
                        }
                        altdict[gnid].Add(an);
                        if (!String.IsNullOrEmpty(an.altname))
                            n++;

                    }
                }
            }
            else
                Console.WriteLine("File not found! " + filename);
            Console.WriteLine("Names found in translit: " + n.ToString());
        }

        public static string getdatestring()
        {
            DateTime thismonth = DateTime.Now;
            string monthstring = thismonth.Month.ToString();
            while (monthstring.Length < 2)
                monthstring = "0" + monthstring;
            string daystring = thismonth.Day.ToString();
            while (daystring.Length < 2)
                daystring = "0" + daystring;
            return thismonth.Year.ToString() + monthstring + daystring;
        }

        public static void list_nameforks()
        {
            int nfork = 0;
            int nfork2 = 0;
            int nfork10 = 0;
            int nfork100 = 0;
            int nfork500 = 0;
            int nfork1000 = 0;
            int maxfork = 0;
            string maxforkname = "xxx";
            //string datestring = getdatestring();

            Console.WriteLine("list_nameforks");

            using (StreamWriter sw = new StreamWriter("namefork-" + getdatestring() + ".csv"))
            {

                foreach (string s in namefork.Keys)
                {
                    if (namefork[s].Count > 0)
                    {
                        nfork++;
                        if (namefork[s].Count > maxfork)
                        {
                            maxfork = namefork[s].Count;
                            maxforkname = s;
                        }
                        if (namefork[s].Count >= 2)
                            nfork2++;
                        if (namefork[s].Count >= 10)
                            nfork10++;
                        if (namefork[s].Count >= 100)
                            nfork100++;
                        if (namefork[s].Count >= 500)
                            nfork500++;
                        if (namefork[s].Count >= 1000)
                            nfork1000++;

                        sw.WriteLine(s);
                        foreach (int i in namefork[s])
                        {
                            if (!gndict.ContainsKey(i))
                            {
                                sw.WriteLine("Bad geonameid " + i.ToString());
                                continue;
                            }
                            sw.Write(i.ToString() + ";" + gndict[i].featurecode + ";");
                            if (countrydict.ContainsKey(gndict[i].adm[0]))
                            {
                                //Console.WriteLine(gndict[i].adm[0].ToString() + " " + countrydict[gndict[i].adm[0]].Name);
                                sw.Write(countrydict[gndict[i].adm[0]].Name);
                            }
                            else if (gndict.ContainsKey(gndict[i].adm[0]))
                                sw.Write(gndict[gndict[i].adm[0]].Name);
                            sw.Write(";");
                            if (gndict.ContainsKey(gndict[i].adm[1]))
                                sw.Write(gndict[gndict[i].adm[1]].Name_ml);
                            sw.Write(";");
                            if (gndict.ContainsKey(gndict[i].adm[2]))
                                sw.Write(gndict[gndict[i].adm[2]].Name_ml);
                            sw.Write(";");
                            sw.Write(gndict[i].latitude.ToString() + ";" + gndict[i].longitude.ToString() + ";");
                            if (gndict[i].Name_ml == s)
                                sw.Write("*");
                            else
                                sw.Write(gndict[i].Name_ml);
                            sw.Write(";" + gndict[i].wdid.ToString());
                            sw.WriteLine();
                        }
                        sw.WriteLine("#");
                        //sw.WriteLine();
                    }
                }

            }
            Console.WriteLine("nfork = " + nfork.ToString());
            Console.WriteLine("maxfork = " + maxfork.ToString());
            Console.WriteLine("maxforkname = |" + maxforkname + "|");
            Console.WriteLine("nfork2 = " + nfork2.ToString());
            Console.WriteLine("nfork10 = " + nfork10.ToString());
            Console.WriteLine("nfork100 = " + nfork100.ToString());
            Console.WriteLine("nfork500 = " + nfork100.ToString());
            Console.WriteLine("nfork1000 = " + nfork100.ToString());
        }

        public static PageList get_geotemplates()
        {
            PageList pl = new PageList(makesite);
            pl.FillAllFromCategoryTree(mp(74));
            //Skip all namespaces except templates (ns = 10):
            Console.WriteLine("pl.Count = " + pl.Count().ToString());
            //pl.RemoveNamespaces(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 11, 12, 13, 14, 15, 100, 101 });
            Console.WriteLine("pl.Count = " + pl.Count().ToString());

            coordparams.Add("coord");
            coordparams.Add("Coord");
            coordparams.Add("lat_d");
            coordparams.Add("lat_g");
            coordparams.Add("latitude");
            coordparams.Add("latitud");

            return pl;
        }


        public static double coordlat(string coordstring)
        {
            //{{Coord|42|33|18|N|1|31|59|E|region:AD_type:city|display=title,inline}}

            string[] cs = coordstring.Split('|');

            if (cs.Length <= 2)
                return 9999.9;
            else
            {
                int ins = -1;
                int iew = -1;
                int iregion = -1;
                for (int i = 1; i < cs.Length; i++)
                {
                    if ((cs[i].ToUpper() == "N") || (cs[i].ToUpper() == "S"))
                        ins = i;
                    if ((cs[i].ToUpper() == "E") || (cs[i].ToUpper() == "W"))
                        iew = i;
                    if (cs[i].ToLower().Contains("region"))
                        iregion = i;
                }
                if (ins < 0)
                    return tryconvertdouble(cs[1]);
                else
                {
                    double lat = 0.0;
                    double scale = 1.0;
                    for (int i = 1; i < ins; i++)
                    {
                        double lx = tryconvertdouble(cs[i]);
                        if (lx < 90.0)
                            lat += lx / scale;
                        scale *= 60.0;
                    }
                    if (cs[ins].ToUpper() == "S")
                        lat = -lat;
                    return lat;
                }
            }
            //else if (cs.Length < 9)
            //{
            //    return tryconvertdouble(cs[1]);
            //}
            //else
            //{
            //    double lat = tryconvertdouble(cs[1]) + tryconvertdouble(cs[2]) / 60 + tryconvertdouble(cs[3]) / 3600;
            //    if (cs[4].ToUpper() == "S")
            //        lat = -lat;
            //    return lat;
            //}
        }

        public static double coordlong(string coordstring)
        {
            //{{Coord|42|33|18|N|1|31|59|E|region:AD_type:city|display=title,inline}}

            string[] cs = coordstring.Split('|');
            if (cs.Length <= 2)
                return 9999.9;
            else
            {
                int ins = -1;
                int iew = -1;
                int iregion = -1;
                for (int i = 1; i < cs.Length; i++)
                {
                    if ((cs[i].ToUpper() == "N") || (cs[i].ToUpper() == "S"))
                        ins = i;
                    if ((cs[i].ToUpper() == "E") || (cs[i].ToUpper() == "W"))
                        iew = i;
                    if (cs[i].ToLower().Contains("region"))
                        iregion = i;
                }
                if (iew < 0)
                    return tryconvertdouble(cs[2]);
                else
                {
                    double lon = 0.0;
                    double scale = 1.0;
                    for (int i = ins + 1; i < iew; i++)
                    {
                        double lx = tryconvertdouble(cs[i]);
                        if (lx < 180.0)
                            lon += lx / scale;
                        scale *= 60.0;
                    }
                    if (cs[iew].ToUpper() == "W")
                        lon = -lon;
                    return lon;
                }
            }
            //else
            //{
            //    double lon = tryconvertdouble(cs[5]) + tryconvertdouble(cs[6]) / 60 + tryconvertdouble(cs[7]) / 3600;
            //    if (cs[8].ToUpper() == "W")
            //        lon = -lon;
            //    return lon;
            //}
        }


        public static string removearticle(string s)
        {
            string rs = s;
            if (makelang == "sv")
            {
                if (s.IndexOf("en ") == 0)
                    rs = s.Remove(0, 3);
                else if (s.IndexOf("ett ") == 0)
                    rs = s.Remove(0, 4);
            }
            else if (makelang == "ceb")
            {
                if (s.IndexOf("ang ") == 0)
                    rs = s.Remove(0, 4);

            }
            return rs;
        }

        public static string getwikilink(string s)
        {
            int i1 = s.IndexOf("[[");
            int i2 = s.IndexOf("]]");
            if (i1 < 0)
                return "";
            if (i2 < i1 + 2)
                return "";

            return s.Substring(i1 + 2, i2 - i1 - 2);

        }

        public static void fill_featurearticle()
        {
            if (featurearticle.ContainsKey("vik"))
                return;
            if (makelang == "sv")
            {
                featurearticle.Add("vik", "havsvik");
                featurearticle.Add("samhälle", "samhälle (geografi)");
                featurearticle.Add("udde", "halvö");
                featurearticle.Add("ö", "ö (landområde)");
                featurearticle.Add("kulle", "kulle (landform)");
                featurearticle.Add("del av lagun", "lagun");
                //featurearticle.Add("periodisk sjö", "sjö");
                //featurearticle.Add("periodisk saltsjö", "saltsjö");
                //featurearticle.Add("periodisk korvsjö", "korvsjö");
                featurearticle.Add("periodiska sjöar", "periodisk sjö");
                featurearticle.Add("periodiska saltsjöar", "periodisk saltsjö");
                featurearticle.Add("saltsjöar", "saltsjö");
                featurearticle.Add("del av en sjö", "sjö");
                featurearticle.Add("del av ett rev", "rev");
                featurearticle.Add("trångt sund", "sund");
                //featurearticle.Add("saltträsk", "våtmark");
                featurearticle.Add("fors", "fors (vattendrag)");
                featurearticle.Add("periodisk reservoar", "vattenmagasin");
                featurearticle.Add("sabkha", "saltöken");
                featurearticle.Add("grund", "grund (sjöfart)");
                featurearticle.Add("källa", "vattenkälla");
                featurearticle.Add("periodiskt vattendrag", "vattendrag");
                featurearticle.Add("wadier", "wadi");
                featurearticle.Add("periodisk våtmark", "våtmark");
                featurearticle.Add("reservat", "indianreservat");
                featurearticle.Add("övergiven gruva", "gruva");
                featurearticle.Add("kitteldalar", "kitteldal");
                featurearticle.Add("sänka", "bäcken (geografi)");
                featurearticle.Add("klippöken", "öken");
                featurearticle.Add("del av en ö", "ö (landområde)");
                featurearticle.Add("karstområde", "karst");
                featurearticle.Add("höjd", "kulle (landform)");
                featurearticle.Add("undervattenshöjd", "kulle (landform)");
                featurearticle.Add("morän", "morän (landform)");
                featurearticle.Add("nunataker", "nunatak");
                featurearticle.Add("sänkor", "bäcken (geografi)");
                featurearticle.Add("del av halvö", "halvö");
                featurearticle.Add("bergstoppar", "bergstopp");
                featurearticle.Add("klippa", "klippa (geologi)");
                featurearticle.Add("åmynning", "flodmynning");
                featurearticle.Add("militärt övningsområde", "övningsfält");
                featurearticle.Add("mangroveö", "mangrove");
                featurearticle.Add("del av en halvö", "halvö");
                featurearticle.Add("del av en platå", "platå");
                featurearticle.Add("del av en slätt", "slätt");
                featurearticle.Add("uddar", "halvö");
                featurearticle.Add("stenöken", "öken");
                featurearticle.Add("bankar", "sandbank");
                featurearticle.Add("bank", "sandbank");
                featurearticle.Add("sandbankar", "sandbank");
                featurearticle.Add("dalar", "dal");
                //featurearticle.Add("sadelpass", "bergspass");
                featurearticle.Add("del av en lagun", "lagun");
                featurearticle.Add("del av en ort", "stadsdel");
                featurearticle.Add("delta", "floddelta");
                featurearticle.Add("plattform", "massrörelse (geologi)");
                featurearticle.Add("veckterräng", "veckning");
                featurearticle.Add("bassäng", "bassäng (geologi)");
                featurearticle.Add("kanjoner", "kanjon");
                featurearticle.Add("fossil skog", "förstenat trä");
                featurearticle.Add("åsar", "ås");
                featurearticle.Add("undervattensåsar", "ås");
                featurearticle.Add("undervattensås", "ås");
                featurearticle.Add("orter", "ort");
                featurearticle.Add("hög udde", "halvö");
                featurearticle.Add("del av en dal", "dal");
                featurearticle.Add("liten undervattenskulle", "kulle (landform)");
                featurearticle.Add("små undervattenskullar", "kulle (landform)");
                featurearticle.Add("undervattenskulle", "kulle (landform)");
                featurearticle.Add("undervattenskullar", "kulle (landform)");
                featurearticle.Add("tröskel", "tröskelfjord");
                featurearticle.Add("kontinentalsluttning", "kontinentalbranten");
                featurearticle.Add("undervattensdal", "dal");
                featurearticle.Add("undervattensdalar", "dal");
                featurearticle.Add("utlöpare", "utlöpare (landform)");
                featurearticle.Add("guyoter", "guyot");
                featurearticle.Add("terass", "terass (landform)");
                featurearticle.Add("å", "å (vattendrag)");
                featurearticle.Add("klint", "klint (landform)");
                //featurearticle.Add("ekonomisk region", "ekonomisk region (Finland)");
                if (makecountry == "CN")
                {
                    featurearticle.Add("köping", "Kinas köpingar");
                    featurearticle.Add("socken", "Kinas socknar");
                }

                featurearticle.Add("stup", "stup (berg)");
                featurearticle.Add("", "");

            }
        }

        public static string comment(string incomment)
        {
            return "<!--" + incomment + "-->";
        }

        public static List<string> getcomments(string text)
        {
            List<string> rl = new List<string>();
            Match m;
            Regex HeaderPattern = new Regex("<!--(.+?)-->");

            try
            {
                m = HeaderPattern.Match(text);
                while (m.Success)
                {
                    //Console.WriteLine("Found comment " + m.Groups[1] + " at " + m.Groups[1].Index);
                    rl.Add(m.Groups[1].Value);
                    m = m.NextMatch();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return rl;

        }

        public static string get_fcode(string text)
        {
            List<string> comments = getcomments(text);
            foreach (string c in comments)
            {
                string[] c2 = c.Split('.');
                if (c2.Length == 2)
                {
                    if (c2[0].Length == 1)
                        if (c2[1].ToUpper() == c2[1])
                            return c2[1];
                }
            }

            return "";
        }



        public static string linkfeature(string fcode, int gnid)
        {
            fill_featurearticle();
            string s = getfeaturelabelindet(makecountry, fcode, gnid);
            Console.WriteLine("linkfeature " + fcode + " " + s);

            //if (fcode.Contains("ADM"))
            //{
            //    //Console.WriteLine("linkfeature ADM");
            //    int admlevel = -1;
            //    if (fcode.Length >= 4)
            //        admlevel = tryconvert(fcode.Substring(3, 1));
            //    if (admlevel > 0)
            //        s = getadmindet(makecountry, admlevel,gnid);
            //}

            string rs = s;

            if (makelang == "sv")
            {
                if (s.IndexOf("en ") == 0)
                    rs = s.Insert(3, "[[");
                //rs = s.Replace("en ", "en [[");
                else if (s.IndexOf("ett ") == 0)
                    rs = s.Insert(4, "[[");
                //rs = s.Replace("ett ", "ett [[");
            }
            else if (makelang == "ceb")
            {
                if (s.IndexOf("ang ") == 0)
                    rs = s.Insert(4, "[[");
                if (s.IndexOf("mga ") == 0)
                    rs = s.Insert(4, "[[");

            }

            if (!rs.Contains("[["))
                rs = "[[" + rs;

            rs = rs + "]]";

            Console.WriteLine("rs = " + rs);
            string gw = getwikilink(rs);
            Console.WriteLine("gw = " + gw);
            if (featurearticle.ContainsKey(gw))
                rs = rs.Replace(gw, featurearticle[gw] + "|" + gw);

            rs = comment(featureclassdict[fcode].ToString() + "." + fcode) + rs;

            if (rs.Contains("{{")) //Don't link if label contains template
            {
                rs = rs.Replace("[[", "").Replace("]]", "");
                if ( rs.Contains(mp(174,null)))
                    hasnotes = true;
            }

            return rs;
        }

        public static string fix_artname(string artname)
        {
            string rs = artname;
            if (funny_quotes.Count == 0)
            {
                funny_quotes.Add("„", "“");//„...“ (makedonska m.m.)
                funny_quotes.Add("“", "”");//“…” (engelska m.m.) 
                funny_quotes.Add("«", "»");//«…» (franska m.m.)

            }

            foreach (string q1 in funny_quotes.Keys)
            {
                if (rs.Contains(q1) && rs.Contains(funny_quotes[q1]))
                    rs = rs.Replace(q1, "\"").Replace(funny_quotes[q1], "\"");
            }

            rs = rs.Replace("’", "'");
            rs = rs.Replace("[", "").Replace("]", "");
            rs = rs.Replace("{", "").Replace("}", "");

            bool hascomma = rs.Contains(",");
            rs = Regex.Replace(rs, @"[\u0000-\u001F]|[\u00AD]", string.Empty); //Ta bort nonprintable. \u00AD är mjukt bindestreck.
            bool stillhascomma = rs.Contains(",");
            if (hascomma != stillhascomma)
            {
                Console.WriteLine(rs);
                Console.WriteLine("Comma removed <cr>");
                Console.ReadLine();
            }
            return rs;
        }

        public static void fix_names()
        {
            foreach (int gnid in gndict.Keys)
            {
                gndict[gnid].Name = fix_artname(gndict[gnid].Name);
                gndict[gnid].Name_ml = fix_artname(gndict[gnid].Name_ml);
            }
        }

        public static void read_artname2_file(string filename)
        {
            int nartname = 0;
            using (StreamReader sr = new StreamReader(geonamesfolder+filename))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    string[] words = s.Split(tabchar);
                    if (words.Length < 2)
                        continue;
                    nartname++;
                    int gnid = tryconvert(words[0]);
                    string aname = fix_artname(words[1]);

                    if ((gndict.Count > 0) && !checkdoubles)
                    {
                        if (gndict.ContainsKey(gnid))
                        {
                            gndict[gnid].artname2 = aname.Replace("*", "");
                        }
                    }
                    if ((nartname % 1000000) == 0)
                        Console.WriteLine("nartname2 = " + nartname.ToString());
                }

            }
            Console.WriteLine("nartname2 = " + nartname.ToString());
        }

        public static void read_oldartname_file(string filename)
        {
            int nartname = 0;
            using (StreamReader sr = new StreamReader(geonamesfolder + filename))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    string[] words = s.Split(tabchar);
                    if (words.Length < 2)
                        continue;
                    nartname++;
                    int gnid = tryconvert(words[0]);
                    string aname = fix_artname(words[1]);

                    if ((gndict.Count > 0) && !checkdoubles)
                    {
                        if (gndict.ContainsKey(gnid))
                        {
                            if (gndict[gnid].articlename != aname)
                                gndict[gnid].oldarticlename = aname;
                        }
                    }
                    if (checkdoubles)
                    {
                        if (!oldartnamedict.ContainsKey(gnid))
                            oldartnamedict.Add(gnid, aname);
                    }

                    if ((nartname % 1000000) == 0)
                        Console.WriteLine("noldartname = " + nartname.ToString());
                }

            }
            Console.WriteLine("noldartname = " + nartname.ToString());
        }


        public static void read_artname_file(string filename)
        {
            int nartname = 0;
            int nparish = 0;
            Console.WriteLine("artname: " + filename);
            using (StreamReader sr = new StreamReader(geonamesfolder + filename))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    string[] words = s.Split(tabchar);
                    if (words.Length < 2)
                        continue;
                    nartname++;
                    int gnid = tryconvert(words[0]);
                    string aname = fix_artname(words[1]);

                    //if ((makecountry == "AZ") && (filename.Contains("missing")) && (makelang == "sv")) //kludge to get around weird Azerbadjan bug
                    //{
                    //    Page pz = new Page(makesite, aname.Replace("*", ""));
                    //    tryload(pz, 1);
                    //    if (is_fork(pz) && pz.text.Contains("Azerbajdzjan"))
                    //    {
                    //        aname += " (distrikt i Azerbajdzjan)";
                    //        Console.WriteLine(aname);
                    //    }
                    //}


                    if ((gndict.Count > 0) && !checkdoubles)
                    {
                        if (gndict.ContainsKey(gnid))
                        {
                            if (makecountry == "BE")
                            {
                                if (aname.Contains("(församling"))
                                {
                                    aname = aname.Replace("(församling)", "(kommun i Belgien)");
                                    aname = aname.Replace("(församling i", "(kommun i");
                                    aname = aname.Replace("(församling,", "(kommun i Belgien,");
                                    aname = aname.Replace("(församlingshuvudort", "(kommunhuvudort");
                                    if (aname.Contains("(församling"))
                                    {
                                        nparish++;
                                        Console.WriteLine(aname);
                                    }
                                }
                            }

                            if (makecountry == "CW")
                            {
                                if (aname.Contains("Curacao"))
                                {
                                    aname = aname.Replace("Curacao", "Curaçao");
                                }
                            }

                            if (makecountry == "FI")
                            {
                                if (aname.Contains("Åboland-Turunmaa"))
                                    aname = aname.Replace("Åboland-Turunmaa", "Åboland");
                            }

                            if (makecountry == "DE")
                            {
                                if (aname.Contains("Upper Palatinate"))
                                    aname = aname.Replace("Upper Palatinate", "Oberpfalz");
                                if (aname.Contains("Upper Bavaria"))
                                    aname = aname.Replace("Upper Bavaria", "Oberbayern");
                                if (aname.Contains("Upper Franconia"))
                                    aname = aname.Replace("Upper Franconia", "Oberfranken");
                                if (aname.Contains(" Swabia"))
                                    aname = aname.Replace(" Swabia", " Schwaben");
                            }


                            if ((!gndict[gnid].articlename.Contains("*")) || (aname.Contains("*")) || filename.Contains("missing"))
                            {
                                gndict[gnid].articlename = aname;
                                if (words[1] != aname)
                                    gndict[gnid].unfixedarticlename = words[1];
                            }
                            if ((aname.Contains("*")) || (gndict[gnid].Name == gndict[gnid].Name_ml) || filename.Contains("missing"))
                            {
                                gndict[gnid].Name_ml = remove_disambig(aname.Replace("*", ""));
                            }
                            if (aname.Contains("Östprovinsen"))
                                Console.WriteLine(gnid.ToString() + ": " + aname);
                        }
                    }
                    //else 
                    {
                        if (!artnamedict.ContainsKey(gnid))
                            artnamedict.Add(gnid, aname);
                        else if (!artnamedict[gnid].Contains("*"))
                            artnamedict[gnid] = aname;

                    }
                    if ((nartname % 1000000) == 0)
                        Console.WriteLine("nartname = " + nartname.ToString());
                }

            }
            Console.WriteLine("nartname = " + nartname.ToString());
            Console.WriteLine("nparish = " + nparish.ToString());
        }

        public static void read_artname()
        {
            Console.WriteLine("read_artname");
            string filename = "artname-" + makelang + ".txt"; //use current file
            if (checkdoubles)
                filename = "artname-" + makelang + "-checkwiki.txt"; //use the file that really was checked against wiki
            read_artname_file(filename);

            //fil med diverse handfixade namn
            read_artname_file("missing-adm1-toartname-" + makelang + ".txt");

            //Console.ReadLine();

            if (makelang == "sv")
                filename = "artname-ceb.txt";
            else
                filename = "artname-sv.txt";
            read_artname2_file(filename);

            //filename = "artname-"+makelang+"-old.txt";
            //read_oldartname_file(filename);
        }

        public static string remove_disambig(string title)
        {
            string tit = title;
            if (tit.IndexOf("(") > 0)
                tit = tit.Remove(tit.IndexOf("(")).Trim();
            else if (tit.IndexOf(",") > 0)
                tit = tit.Remove(tit.IndexOf(",")).Trim();
            //if (tit != title)
            //    Console.WriteLine(title + " |" + tit + "|");
            return tit;
        }

        public static bool is_disambig(string title)
        {
            return (title != remove_disambig(title));
        }

        public static void fill_donecountries()
        {
            if (makelang == "sv")
            {
                donecountries.Add("BT");
                donecountries.Add("AG");
                donecountries.Add("BH");
                donecountries.Add("MK");
                donecountries.Add("MT");
                donecountries.Add("SS");
                donecountries.Add("NI");
                donecountries.Add("LU");
            }
        }

        public static void check_doubles()
        {
            int ndouble = 0;
            int ndouble_ow = 0;
            int ndouble_cw = 0;
            int ndouble_coord = 0;
            int ndouble_iw = 0;
            int nfuzzy = 0;
            int nadm1 = 0;
            int nalldisambig = 0;
            List<string> fuzzylist = new List<string>();
            bool checkwiki = false;

            hbookclass scripthist = new hbookclass();


            Console.WriteLine("Check doubles");
            //PageList geolist = get_geotemplates();
            //foreach (Page p in geolist)
            //    Console.WriteLine("Template " + p.title);

            Dictionary<string, int> geotempdict = new Dictionary<string, int>();

            int np = 0;

            using (StreamWriter swfuzz = new StreamWriter("manualmatch-ADM1-" + makelang + "-" + getdatestring() + ".txt"))
            using (StreamWriter sw = new StreamWriter("artname-" + makelang + "-" + getdatestring() + ".txt"))
            {
                using (StreamReader sr = new StreamReader(geonamesfolder + "namefork-" + makelang + ".csv"))
                {
                    while (!sr.EndOfStream)
                    {
                        string s = sr.ReadLine();
                        s = s.Trim(';');

                        //scripthist.Add(get_alphabet(s));
                        if (get_alphabet(s) == "none")
                        {
                            Console.WriteLine("none:" + s + "|");
                            //Console.ReadLine();
                        }
                        List<forkclass> fl = new List<forkclass>();

                        string countryname = "";
                        string adm1name = "";
                        string adm2name = "";
                        string fcode = "";


                        int nrealnames = 0;
                        int nnames = 0;
                        int imakelang = langtoint[makelang];

                        while (true)
                        {
                            string line = sr.ReadLine();
                            if (line[0] == '#')
                                break;
                            string[] words = line.Split(';');

                            //public class forkclass
                            //{
                            //    public int geonameid = 0;
                            //    public string featurecode = "";
                            //    public string[] admname = new string[3];
                            //    public double latitude = 0.0;
                            //    public double longitude = 0.0;

                            //}

                            forkclass fc = new forkclass();
                            fc.geonameid = tryconvert(words[0]);
                            fc.featurecode = words[1];
                            fc.admname[0] = words[2];
                            if (countryml.ContainsKey(words[2]))
                                fc.admname[0] = countryml[words[2]];
                            if (countryiso.ContainsKey(words[2]))
                                fc.iso = countryiso[words[2]];
                            fc.admname[1] = words[3];
                            fc.admname[2] = words[4];
                            fc.latitude = tryconvertdouble(words[5]);
                            fc.longitude = tryconvertdouble(words[6]);
                            fc.realname = words[7];
                            fc.wdid = tryconvert(words[8]);
                            fc.featurename = getfeaturelabel(fc.iso, fc.featurecode, fc.geonameid);
                            if (altdict.ContainsKey(fc.geonameid))
                            {
                                foreach (altnameclass ac in altdict[fc.geonameid])
                                {
                                    if (ac.ilang == imakelang)
                                    {
                                        if (ac.altname == s)
                                            fc.realname = "*";
                                        else
                                            fc.realname = ac.altname;
                                    }
                                }
                            }
                            nnames++;
                            if (fc.realname == "*")
                            {
                                nrealnames++;
                            }
                            fl.Add(fc);
                            countryname = words[2];
                            adm1name = words[3];
                            adm2name = words[4];
                            fcode = words[1];
                        }

                        bool allsamecountry = true;
                        //bool allsamefcode = true;
                        bool allsameadm1 = true;
                        bool allsameadm2 = true;
                        bool somesamecountry = false;
                        bool somesamefcode = false;
                        bool somesameadm1 = false;
                        bool somesameadm2 = false;


                        foreach (forkclass ff in fl)
                        {
                            if (ff.realname == "*")
                            {
                                if (ff.admname[0] != countryname)
                                    allsamecountry = false;
                                if (ff.admname[1] != adm1name)
                                    allsameadm1 = false;
                                if (ff.admname[2] != adm2name)
                                    allsameadm2 = false;
                                //if (ff.featurecode != fcode)
                                //    allsamefcode = false;

                                if (String.IsNullOrEmpty(ff.admname[1]))
                                    somesameadm1 = true;
                                if (String.IsNullOrEmpty(ff.admname[2]))
                                    somesameadm2 = true;

                                foreach (forkclass ff2 in fl)
                                {
                                    if ((ff2.realname == "*") && (ff.geonameid != ff2.geonameid))
                                    {
                                        if (ff.admname[0] == ff2.admname[0])
                                            somesamecountry = true;
                                        if (ff.admname[1] == ff2.admname[1])
                                            somesameadm1 = true;
                                        if (ff.admname[2] == ff2.admname[2])
                                            somesameadm2 = true;
                                        if (ff.featurecode == ff2.featurecode)
                                            somesamefcode = true;
                                        if (ff.featurename == ff2.featurename)
                                            somesamefcode = true;
                                    }
                                }
                            }
                        }

                        if (nrealnames == 0)
                            continue;

                        //bool geotemplatefound = false;
                        bool coordfound = false;
                        bool pagefound = false;
                        string namefound = "";
                        double lat = 999.0;
                        double lon = 999.0;

                        //foreach (forkclass fc in fl)
                        //    if (fc.realname == "*")
                        //    {
                        //        List<int> enb = getexistingneighbors(fc.latitude, fc.longitude, 10.0);
                        //        foreach (int nb in enb)
                        //        {
                        //            if (existingdict.ContainsKey(nb))
                        //            {
                        //                if ( remove_disambig(existingdict[nb].articlename) == s)
                        //                {
                        //                    pagefound = true;
                        //                    coordfound = true;
                        //                    namefound = existingdict[nb].articlename;
                        //                    lat = fc.latitude;
                        //                    lon = fc.longitude;
                        //                    break;
                        //                }
                        //            }
                        //        }
                        //    }


                        if (checkwiki)
                        {
                            Page oldpage = new Page(makesite, s);
                            //Page forkpage = new Page(makesite, testprefix + s + " (" + mp(67) + ")");
                            if (tryload(oldpage, 1))
                            {
                                if (oldpage.Exists())
                                {
                                    pagefound = true;
                                    if (oldpage.IsRedirect())
                                    {
                                        oldpage.title = oldpage.RedirectsTo();
                                        tryload(oldpage, 1);
                                        pagefound = oldpage.Exists();
                                    }

                                    if (is_fork(oldpage))
                                        pagefound = false;

                                    if (pagefound)
                                    {
                                        //geotemplatefound = false;
                                        coordfound = false;
                                        double[] latlong = get_article_coord(oldpage);
                                        if (latlong[0] + latlong[1] < 720.0)
                                        {
                                            coordfound = true;
                                            Console.WriteLine(latlong[0].ToString() + "|" + latlong[1].ToString());
                                            lat = latlong[0];
                                            lon = latlong[1];
                                            namefound = oldpage.title;
                                            ndouble_cw++;
                                        }
                                    }
                                }
                            }
                        }

                        else //check against old artname-file
                        {
                            bool alldisambig = true;
                            foreach (forkclass fc in fl)
                                if (fc.realname == "*")
                                {
                                    if (artnamedict.ContainsKey(fc.geonameid))
                                    {
                                        if (artnamedict[fc.geonameid].Contains("*"))
                                        {
                                            pagefound = true;
                                            coordfound = true;
                                            namefound = artnamedict[fc.geonameid].Replace("*", "");
                                            lat = fc.latitude;
                                            lon = fc.longitude;
                                            alldisambig = false;
                                            ndouble_ow++;
                                            break;
                                        }
                                        else if (!artnamedict[fc.geonameid].Contains("("))
                                            alldisambig = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("gnid missing in artnamedict! " + s);
                                        //Console.ReadLine();
                                    }
                                }
                            if ((alldisambig) && (nrealnames == 1)) //page with that name exists but doesn't match any place
                            {
                                pagefound = true;
                                coordfound = false;
                                nalldisambig++;
                            }
                        }




                        Dictionary<int, Disambigclass> dadict = new Dictionary<int, Disambigclass>();
                        //public class disambigclass
                        //{
                        //    bool existsalready = false;
                        //    bool country = false;
                        //    bool adm1 = false;
                        //    bool adm2 = false;
                        //    bool latlong = false;
                        //    bool fcode = false;
                        //}

                        //Now we know if page exists:

                        if (pagefound)
                        {
                            //ndouble++;
                            if (nrealnames == 1)
                            {
                                foreach (forkclass fc in fl)
                                    if (fc.realname == "*")
                                    {
                                        Disambigclass da = new Disambigclass();
                                        da.fork = fc;

                                        if (coordfound)
                                        {
                                            double dist = get_distance_latlong(lat, lon, fc.latitude, fc.longitude);
                                            if (dist < 5.0) //Probably same object
                                            {
                                                da.existsalready = true;
                                                //sw.WriteLine(fc.geonameid + tabstring + "*"+s);
                                            }
                                            else
                                            {
                                                da.fcode = true;
                                                da.country = true;
                                                //sw.WriteLine(fc.geonameid + tabstring + s + " (" + removearticle(featuredict[fc.featurecode]) + " " + mp(75) + " " + fc.admname[0] + ")");
                                            }
                                        }
                                        else //no coordinates
                                        {
                                            da.fcode = true;
                                            da.country = true;
                                            //sw.WriteLine(fc.geonameid + tabstring + s + " (" + removearticle(featuredict[fc.featurecode]) + " " + mp(75) + " " + fc.admname[0] + ")");
                                        }
                                        dadict.Add(fc.geonameid, da);

                                    }
                            }
                            else //several realnames, pagefound
                            {
                                if (coordfound)
                                {
                                    foreach (forkclass fc in fl)
                                        if (fc.realname == "*")
                                        {
                                            Disambigclass da = new Disambigclass();
                                            da.fork = fc;

                                            double dist = get_distance_latlong(lat, lon, fc.latitude, fc.longitude);
                                            if (dist < 5.0) //Probably same object
                                            {
                                                da.existsalready = true;
                                                //sw.WriteLine(fc.geonameid + tabstring + "X");
                                            }
                                            else
                                            {
                                                //sw.Write(fc.geonameid + tabstring + s + " (");
                                                //sw.Write(removearticle(featuredict[fc.featurecode]));
                                                da.fcode = true;
                                                if (somesamefcode)
                                                {
                                                    //sw.Write(" " + mp(75) + " " + fc.admname[0]);
                                                    da.country = !allsamecountry && !String.IsNullOrEmpty(da.fork.admname[0]);
                                                    if (somesamecountry)
                                                    {

                                                        //sw.Write(", ");
                                                        //if (!allsameadm1)
                                                        da.adm1 = !allsameadm1 && !String.IsNullOrEmpty(da.fork.admname[1]); // sw.Write(fc.admname[1]);
                                                        if (somesameadm1)
                                                        {
                                                            //if (!allsameadm1 && !String.IsNullOrEmpty(fc.admname[1]))
                                                            //    sw.Write(", ");
                                                            //if (!allsameadm2)
                                                            //    sw.Write(fc.admname[2]);
                                                            da.adm2 = !allsameadm2 && !String.IsNullOrEmpty(da.fork.admname[2]);
                                                            if (somesameadm2)
                                                            {
                                                                da.latlong = true;
                                                                //if (!allsameadm2 && !String.IsNullOrEmpty(fc.admname[2]))
                                                                //    sw.Write(", ");
                                                                //sw.Write("lat " + fc.latitude.ToString("F2") + ", long " + fc.longitude.ToString("F2"));
                                                            }
                                                        }

                                                    }
                                                }
                                                //sw.WriteLine(")");
                                            }
                                            dadict.Add(fc.geonameid, da);

                                        }
                                }
                                else //no coordinates, several realnames, page found
                                {
                                    foreach (forkclass fc in fl)
                                        if (fc.realname == "*")
                                        {
                                            Disambigclass da = new Disambigclass();
                                            da.fork = fc;

                                            //sw.Write(fc.geonameid + tabstring + s + " (");
                                            //sw.Write(removearticle(featuredict[fc.featurecode]));
                                            da.fcode = true;

                                            if (somesamefcode)
                                            {
                                                //sw.Write(" " + mp(75) + " " + fc.admname[0]);
                                                da.country = !allsamecountry && !String.IsNullOrEmpty(da.fork.admname[0]);
                                                if (somesamecountry)
                                                {

                                                    //sw.Write(", ");
                                                    //if (!allsameadm1)
                                                    da.adm1 = !allsameadm1 && !String.IsNullOrEmpty(da.fork.admname[1]); // sw.Write(fc.admname[1]);
                                                    if (somesameadm1)
                                                    {
                                                        //if (!allsameadm1 && !String.IsNullOrEmpty(fc.admname[1]))
                                                        //    sw.Write(", ");
                                                        //if (!allsameadm2)
                                                        //    sw.Write(fc.admname[2]);
                                                        da.adm2 = !allsameadm2 && !String.IsNullOrEmpty(da.fork.admname[2]);
                                                        if (somesameadm2)
                                                        {
                                                            da.latlong = true;
                                                            //if (!allsameadm2 && !String.IsNullOrEmpty(fc.admname[2]))
                                                            //    sw.Write(", ");
                                                            //sw.Write("lat " + fc.latitude.ToString("F2") + ", long " + fc.longitude.ToString("F2"));
                                                        }
                                                    }

                                                }

                                            }
                                            //sw.WriteLine(")");
                                            dadict.Add(fc.geonameid, da);
                                        }


                                }
                            }
                        }
                        else //page not found
                        {
                            if (nrealnames == 1)
                            {
                                foreach (forkclass fc in fl)
                                {
                                    if (fc.realname == "*")
                                    {
                                        //sw.WriteLine(fc.geonameid + tabstring + s);
                                        Disambigclass da = new Disambigclass();
                                        da.fork = fc;
                                        if (nnames > 1)
                                        {
                                            da.fcode = true;
                                            da.country = true;
                                        }
                                        dadict.Add(fc.geonameid, da);
                                    }
                                }
                            }
                            else //several realnames, page not found
                            {
                                foreach (forkclass fc in fl)
                                    if (fc.realname == "*")
                                    {
                                        Disambigclass da = new Disambigclass();
                                        da.fork = fc;

                                        //sw.Write(fc.geonameid + tabstring + s + " (");
                                        //sw.Write(removearticle(featuredict[fc.featurecode]));
                                        da.fcode = true;

                                        if (somesamefcode)
                                        {
                                            //sw.Write(" " + mp(75) + " " + fc.admname[0]);
                                            da.country = !allsamecountry && !String.IsNullOrEmpty(da.fork.admname[0]);
                                            if (somesamecountry)
                                            {

                                                //sw.Write(", ");
                                                //if (!allsameadm1)
                                                da.adm1 = !allsameadm1 && !String.IsNullOrEmpty(da.fork.admname[1]); // sw.Write(fc.admname[1]);
                                                if (somesameadm1)
                                                {
                                                    //if (!allsameadm1 && !String.IsNullOrEmpty(fc.admname[1]))
                                                    //    sw.Write(", ");
                                                    //if (!allsameadm2)
                                                    //    sw.Write(fc.admname[2]);
                                                    da.adm2 = !allsameadm2 && !String.IsNullOrEmpty(da.fork.admname[2]);
                                                    if (somesameadm2)
                                                    {
                                                        da.latlong = true;
                                                        //if (!allsameadm2 && !String.IsNullOrEmpty(fc.admname[2]))
                                                        //    sw.Write(", ");
                                                        //sw.Write("lat " + fc.latitude.ToString("F2") + ", long " + fc.longitude.ToString("F2"));
                                                    }
                                                }

                                            }

                                        }
                                        //sw.WriteLine(")");
                                        dadict.Add(fc.geonameid, da);

                                    }
                            }
                        }

                        foreach (int gnid in dadict.Keys)
                        {
                            if (nrealnames > 1)
                            {
                                bool uniquecountry = !String.IsNullOrEmpty(dadict[gnid].fork.admname[0]);
                                bool uniqueadm1 = !String.IsNullOrEmpty(dadict[gnid].fork.admname[1]);
                                bool uniqueadm2 = !String.IsNullOrEmpty(dadict[gnid].fork.admname[2]);
                                bool uniquefcode = true;

                                foreach (forkclass ff2 in fl)
                                {
                                    if ((ff2.realname == "*") && (ff2.geonameid != gnid))
                                    {
                                        if (dadict[gnid].fork.admname[0] == ff2.admname[0])
                                            uniquecountry = false;
                                        if (dadict[gnid].fork.admname[1] == ff2.admname[1])
                                            uniqueadm1 = false;
                                        if (dadict[gnid].fork.admname[2] == ff2.admname[2])
                                            uniqueadm2 = false;
                                        if (dadict[gnid].fork.featurecode == ff2.featurecode)
                                            uniquefcode = false;
                                    }

                                }

                                if (dadict[gnid].fcode && uniquefcode)
                                {
                                    dadict[gnid].country = false;
                                    dadict[gnid].adm1 = false;
                                    dadict[gnid].adm2 = false;
                                    dadict[gnid].latlong = false;
                                }
                                else if (dadict[gnid].country && uniquecountry)
                                {
                                    dadict[gnid].adm1 = false;
                                    dadict[gnid].adm2 = false;
                                    dadict[gnid].latlong = false;
                                }
                                else if (dadict[gnid].adm1 && uniqueadm1)
                                {
                                    dadict[gnid].adm2 = false;
                                    dadict[gnid].latlong = false;
                                }
                                else if (dadict[gnid].adm2 && uniqueadm2)
                                {
                                    dadict[gnid].latlong = false;
                                }
                            }

                            //if (!gndict.ContainsKey(gnid))
                            //    continue;
                            string artname = "";

                            if (countrydict.ContainsKey(gnid))
                            {
                                artname = "*" + countrydict[gnid].Name_ml;
                            }

                            if (checkwiki && String.IsNullOrEmpty(artname)) //Look for interwiki matches
                            {
                                if (dadict[gnid].fork.wdid > 0)
                                {
                                    XmlDocument cx = get_wd_xml(wdid);
                                    if (cx != null)
                                    {
                                        Dictionary<string, string> rd = get_wd_sitelinks(cx);
                                        foreach (string wiki in rd.Keys)
                                        {
                                            string ssw = wiki.Replace("wiki", "");
                                            if (ssw == makelang)
                                            {
                                                artname = "*" + rd[wiki];
                                                ndouble_iw++;
                                                break;
                                            }
                                        }
                                    }

                                }
                            }

                            if ((String.IsNullOrEmpty(artname)) && (dadict[gnid].fork.featurecode == "ADM1")) //Look for ADM1 in category:
                            {
                                Console.WriteLine("Checking for ADM1 match " + s + ", " + dadict[gnid].fork.admname[0]);
                                if (existing_adm1.ContainsKey(dadict[gnid].fork.admname[0]))
                                {
                                    Console.WriteLine("Country found; count = " + existing_adm1[dadict[gnid].fork.admname[0]].Count.ToString());
                                    if (existing_adm1[dadict[gnid].fork.admname[0]].Contains(s))
                                        artname = "*" + s;
                                    else
                                    {
                                        foreach (string es in existing_adm1[dadict[gnid].fork.admname[0]])
                                            if (remove_disambig(es) == "s")
                                            {
                                                artname = "*" + es;
                                                nadm1++;
                                                break;
                                            }

                                        if (String.IsNullOrEmpty(artname)) //Look for fuzzy matches:
                                        {
                                            int mindist = 999;
                                            if (s.Length < 4)
                                                mindist = 1;
                                            else if (s.Length < 7)
                                                mindist = 3;
                                            else if (s.Length < 20)
                                                mindist = 4;
                                            else
                                                mindist = 5;
                                            int distmax = mindist;
                                            mindist = 999;
                                            string mindistname = "";

                                            foreach (string es in existing_adm1[dadict[gnid].fork.admname[0]])
                                            {
                                                string tit = remove_disambig(es);
                                                int dist = LevenshteinDistance(s, tit);
                                                //Console.WriteLine(s+" | "+tit + ": "+dist.ToString());
                                                if (dist < mindist)
                                                {
                                                    mindist = dist;
                                                    mindistname = es;
                                                }

                                            }
                                            if (mindist < distmax)
                                            {
                                                Console.WriteLine("Fuzzy match: " + s + " | " + mindistname + ": " + mindist.ToString());
                                                //Console.ReadLine();
                                                nadm1++;
                                                fuzzylist.Add(gnid.ToString() + ": " + s + " | " + mindistname + ": " + mindist.ToString());
                                                artname = "*" + mindistname;
                                            }
                                            else if (manualcheck && (!String.IsNullOrEmpty(mindistname)))
                                            {
                                                Console.WriteLine("Fuzzy match: " + s + " | " + mindistname + ": " + mindist.ToString());
                                                Console.Write("OK? (y/n)");
                                                char yn = Console.ReadKey().KeyChar;
                                                if (yn == 'y')
                                                {
                                                    nadm1++;
                                                    fuzzylist.Add(gnid.ToString() + ": " + s + " | " + mindistname + ": " + mindist.ToString());
                                                    artname = "*" + mindistname;
                                                    Console.WriteLine("Saving " + artname);
                                                    swfuzz.WriteLine(gnid.ToString() + tabstring + artname);

                                                }
                                                else
                                                    Console.WriteLine(yn.ToString());
                                            }

                                        }
                                    }
                                }
                                //Console.ReadLine();
                            }

                            if (String.IsNullOrEmpty(artname)) //Look for fuzzy matches:
                            {
                                int mindist = 999;
                                if (s.Length < 4)
                                    mindist = 0;
                                else if (s.Length < 7)
                                    mindist = 2;
                                else if (s.Length < 20)
                                    mindist = 3;
                                else
                                    mindist = 4;
                                int distmax = mindist;
                                string mindistname = "";
                                List<int> enb = getexisting(dadict[gnid].fork.latitude, dadict[gnid].fork.longitude, 10.0);
                                foreach (int nb in enb)
                                {
                                    if (existingdict.ContainsKey(nb))
                                    {
                                        string cleanart = remove_disambig(existingdict[nb].articlename);
                                        if (cleanart == s)
                                        {
                                            artname = "*" + existingdict[nb].articlename;
                                            ndouble_coord++;
                                            break;
                                        }
                                        else
                                        {
                                            int dist = LevenshteinDistance(s, cleanart);
                                            //Console.WriteLine(s+" | "+cleanart + ": "+dist.ToString());
                                            if (dist < mindist)
                                            {
                                                mindist = dist;
                                                mindistname = existingdict[nb].articlename;
                                            }
                                        }

                                    }
                                }
                                if (String.IsNullOrEmpty(artname))
                                {
                                    if (mindist < distmax)
                                    {
                                        Console.WriteLine("Fuzzy match: " + s + " | " + mindistname + ": " + mindist.ToString());
                                        //Console.ReadLine();
                                        nfuzzy++;
                                        fuzzylist.Add(gnid.ToString() + ": " + s + " | " + mindistname + ": " + mindist.ToString());
                                        artname = "*" + mindistname;
                                    }
                                }
                            }

                            if (String.IsNullOrEmpty(artname))
                            {

                                if (dadict[gnid].existsalready)
                                    artname = "*" + s;
                                else
                                {
                                    bool daneeded = dadict[gnid].fcode || dadict[gnid].country || dadict[gnid].adm1 || dadict[gnid].adm2 || dadict[gnid].latlong;
                                    if (!daneeded)
                                        artname = s;
                                    else
                                    {
                                        artname = s + " " + make_disambig(dadict[gnid], gnid);
                                    }

                                }
                            }

                            if (donecountries.Contains(dadict[gnid].fork.iso))
                            {

                                if (oldartnamedict.ContainsKey(gnid))
                                {
                                    string fname = removearticle(getfeaturelabel(dadict[gnid].fork.iso, dadict[gnid].fork.featurecode, gnid));
                                    if (!oldartnamedict[gnid].Contains("(") || oldartnamedict[gnid].Contains(fname))
                                        artname = oldartnamedict[gnid];
                                }
                            }

                            sw.WriteLine(gnid.ToString() + tabstring + artname);
                            np++;
                            if (artname.Contains("*"))
                                ndouble++;
                            if ((np % 1000) == 0)
                            {
                                Console.WriteLine("np  = " + np.ToString() + ", " + countryname);
                            }
                        }

                        if ((ndouble % 100) == 0)
                        {
                            Console.WriteLine("n (doubles)   = " + ndouble.ToString());
                        }


                        //while (s[0] != '#')
                        //    s = sr.ReadLine();
                        //continue;
                    }

                    foreach (string ss in fuzzylist)
                        Console.WriteLine(ss);
                    Console.WriteLine("n    (doubles)     = " + ndouble.ToString());
                    Console.WriteLine("n    (checkwiki)   = " + ndouble_cw.ToString());
                    Console.WriteLine("n    (oldwiki)     = " + ndouble_ow.ToString());
                    Console.WriteLine("n    (coord)       = " + ndouble_coord.ToString());
                    Console.WriteLine("n    (wikidata)    = " + ndouble_iw.ToString());
                    Console.WriteLine("n    (fuzzy match) = " + nfuzzy.ToString());
                    Console.WriteLine("n    (ADM1-match)  = " + nadm1.ToString());
                    Console.WriteLine("n    (alldisambig) = " + nalldisambig.ToString());

                    //scripthist.PrintSHist();
                    //foreach (string gt in geotempdict.Keys)
                    //    Console.WriteLine(gt + ":" + geotempdict[gt].ToString());

                }
            }
        }

        public static string make_disambig(Disambigclass da, int gnid)
        {
            //public class forkclass //class for entries in a fork page
            //{
            //    public int geonameid = 0;
            //    public string featurecode = "";
            //    public string[] admname = new string[3];
            //    public double latitude = 0.0;
            //    public double longitude = 0.0;
            //    public string realname = "*"; 
            //    public int wdid = -1;    //wikidata id
            //    public string iso = "XX"; //country iso code
            //    public string featurename = "";
            //}

            //public class Disambigclass //class for disambiguation in article names
            //{
            //    public bool existsalready = false;
            //    public bool country = false;
            //    public bool adm1 = false;
            //    public bool adm2 = false;
            //    public bool latlong = false;
            //    public bool fcode = false;
            //    public forkclass fork = new forkclass();
            //}

            if (String.IsNullOrEmpty(da.fork.admname[0]))
                da.country = false;
            if (String.IsNullOrEmpty(da.fork.admname[1]))
                da.adm1 = false;
            if (String.IsNullOrEmpty(da.fork.admname[2]))
                da.adm2 = false;

            string artname = "(";
            bool needscomma = false;
            if (da.fcode)
            {
                artname += removearticle(getfeaturelabel(da.fork.iso, da.fork.featurecode, gnid));
                if (da.adm1 || da.adm2 || da.country)
                    artname += " " + mp(75) + " ";
                else
                    needscomma = true;
            }
            if (da.country)
            {
                if (needscomma)
                    artname += ", ";
                artname += da.fork.admname[0];
                needscomma = true;
            }
            if (da.adm1)
            {
                if (needscomma)
                    artname += ", ";
                artname += da.fork.admname[1];
                needscomma = true;
            }
            if (da.adm2)
            {
                if (needscomma)
                    artname += ", ";
                artname += da.fork.admname[2];
                needscomma = true;
            }
            if (da.latlong)
            {
                if (needscomma)
                    artname += ", ";
                artname += "lat " + da.fork.latitude.ToString("F2") + ", long " + da.fork.longitude.ToString("F2");
                needscomma = true;
            }

            artname += ")";

            return artname;
        }

        public static string make_coord_template(string countrycode, string fcode, double lat, double lon, string artname)
        {
            string rs = "{{Coord|";
            rs += lat.ToString(culture_en) + "|";
            rs += lon.ToString(culture_en) + "|";
            //rs += "display=inline|";

            string typecode = "landmark";
            //Console.WriteLine("fcode = " + fcode);
            string cat = "geography";
            if (categorydict.ContainsKey(fcode))
                cat = categorydict[fcode];

            if (fcode == "ADM1")
                typecode = "adm1st";
            else if (fcode == "ADM2")
                typecode = "adm2nd";
            else if (fcode.Contains("ADM"))
                typecode = "adm3rd";
            else
            {
                switch (cat)
                {
                    case "populated places":
                        typecode = "city";
                        break;
                    case "areas":
                    case "plains":
                    case "deserts":
                        typecode = "adm1st";
                        break;
                    case "navigation":
                    case "wetlands":
                    case "seabed":
                    case "lakes":
                    case "coasts":
                    case "straits":
                    case "bays":
                        typecode = "waterbody";
                        break;
                    case "mountains":
                    case "hills":
                    case "volcanoes":
                    case "rock formations":
                    case "valleys":
                        typecode = "mountain";
                        break;
                    case "islands":
                    case "peninsulas":
                        typecode = "isle";
                        break;
                    case "forests":
                        typecode = "forest";
                        break;
                    case "streams":
                        typecode = "river";
                        break;
                    case "ice":
                        typecode = "glacier";
                        break;
                    default:
                        typecode = "landmark";
                        break;
                }

            }


            rs += "region:" + countrycode + "_type:" + typecode;
            rs += "|name=" + artname + "}}";
            return rs;
        }

        public static bool is_fork(Page p)
        {
            if (!p.Exists())
                return false;

            if (makelang == "ceb")
            {
                if (p.text.ToLower().Contains("{{giklaro"))
                    return true;
            }
            else if (makelang == "war")
            {
                if (p.text.ToLower().Contains("{{pansayod"))
                    return true;
            }
            else if (makelang == "sv")
            {
                if (forktemplates.Count == 0)
                {
                    PageList pl = new PageList(makesite);
                    pl.FillFromCategory("Förgreningsmallar");
                    foreach (Page pp in pl)
                        forktemplates.Add(pp.title.Replace("Mall:", "").ToLower());
                }
                foreach (string ft in forktemplates)
                    if (p.text.ToLower().Contains("{{" + ft))
                    {
                        Console.WriteLine("is_fork: ft = |" + ft + "|");
                        return true;
                    }
            }

            return false;
        }

        public static string saveconflict(string thisname, string othername)
        {
            if (thisname == othername)
                return "";

            string rs = "";
            string[] p9 = new string[2] { othername, getmonthstring() };
            rs = mp(9, p9) + "\n";
            if (pconflict == null)
            {
                pconflict = new Page(makesite, mp(13) + botname + "/Namnkonflikter-PRIVAT");
            }
            tryload(pconflict, 1);

            string ptt = "*[[" + thisname + "]] [[" + othername + "]]\n";
            if (!pconflict.text.Contains(ptt))
            {
                if (!conflictheadline)
                {
                    pconflict.text += "\n\n== " + countryml[makecountryname] + " ==\n";
                    conflictheadline = true;
                }

                pconflict.text += "\n" + ptt;

                trysave(pconflict, 1,mp(304,null));
            }
            return rs;
        }

        public static string saveanomaly(string thisname, string reason)
        {
            string rs = "";
            string[] p196 = new string[1] { reason };
            rs = mp(196, p196) + "\n";
            if (panomaly == null)
            {
                panomaly = new Page(makesite, mp(13) + botname + "/Anomalier-PRIVAT");
                tryload(panomaly, 1);
            }

            string ptt = "*[[" + thisname + "]] " + reason;
            if (!panomaly.text.Contains(ptt))
            {
                if (!anomalyheadline)
                {
                    panomaly.text += "\n\n== " + countryml[makecountryname] + " ==\n";
                    anomalyheadline = true;
                }
                panomaly.text += "\n" + ptt;
                trysave(panomaly, 1, mp(304,null));
            }
            return rs;
        }

        public static void find_duplicate_forks()
        {
            Dictionary<string, List<string>> forkdict = new Dictionary<string, List<string>>();

            using (StreamReader sw = new StreamReader(geonamesfolder + "namefork-" + makelang + ".csv"))
            {
                while (!sw.EndOfStream)
                {
                    string s = sw.ReadLine();
                    s = s.Trim(';');

                    List<forkclass> fl = new List<forkclass>();

                    string countryname = "";

                    string gnidstring = "";
                    int nreal = 0;

                    while (true)
                    {
                        string line = sw.ReadLine();
                        if (line[0] == '#')
                            break;
                        string[] words = line.Split(';');

                        //public class forkclass
                        //{
                        //    public int geonameid = 0;
                        //    public string featurecode = "";
                        //    public string[] admname = new string[3];
                        //    public double latitude = 0.0;
                        //    public double longitude = 0.0;

                        //}

                        forkclass fc = new forkclass();
                        fc.geonameid = tryconvert(words[0]);
                        gnidstring += " " + fc.geonameid;
                        fc.featurecode = words[1];
                        fc.admname[0] = words[2];
                        fc.admname[1] = words[3];
                        fc.admname[2] = words[4];
                        fc.latitude = tryconvertdouble(words[5]);
                        fc.longitude = tryconvertdouble(words[6]);
                        fc.realname = words[7];

                        if (fc.realname == "*")
                        {
                            fc.realname = s;
                            nreal++;
                        }
                        //if (artnamedict.ContainsKey(fc.geonameid))
                        //    fc.realname = artnamedict[fc.geonameid];
                        fl.Add(fc);
                        countryname = words[2];
                    }

                    if (fl.Count < 2)
                        continue;

                    gnidstring = gnidstring.Trim();

                    if (forkdict.ContainsKey(gnidstring))
                    {
                        Console.WriteLine(s + ";" + nreal.ToString());
                        forkdict[gnidstring].Add(s + ";" + nreal.ToString());
                    }
                    else
                    {
                        List<string> ll = new List<string>();
                        ll.Add(s + ";" + nreal.ToString());
                        forkdict.Add(gnidstring, ll);
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter("namefork-duplicates-" + makelang + getdatestring() + ".txt"))
            {
                foreach (string gnidstring in forkdict.Keys)
                {
                    if (forkdict[gnidstring].Count > 1)
                    {
                        int nrmax = -1;
                        string srmax = "";
                        foreach (string ss in forkdict[gnidstring])
                        {
                            string[] sss = ss.Split(';');
                            int nreal = tryconvert(sss[1]);
                            if (nreal > nrmax)
                            {
                                nrmax = nreal;
                                srmax = sss[0];
                            }
                        }
                        if (nrmax > 0)
                        {
                            sw.Write(srmax);
                            foreach (string ss in forkdict[gnidstring])
                            {
                                string[] sss = ss.Split(';');
                                if (sss[0] != srmax)
                                    sw.Write(";" + sss[0]);
                            }
                            sw.WriteLine();

                        }
                    }
                }
            }

        }

        public static void makeforkpages()
        {
            int nfork = 0;
            List<string> forkdoubles = new List<string>();
            int nu2019 = 0;

            makesite.defaultEditComment = mp(69);
            if (!String.IsNullOrEmpty(makecountry))
                makesite.defaultEditComment += " " + countryml[makecountryname];

            if (pstats == null)
            {
                pstats = new Page(makesite, mp(13) + botname + "/Statistik");
                pstats.Load();
            }
            pstats.text += "\n\n== [[" + countryml[makecountryname] + "]] grensidor ==\n\n";
            trysave(pstats, 1, mp(302) + " " + countryml[makecountryname]);

            Dictionary<string, string> forkduplicatedict = new Dictionary<string, string>();
            using (StreamReader sw = new StreamReader(geonamesfolder + "namefork-duplicates-" + makelang + ".txt"))
            {
                while (!sw.EndOfStream)
                {
                    string s = sw.ReadLine();
                    string[] ss = s.Split(';');
                    if (ss.Length < 2)
                        continue;
                    for (int i = 1; i < ss.Length; i++)
                    {
                        if (!forkduplicatedict.ContainsKey(ss[i]))
                            forkduplicatedict.Add(ss[i], ss[0]); //dictionary from duplicate name to proper name
                    }
                }
            }

            using (StreamReader sw = new StreamReader(geonamesfolder + "namefork-" + makelang + ".csv"))
            {
                while (!sw.EndOfStream)
                {
                    string s = sw.ReadLine();
                    s = s.Trim(';');

                    int nbranches = 0;

                    List<forkclass> fl = new List<forkclass>();

                    string countryname = "";

                    int nsartname = 0;
                    while (true)
                    {
                        string line = sw.ReadLine();

                        if (line[0] == '#')
                            break; //normal exit from the loop

                        string[] words = line.Split(';');

                        //public class forkclass
                        //{
                        //    public int geonameid = 0;
                        //    public string featurecode = "";
                        //    public string[] admname = new string[3];
                        //    public double latitude = 0.0;
                        //    public double longitude = 0.0;

                        //}

                        forkclass fc = new forkclass();
                        fc.geonameid = tryconvert(words[0]);
                        fc.featurecode = words[1];
                        if (!featuredict.ContainsKey(fc.featurecode))
                            continue;

                        fc.admname[0] = words[2];
                        fc.admname[1] = words[3];
                        fc.admname[2] = words[4];
                        fc.latitude = tryconvertdouble(words[5]);
                        fc.longitude = tryconvertdouble(words[6]);
                        fc.realname = words[7];
                        if (fc.realname == "*")
                            fc.realname = s;
                        //if (artnamedict.ContainsKey(fc.geonameid))
                        //    fc.realname = artnamedict[fc.geonameid];
                        fl.Add(fc);
                        countryname = words[2];
                        if (artnamedict.ContainsKey(fc.geonameid))
                            if (s == artnamedict[fc.geonameid].Replace("*", ""))
                                nsartname++;
                    }

                    //bool allsamecountry = true;
                    bool hasmakecountry = false;
                    bool hasanomaly = false;
                    string anomalytext = "";

                    //Console.WriteLine("# names in fork page = " + fl.Count.ToString());

                    if (fl.Count < 2)
                        continue;

                    foreach (forkclass ff in fl)
                    {
                        //if (ff.admname[0] != countryname)
                        //    allsamecountry = false;
                        if (ff.admname[0] == makecountryname)
                            hasmakecountry = true;
                    }

                    if (!hasmakecountry)
                    {
                        Console.WriteLine("No place in makecountry");
                        continue;
                    }

                    if (!String.IsNullOrEmpty(resume_at_fork))
                        if (s != resume_at_fork)
                        {
                            stats.Addskip();
                            continue;
                        }
                        else
                            resume_at_fork = "";


                    Console.WriteLine("nsartname = " + nsartname.ToString());
                    if (nsartname >= 2)
                    {
                        Console.WriteLine(s + " Too many places link to same!");
                        //Console.ReadLine();
                    }

                    bool alreadyfork = false;
                    bool willoverwrite = false;

                    string alreadyforktitle = "";

                    string forkpagename = testprefix + s;
                    Page forkpage = new Page(makesite, forkpagename);
                    if (tryload(forkpage, 2))
                    {
                        if (forkpage.Exists() || (nsartname > 0))
                        {
                            if (!forkpage.Exists())
                                forkpage.text = "";

                            if (forkpage.text.Contains(mp(69))) //botmade fork; don't make again unless overwrite is set
                            {
                                if (!overwrite || human_touched(forkpage, makesite))
                                    continue;
                                else
                                {
                                    alreadyfork = false;
                                    willoverwrite = true;
                                }
                            }
                            else
                            {
                                if (is_fork(forkpage))
                                {
                                    alreadyfork = true;
                                    alreadyforktitle = forkpage.title;
                                }



                                Page fp2 = new Page(makesite, forkpage.title + " (" + mp(67) + ")");

                                if (tryload(fp2, 1))
                                {
                                    if (fp2.Exists())
                                    {
                                        alreadyfork = true;
                                        alreadyforktitle = forkpage.title;

                                        if (fp2.text.Contains(mp(69))) //botmade fork; don't make again
                                        {
                                            if (!overwrite || human_touched(fp2, makesite))
                                                continue;
                                            else
                                            {
                                                alreadyfork = false;
                                                forkpage = fp2;
                                                forkpage.text = "";
                                                willoverwrite = true;
                                            }
                                        }
                                        else
                                        {
                                            Page fp3 = new Page(makesite, fp2.title.Replace(")", " 2)"));

                                            if (tryload(fp3, 1))
                                            {
                                                if (fp3.Exists())
                                                    continue;
                                                else
                                                {
                                                    forkpage = fp3;
                                                    forkpage.text = "";
                                                }

                                            }
                                            else
                                            {
                                                forkpage = fp3;
                                                forkpage.text = "";
                                            }

                                        }
                                    }
                                    else
                                    {
                                        forkpage = fp2;
                                        forkpage.text = "";
                                    }
                                }
                                else
                                {
                                    forkpage = fp2;
                                    forkpage.text = "";
                                }
                            }
                        }
                    }
                    Console.WriteLine("forkpage.title = " + forkpage.title);

                    if (forkduplicatedict.ContainsKey(s))
                    {
                        if (forkpage.title == remove_disambig(forkpage.title))
                            make_redirect_override(forkpage, forkduplicatedict[s], "", -1);
                        Console.WriteLine("duplicate fork " + forkpage.title + " - " + forkduplicatedict[s]);
                        //Console.ReadLine();
                        continue;
                    }

                    string origtext = forkpage.text;
                    forkpage.text = "";
                    if (alreadyfork)
                    {
                        forkpage.text += saveconflict(forkpage.title, alreadyforktitle);
                    }

                    forkpage.text += mp(120) + "\n\n";


                    string[] p68 = new string[1] { s };
                    forkpage.text += mp(68, p68) + ":\n";

                    forkpage.text += "== " + mp(307) + " ==\n";
                    if (makelang == "sv")
                    {
                        forkpage.text = mp(142) + "\n" + forkpage.text;
                        forkpage.text += "\n" + comment("NOTERA: Om platser läggs till, tas bort eller ordningen på platserna ändras, bör också mallen Kartposition under samma rubrik korrigeras för att kartan ska förbli rättvisande.") + "\n";
                    }

                    string[] p71 = new string[1] { countryname };

                    //if (allsamecountry)
                    //{
                    //    if (fl.Count > 2)
                    //        forkpage.text += mp(71, p71);
                    //    else
                    //        forkpage.text += mp(78, p71);
                    //}

                    forkpage.text += "\n\n";

                    //if (allsamecountry)
                    //{
                    //    string countrynameml = countryname;
                    //    if (countryml.ContainsKey(countryname))
                    //        countrynameml = countryml[countryname];
                    //    string[] p73 = new string[2] { countrynameml, s };
                    //    if (locatordict.ContainsKey(countryname))
                    //    {
                    //        forkpage.text += mp(72) + "+|" + locatordict[countryname] + "\n |caption = " + mp(73, p73) + "\n  |float = right\n  |width=300\n  | places =";
                    //        int inum = 0;
                    //        foreach (forkclass ff in fl)
                    //        {
                    //            inum++;
                    //            forkpage.text += mp(72) + "~|" + locatordict[countryname] + "| label = " + inum.ToString() + "| mark =Blue_pog.svg|position=right|background=white|lat=" + ff.latitude.ToString(culture_en) + "|long=" + ff.longitude.ToString(culture_en) + "|caption=|float=}}\n";
                    //        }
                    //        forkpage.text += "}}\n";
                    //    }
                    //    foreach (forkclass ff in fl)
                    //    {
                    //        string artname = s;
                    //        if (artnamedict.ContainsKey(ff.geonameid))
                    //        {
                    //            if (artnamedict[ff.geonameid] != "X")
                    //                artname = artnamedict[ff.geonameid];
                    //        }
                    //        string ss = "# [[" + artname + "]], ";
                    //        if (!artname.Contains("(" + featuredict[ff.featurecode]))
                    //            ss += featuredict[ff.featurecode];
                    //        if (!String.IsNullOrEmpty(ff.admname[1]) && !artname.Contains(ff.admname[1]))
                    //            ss += ", " + ff.admname[1];
                    //        if (!String.IsNullOrEmpty(ff.admname[2]) && !artname.Contains(ff.admname[2]))
                    //            ss += ", " + ff.admname[2];
                    //        if (!artname.Contains(" lat "))
                    //        {
                    //            ss += ", lat. " + ff.latitude.ToString("F1", culture);
                    //            ss += ", long. " + ff.longitude.ToString("F1", culture);
                    //        }
                    //        forkpage.text += ss + "\n";
                    //        nbranches++;
                    //    }
                    //}
                    //else
                    //{
                    Dictionary<string, List<forkclass>> fd = new Dictionary<string, List<forkclass>>();
                    SortedDictionary<string, string> scountry = new SortedDictionary<string, string>();


                    foreach (forkclass ff in fl)
                    {
                        string sortcountry = ff.admname[0];
                        if (countryml.ContainsKey(ff.admname[0]))
                            sortcountry = countryml[ff.admname[0]];
                        string locatorkey = ff.admname[0];
                        if ((String.IsNullOrEmpty(sortcountry)) || (countrydict.ContainsKey(ff.geonameid)))
                        {
                            sortcountry = mp(166);
                            locatorkey = "";
                        }
                        if (!scountry.ContainsKey(sortcountry))
                            scountry.Add(sortcountry, locatorkey);
                        if (!fd.ContainsKey(locatorkey))
                        {
                            List<forkclass> ffl = new List<forkclass>();
                            fd.Add(locatorkey, ffl);
                        }
                        fd[locatorkey].Add(ff);
                    }

                    int ncountries = 0;
                    int maxpercountry = 0;
                    int nplaces = 0;
                    foreach (string cs in fd.Keys)
                    {
                        ncountries++;
                        nplaces += fd[cs].Count;
                        if (fd[cs].Count > maxpercountry)
                            maxpercountry = fd[cs].Count;
                    }

                    bool worldmaponly = ((ncountries > 4) && (maxpercountry < 4));

                    if (makeworldmaponly & !worldmaponly)
                        continue;

                    int inum = 0;

                    if (worldmaponly)
                    {
                        int mapsize = 450;

                        string caption = "";
                        if (makelang == "sv")
                        {
                            string ifcollapsed = "";//" mw-collapsed";
                            string collapseintro = "{| class=\"mw-collapsible" + ifcollapsed + "\" data-expandtext=\"Visa karta\" data-collapsetext=\"Dölj karta\" style=\"float:right; clear:right;\"\n|-\n!\n|-\n|\n";
                            forkpage.text += collapseintro;
                        }
                        forkpage.text += mp(72) + "+|" + locatordict[""].locatorname + "\n |caption = " + caption + "\n  |float = right\n  |width=" + mapsize.ToString() + "\n  | places =";
                        inum = 0;
                        foreach (string csl in scountry.Keys)
                        {
                            string cs = scountry[csl];
                            foreach (forkclass ff in fd[cs])
                            {
                                inum++;
                                forkpage.text += mp(72) + "~|" + locatordict[""].locatorname + "| label = " + inum.ToString() + "| mark =Blue_pog.svg|position=right|background=white|lat=" + ff.latitude.ToString(culture_en) + "|long=" + ff.longitude.ToString(culture_en) + "}}\n";
                            }
                        }
                        forkpage.text += "}}\n";
                        if (makelang == "sv")
                            forkpage.text += "|}\n"; //collapse-end
                    }

                    inum = 0;

                    foreach (string csl in scountry.Keys)
                    {
                        forkpage.text += "=== " + csl + " ===\n";
                        string cs = scountry[csl];

                        string ciso = "";
                        string locname = csl;
                        if (countryiso.ContainsKey(cs))
                        {
                            ciso = countryiso[cs];
                            locname = linkcountry(ciso);
                        }

                        if (locatordict.ContainsKey(cs) && !worldmaponly)
                        {
                            int mapsize = 300;
                            if (fd[cs].Count > 40)
                                mapsize = 600;
                            else if (fd[cs].Count > 8)
                                mapsize = 450;
                            else if (fd[cs].Count == 1)
                                mapsize = 150;

                            string[] p73 = new string[2] { locname, s };
                            string caption = mp(73, p73);
                            if (csl == mp(166))
                                caption = csl;
                            if (makelang == "sv")
                            {
                                string ifcollapsed = "";//" mw-collapsed";
                                string collapseintro = "{| class=\"mw-collapsible" + ifcollapsed + "\" data-expandtext=\"Visa karta\" data-collapsetext=\"Dölj karta\" style=\"float:right; clear:right;\"\n|-\n!\n|-\n|\n";
                                forkpage.text += collapseintro;
                            }
                            forkpage.text += mp(72) + "+|" + locatordict[cs].locatorname + "\n |caption = " + caption + "\n  |float = right\n  |width=" + mapsize.ToString() + "\n  | places =";
                            inum = 0;
                            foreach (forkclass ff in fd[cs])
                            {
                                inum++;
                                forkpage.text += mp(72) + "~|" + locatordict[cs].locatorname + "| label = " + inum.ToString() + "| mark =Blue_pog.svg|position=right|background=white|lat=" + ff.latitude.ToString(culture_en) + "|long=" + ff.longitude.ToString(culture_en) + "}}\n";
                            }
                            forkpage.text += "}}\n";
                            if (makelang == "sv")
                                forkpage.text += "|}\n"; //collapse-end
                        }

                        List<string> artnames = new List<string>();
                        foreach (forkclass ff in fd[cs])
                        {
                            string artname = s;
                            if (artnamedict.ContainsKey(ff.geonameid))
                            {
                                //if (artnamedict[ff.geonameid] != "X")
                                //    artname = artnamedict[ff.geonameid];
                                artname = artnamedict[ff.geonameid].Replace("*", "");
                            }
                            if (artnames.Contains(artname))
                            {
                                string existing = "";
                                if (artnamedict[ff.geonameid].Contains("*"))
                                    existing = "*";
                                if (!forkdoubles.Contains(existing + s))
                                {
                                    forkdoubles.Add(existing + s);
                                    hasanomaly = true;
                                    //forkpage.text = saveanomaly(forkpage.title, mp(201)) + forkpage.text;
                                    if (!String.IsNullOrEmpty(anomalytext))
                                        anomalytext += " ";
                                    anomalytext += mp(201) + " [[" + artname + "]]";
                                }

                            }
                            else
                                artnames.Add(artname);
                            foreach (forkclass ff2 in fd[cs])
                            {
                                if (ff2 != ff)
                                {
                                    if (get_distance_latlong(ff.latitude, ff.longitude, ff2.latitude, ff2.longitude) < 1.0)
                                    {
                                        Console.WriteLine("featurecodes potential anomaly: " + ff.featurecode+" "+ff2.featurecode);
                                        if (!(((ff.featurecode.Contains("PPL")) && (ff2.featurecode.Contains("ADM"))) || ((ff.featurecode.Contains("ADM")) && (ff2.featurecode.Contains("PPL")))))
                                        {
                                            //forkpage.text = saveanomaly(forkpage.title, mp(202)) + forkpage.text;
                                            hasanomaly = true;
                                            Console.WriteLine("Has anomaly");
                                            string artname2 = "";
                                            if (artnamedict.ContainsKey(ff.geonameid))
                                            {
                                                artname2 = artnamedict[ff2.geonameid].Replace("*", "");
                                            }

                                            if (!String.IsNullOrEmpty(anomalytext))
                                                anomalytext += " ";
                                            anomalytext += mp(202) + " [[" + artname + "]]" + " [[" + artname2 + "]]";
                                            //Console.ReadLine();
                                            break;
                                        }
                                        //Console.ReadLine();
                                    }
                                }
                                else
                                    break;
                            }
                            string fstart = "#";
                            if (worldmaponly)
                            {
                                inum++;
                                fstart = "*"+inum.ToString();
                            }
                            string ss = fstart + " [[" + artname.Replace("*", "") + "]], ";
                            if (!artname.Contains("(" + getfeaturelabel(ciso, ff.featurecode, ff.geonameid)))
                                ss += getfeaturelabel(ciso, ff.featurecode, ff.geonameid) + ", ";
                            if (!String.IsNullOrEmpty(ff.admname[1]) && !artname.Contains(ff.admname[1]))
                                ss += ff.admname[1] + ", ";
                            if (!String.IsNullOrEmpty(ff.admname[2]) && !artname.Contains(ff.admname[2]))
                                ss += ff.admname[2] + ", ";
                            //if (!artname.Contains(" lat "))
                            //{
                            //    ss += ", lat. " + ff.latitude.ToString("F1", culture);
                            //    ss += ", long. " + ff.longitude.ToString("F1", culture);
                            //}

                            //Console.WriteLine(make_coord_template(ciso, ff.featurecode, ff.latitude, ff.longitude));
                            //Console.ReadLine();

                            ss += make_coord_template(ciso, ff.featurecode, ff.latitude, ff.longitude, artname.Replace("*", ""));

                            ss += comment("Geonames ID " + ff.geonameid.ToString());

                            forkpage.text += ss + "\n";
                            nbranches++;
                        }
                    }
                    //}


                    forkpage.text += "\n{{" + mp(69) + "}}\n";
                    //forkpage.text += "[[" + mp(70) + "]]\n";

                    if ((makelang == "sv") && (!is_latin(forkpage.title)))
                    {
                        string alph_sv = get_alphabet_sv(get_alphabet(remove_disambig(forkpage.title)));
                        if (!alph_sv.Contains("okänd"))
                            forkpage.text += "{{Sidnamn annan skrift|" + alph_sv + "}}\n";
                        else
                        {
                            Console.WriteLine(forkpage.title);
                            Console.WriteLine(remove_disambig(forkpage.title));
                            Console.WriteLine(alph_sv);
                            //Console.ReadLine();
                        }

                    }

                    string[] p215 = new string[] { "", getmonthstring() };
                    forkpage.AddToCategory(mp(215, p215).Trim());
                    p215[1] = "";
                    foreach (string csl in scountry.Keys)
                    {
                        p215[0] = csl;
                        forkpage.AddToCategory(mp(215, p215).Trim());
                    }


                    if (nbranches > 1)
                    {
                        if (hasanomaly)
                            forkpage.text = saveanomaly(forkpage.title, anomalytext) + forkpage.text;

                        forkpage.text = cleanup_text(forkpage.text);

                        if (forkpage.text != origtext)
                        {
                            if (willoverwrite)
                                trysave(forkpage, 2, mp(303) + " " + makesite.defaultEditComment);
                            else
                                trysave(forkpage, 2);
                        }

                        nfork++;
                        //if ( s == "Andorra" )
                        //    Console.ReadLine();
                        Console.WriteLine("nfork = " + nfork.ToString());
                        romanian_redirect(forkpage.title);

                    }

                }


            }
            Console.WriteLine(stats.GetStat());
            if (pstats == null)
            {
                pstats = new Page(makesite, mp(13) + botname + "/Statistik");
                pstats.Load();
            }
            //pstats.text += "\n\n== [[" + countryml[makecountryname] + "]] grensidor ==\n\n";
            pstats.text += stats.GetStat();
            trysave(pstats, 1, mp(302) + " " + countryml[makecountryname]);
            stats.ClearStat();

            Console.WriteLine("nfork = " + nfork.ToString());
            foreach (string fd in forkdoubles)
                Console.WriteLine(fd);
            using (StreamWriter sw = new StreamWriter("forkdoubles.txt"))
            {
                foreach (string ul in forkdoubles)
                    sw.WriteLine(ul);
            }

            Console.WriteLine("forkdoubles = " + forkdoubles.Count.ToString());

            Console.WriteLine("nu2019 = " + nu2019.ToString());
        }

        public static void print_geonameid(int id)
        {
            if (gndict.ContainsKey(id))
            {
                Console.WriteLine("Name = " + gndict[id].Name);
                Console.WriteLine("Country = " + gndict[gndict[id].adm[0]].Name);
                Console.WriteLine("Province = " + gndict[gndict[id].adm[1]].Name);
            }
        }

        public static void read_locatorlist()
        {
            int n = 0;


            using (StreamReader sr = new StreamReader(geonamesfolder + "locatorlist.txt"))
            {
                int makelangcol = -1;
                int altcol = -1;
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();

                    if (line[0] == '#')
                        continue;

                    //if (n > 250)
                    //    Console.WriteLine(line);

                    string[] words = line.Split(tabchar);

                    //foreach (string s in words)
                    //    Console.WriteLine(s);

                    //Console.WriteLine(words[0] + "|" + words[1]);

                    if (words[0] == "en") //headline
                    {
                        for (int i = 1; i < words.Length; i++)
                        {
                            if (words[i] == makelang)
                                makelangcol = i;
                            if (words[i] == makelang + "-alt")
                                altcol = i;
                        }
                        Console.WriteLine("makelangcol = " + makelangcol.ToString());
                        Console.WriteLine("altcol = " + altcol.ToString());
                        continue;
                    }
                    //Console.WriteLine("wl = " + words.Length.ToString());

                    locatorclass lc = new locatorclass();
                    lc.locatorname = words[makelangcol];
                    if ((words.Length > altcol) && (!String.IsNullOrEmpty(words[altcol])))
                        lc.altlocator = words[altcol];

                    if ((words.Length > makelangcol) && (!String.IsNullOrEmpty(words[makelangcol])))
                        locatordict.Add(words[0], lc);

                    n++;
                    if ((n % 10) == 0)
                    {
                        Console.WriteLine("n (locatorlist)   = " + n.ToString());

                    }

                }

                Console.WriteLine("n    (locatorlist)= " + n.ToString());

            }

        }

        public static void fill_admcap()
        {
            if (admcap.Count != 0)
                return;

            if (makelang == "ceb")
            {
                for (int i = 1; i < 5; i++)
                {
                    foreach (string mc in admdict.Keys)
                    {
                        if ((!String.IsNullOrEmpty(admdict[mc].label[i - 1])) && (!admcap.ContainsKey(admdict[mc].label[i - 1])))
                            admcap.Add(admdict[mc].label[i - 1], "kapital sa " + admdict[mc].label[i - 1]);
                    }
                }
            }
            else if (makelang == "sv")
            {
                admcap.Add("administrativ atoll", "atollhuvudort");
                admcap.Add("administrativ by", "byhuvudort");
                admcap.Add("administrativ enhet", "enhetshuvudort");
                admcap.Add("administrativ ö", "öhuvudort");
                admcap.Add("administrativt område", "områdeshuvudort");
                admcap.Add("arrondissement", "arrondissementhuvudort");
                admcap.Add("barrio", "barriohuvudort");
                admcap.Add("byutvecklingskommitté", "byutvecklingskommittéhuvudort");
                admcap.Add("community", "communityhuvudort");
                admcap.Add("constituency", "constituencyhuvudort");
                admcap.Add("corregimiento", "corregimientohuvudort");
                admcap.Add("county", "countyhuvudort");
                admcap.Add("delegation", "delegationshuvudort");
                admcap.Add("delstat", "delstatshuvudstad");
                admcap.Add("departement", "departementshuvudort");
                admcap.Add("distrikt", "distriktshuvudort");
                admcap.Add("division", "divisionshuvudort");
                admcap.Add("emirat", "emirathuvudstad");
                admcap.Add("entitet", "entitetshuvudort");
                admcap.Add("fylke", "fylkeshuvudort");
                admcap.Add("förbundsland", "förbundslandshuvudstad");
                admcap.Add("församling", "församlingshuvudort");
                admcap.Add("gemenskap", "gemenskapshuvudort");
                admcap.Add("gewog", "gewoghuvudort");
                admcap.Add("grannskap", "grannskapshuvudort");
                admcap.Add("grevskap", "grevskapshuvudort");
                admcap.Add("guvernement", "guvernementshuvudort");
                admcap.Add("härad", "häradshuvudort");
                admcap.Add("hövdingadöme", "hövdingadömeshuvudort");
                admcap.Add("hövdingaråd", "hövdingarådshuvudort");
                admcap.Add("kabupaten", "kabupatenhuvudort");
                admcap.Add("kanton", "kantonhuvudstad");
                admcap.Add("klan", "klanhuvudort");
                admcap.Add("kommun", "kommunhuvudort");
                admcap.Add("krets", "kretshuvudort");
                admcap.Add("kungadöme", "kungadömeshuvudstad");
                admcap.Add("kvarter", "kvartershuvudort");
                admcap.Add("lokalstyresområde", "lokalstyresområdeshuvudort");
                admcap.Add("län", "länshuvudort");
                admcap.Add("mahaliyya", "mahaliyyahuvudort");
                admcap.Add("mukim", "mukimhuvudort");
                admcap.Add("oblast", "oblasthuvudort");
                admcap.Add("oblyst", "oblysthuvudort");
                admcap.Add("område", "områdeshuvudort");
                admcap.Add("opština", "opštinahuvudort");
                admcap.Add("parroqui", "parroquihuvudort");
                admcap.Add("powiat", "powiathuvudort");
                admcap.Add("prefektur", "prefekturhuvudort");
                admcap.Add("provins", "provinshuvudstad");
                admcap.Add("rajon", "rajonhuvudort");
                admcap.Add("region", "regionhuvudort");
                admcap.Add("autonom region", "regionhuvudort");
                admcap.Add("regeringsdistrikt", "regeringsdistriktshuvudort");
                admcap.Add("riksdel", "riksdelshuvudstad");
                admcap.Add("rote", "rotehuvudort");
                admcap.Add("rådsområde", "rådsområdeshuvudort");
                admcap.Add("samhällsutvecklingsråd", "samhällsutvecklingsrådshuvudort");
                admcap.Add("sektor", "sektorshuvudort");
                admcap.Add("shehia", "shehiahuvudort");
                admcap.Add("socken", "sockenhuvudort");
                admcap.Add("stad", "stadshuvudort");
                admcap.Add("stadsdel", "stadsdelshuvudort");
                admcap.Add("subbarrio", "subbarriohuvudort");
                admcap.Add("subdistrikt", "subdistriktshuvudort");
                admcap.Add("subprefektur", "subprefekturhuvudort");
                admcap.Add("sýsla", "sýslahuvudort");
                admcap.Add("tehsil", "tehsilhuvudort");
                admcap.Add("territorium", "territoriehuvudort");
                admcap.Add("tidigare kommun", "huvudort för tidigare kommun");
                admcap.Add("underdistrikt", "underdistriktshuvudort");
                admcap.Add("utvecklingsregion", "utvecklingsregionshuvudort");
                admcap.Add("ward", "wardhuvudort");
                admcap.Add("voblast", "voblasthuvudort");
                admcap.Add("vojvodskap", "vojvodskapshuvudort");
                admcap.Add("zon", "zonhuvudort");
                admcap.Add("åldermannaskap", "åldermannaskapshuvudort");
                admcap.Add("ö och specialkommun", "öhuvudort");
                admcap.Add("ögrupp", "ögruppshuvudort");
                admcap.Add("öområde", "öområdeshuvudort");
                admcap.Add("öråd", "örådshuvudort");
                admcap.Add("parish", "parishhuvudort");
                admcap.Add("parroquia", "parroquiahuvudort");
                admcap.Add("freguesia", "freguesiahuvudort");
                admcap.Add("kraj", "krajhuvudort");
                admcap.Add("delrepublik", "delrepublikhuvudstad");
                admcap.Add("autonomt distrikt", "distriktshuvudort");
                admcap.Add("köping", "köpinghuvudort");
            }


        }

        public static void read_adm()
        {
            int n = 0;

            List<string> uniquelabels = new List<string>();

            string lf = "";

            if (firstround)
            {
                using (StreamReader sr = new StreamReader(geonamesfolder + "adm-" + makelang + ".txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();

                        //if (n > 250)
                        //Console.WriteLine(line);

                        string[] words = line.Split(tabchar);
                        while (words.Length < 11)
                        {
                            line += tabstring;
                            words = line.Split(tabchar);
                        }

                        //Console.WriteLine("wl = " + words.Length.ToString());

                        admclass ad = new admclass();

                        int maxlabel = 0;

                        for (int i = 0; i < 5; i++)
                        {
                            ad.label[i] = words[i + 1];
                            if (!String.IsNullOrEmpty(words[i + 1]))
                                maxlabel = i + 1;
                            if (!uniquelabels.Contains(words[i + 1]))
                                uniquelabels.Add(words[i + 1]);
                            ad.det[i] = words[i + 6];

                            if (!admtodet.ContainsKey(ad.label[i]))
                                admtodet.Add(ad.label[i], ad.det[i]);
                        }

                        ad.maxadm = maxlabel;
                        if (words[0] == makecountry)
                            Console.WriteLine(words[0] + ": maxadm = " + maxlabel.ToString());

                        admdict.Add(words[0], ad);

                        if (saveadmlinks)
                        {
                            lf += "* " + linkcountry(words[0]) + "\n";
                            for (int i = 0; i < 5; i++)
                            {
                                if (!String.IsNullOrEmpty(ad.label[i]))
                                    lf += ":* ADM" + (i + 1).ToString() + ": [[" + ad.label[i] + "]]\n";
                            }

                        }


                        n++;
                        if ((n % 10) == 0)
                        {
                            Console.WriteLine("n (adm)   = " + n.ToString());

                        }

                    }

                    if (makelang == "sv")
                    {
                        if (!admtodet.ContainsKey("kraj"))
                            admtodet.Add("kraj", "krajen");
                        if (!admtodet.ContainsKey("köping"))
                            admtodet.Add("köping", "köpingen");
                        if (!admtodet.ContainsKey("socken"))
                            admtodet.Add("socken", "socknen");
                        if (!admtodet.ContainsKey("autonomt distrikt"))
                            admtodet.Add("autonomt distrikt", "det autonoma distriktet");
                        if (!admtodet.ContainsKey("delrepublik"))
                            admtodet.Add("delrepublik", "delrepubliken");
                    }

                    Console.WriteLine("n    (adm)= " + n.ToString());
                    if (saveadmlinks)
                    {
                        Page pf = new Page(makesite, mp(13) + botname + "/linkadmin");
                        pf.text = lf;
                        trysave(pf, 1,"Bot saving linkadmin");
                    }
                }

                //using (StreamWriter sw = new StreamWriter("uniquelabels.txt"))
                //{
                //    foreach (string ul in uniquelabels)
                //        sw.WriteLine(ul);
                //}
                //Console.WriteLine("unique labels written");
                //Console.ReadLine();
            }

            fill_admcap();

            if (makecountry != "")
            {
                if (admdict.ContainsKey(makecountry))
                {
                    string[] p167 = new string[1] { countryml[countrydict[countryid[makecountry]].Name] };
                    string admlink = mp(167, p167);
                    for (int i = 1; i < 5; i++)
                    {
                        if (!String.IsNullOrEmpty(admdict[makecountry].label[i - 1]))
                        {
                            if (!featurearticle.ContainsKey(admdict[makecountry].label[i - 1]))
                                featurearticle.Add(admdict[makecountry].label[i - 1], admlink);
                            else
                                featurearticle[admdict[makecountry].label[i - 1]] = admlink;
                        }
                        string fc = "PPLA";
                        if (i > 1)
                            fc += i.ToString();
                        if (admcap.ContainsKey(admdict[makecountry].label[i - 1]))
                        {
                            featuredict[fc] = admcap[admdict[makecountry].label[i - 1]];
                            if (!featurearticle.ContainsKey(admcap[admdict[makecountry].label[i - 1]]))
                                featurearticle.Add(admcap[admdict[makecountry].label[i - 1]], admlink);
                        }
                    }

                }
                else
                {

                }
            }





            //Console.ReadLine();

        }

        public static string getfeaturelabel(string countrycode, string fcode, int gnid)
        {
            return removearticle(getfeaturelabelindet(countrycode, fcode, gnid));
        }

        public static string getfeaturelabelindet(string countrycode, string fcode, int gnid)
        {
            string rs = "";

            if (!featuredict.ContainsKey(fcode))
                return "unknown feature";

            if (specialfeaturedict.ContainsKey(gnid))
                return specialfeaturedict[gnid];

            if (fcode.Contains("PPLA"))
            {
                int level = 1;
                if (fcode != "PPLA")
                    level = tryconvert(fcode.Replace("PPLA", ""));
                if ((level > 0) && (level <= 5))
                {
                    string admlabel = getadmlabel(countrycode, level, gnid);
                    if (admcap.ContainsKey(admlabel))
                    {
                        if (makelang == "sv")
                            rs = "en " + admcap[admlabel];
                        else
                            rs = admcap[admlabel];
                    }
                    else
                        rs = featuredict[fcode];
                }
                else
                    rs = featuredict[fcode];
            }
            else if (fcode.Contains("ADM"))
            {
                int level = tryconvert(fcode.Replace("ADM", ""));
                if ((level > 0) && (level <= 5))
                {
                    rs = getadmindet(countrycode, level, gnid);
                }
            }

            if (String.IsNullOrEmpty(rs))
                rs = featuredict[fcode];

            Console.WriteLine("getfeaturelabelindet = " + rs);
            return rs;

        }

        public static bool is_zhen(int gnid)
        {
            bool zhenfound = false;
            if (gndict[gnid].Name.ToLower().Contains(" zhen"))
                zhenfound = true;
            else
            {
                if (altdict.ContainsKey(gnid))
                {
                    foreach (altnameclass ac in altdict[gnid])
                    {
                        if (ac.altname.ToLower().Contains(" zhen"))
                        {
                            zhenfound = true;
                            break;
                        }
                    }
                }
            }
            Console.WriteLine("is_zhen = " + zhenfound.ToString());
            return zhenfound;
        }

        public static string getadmlabel(string countrycode, int level, int gnid)
        {
            string rs = "";
            if (specialfeaturedict.ContainsKey(gnid))
                rs = specialfeaturedict[gnid];
            else if (admdict.ContainsKey(countrycode))
            {
                if (level <= admdict[countrycode].maxadm)
                    rs = admdict[countrycode].label[level - 1];
            }
            else
            {
                switch (countrycode)
                {
                    case "MY":
                        if ((gndict.ContainsKey(gnid)) && (gndict[gnid].longitude > 106.0))
                            rs = getadmlabel("MY2", level, gnid);
                        else
                            rs = getadmlabel("MY1", level, gnid);
                        break;
                    case "GB": //different for different kingdoms in United Kingdom
                        int kingdom = 6269131;
                        if (gndict.ContainsKey(gnid))
                            kingdom = gndict[gnid].adm[1];
                        switch (kingdom)
                        {
                            case 6269131: //England
                                rs = getadmlabel("GB1", level, gnid);
                                break;
                            case 2641364: //Northern Ireland
                                rs = getadmlabel("GB2", level, gnid);
                                break;
                            case 2638360: //Scotland
                                rs = getadmlabel("GB3", level, gnid);
                                break;
                            case 2634895: //Wales
                                rs = getadmlabel("GB4", level, gnid);
                                break;
                            default:
                                rs = getadmlabel("GB1", level, gnid);
                                break;

                        }
                        break;
                    case "RU":
                        if (level == 1)
                        {
                            if (gndict.ContainsKey(gnid))
                            {
                                if (gndict[gnid].Name.Contains(" Oblast"))
                                    rs = "oblast";
                                else if ((gndict[gnid].Name.Contains(" Krai")) || (gndict[gnid].Name.Contains(" Kray")))
                                    rs = "kraj";
                                else if (gndict[gnid].Name.Contains(" Okrug"))
                                    rs = "autonomt distrikt";
                                else
                                    rs = "delrepublik";
                            }
                            else
                            {
                                rs = "oblast";
                            }
                        }
                        else
                        {
                            rs = (admdict["RU1"].label[level - 1]);
                        }
                        break;
                    case "CN":
                        if (level == 4)
                        {
                            if (gndict.ContainsKey(gnid))
                            {
                                bool zhenfound = is_zhen(gnid);
                                if (zhenfound)
                                {
                                    rs = mp(297);
                                }
                                else
                                    rs = (admdict["CN1"].label[level - 1]);
                            }
                            else
                            {
                                rs = (admdict["CN1"].label[level - 1]);
                            }
                        }
                        else
                        {
                            rs = (admdict["CN1"].label[level - 1]);
                        }
                        break;

                    default:
                        rs = (admdict["default"].label[level - 1]);
                        break;
                }
            }

            Console.WriteLine("getadmlabel = " + rs);
            return rs;
        }

        public static string getadmindet(string countrycode, int level, int gnid)
        {
            string rs = getadmlabel(countrycode, level, gnid);

            if (makelang == "sv")
            {
                if (getadmdet(countrycode, level, gnid).EndsWith("t"))
                    rs = "ett " + rs;
                else
                    rs = "en " + rs;
            }

            return rs;
        }

        public static string getadmdet(string countrycode, int level, int gnid)
        {
            string rs = getadmlabel(countrycode, level, gnid);
            if (admtodet.ContainsKey(rs))
                rs = admtodet[rs];

            if (makelang == "ceb")
                rs += " sa";

            return rs;

        }

        public static double get_distance(int gnidfrom, int gnidto)
        {
            double gnidlat = gndict[gnidto].latitude;
            double gnidlong = gndict[gnidto].longitude;
            double countrylat = gndict[gnidfrom].latitude;
            double countrylong = gndict[gnidfrom].longitude;

            return get_distance_latlong(countrylat, countrylong, gnidlat, gnidlong);

        }

        public static double get_distance_latlong(double fromlat, double fromlong, double tolat, double tolong) //returns distance in km
        {
            double kmdeg = 40000 / 360; //km per degree at equator
            double scale = Math.Cos(fromlat * 3.1416 / 180); //latitude-dependent longitude scale
            double dlat = (tolat - fromlat) * kmdeg;
            double dlong = (tolong - fromlong) * kmdeg * scale;

            double dist = Math.Sqrt(dlat * dlat + dlong * dlong);

            if (dist > 1000.0) //use great circle distance (Haversine formula)
            {
                double f1 = fromlat * Math.PI / 180.0; //convert to radians
                double f2 = tolat * Math.PI / 180.0;
                double l1 = fromlong * Math.PI / 180.0;
                double l2 = tolong * Math.PI / 180.0;
                double r = 6371.0; //Earth radius

                double underroot = Math.Pow(Math.Sin((f2 - f1) / 2), 2) + Math.Cos(f1) * Math.Cos(f2) * Math.Pow(Math.Sin((l2 - l1) / 2), 2);
                double root = Math.Sqrt(underroot);
                if (root > 1)
                    root = 1;
                dist = 2 * r * Math.Asin(root);

            }

            return dist;

        }


        public static double[] get_article_coord(Page p)
        {
            double lat = 9999.9;
            double lon = 9999.9;
            double[] latlong = { lat, lon };
            int ncoord = 0;

            if (coordparams.Count == 0)
            {
                coordparams.Add("Coord");
                coordparams.Add("coord");
                coordparams.Add("lat_d");
                coordparams.Add("lat_g");
                coordparams.Add("latitude");
                coordparams.Add("latitud");
                coordparams.Add("nordliggrad");
                coordparams.Add("sydliggrad");
                coordparams.Add("breddgrad");
            }


            Dictionary<string, int> geotempdict = new Dictionary<string, int>();

            //string template = mp(63);
            foreach (string tt in p.GetTemplates(true, true))
            {
                if (tt.Length < 5)
                    continue;
                string cleantt = tt.Replace("\n", "").Trim().Substring(0, 5).ToLower();
                Console.WriteLine("cleantt = |" + cleantt + "|");
                //if (true)//(geolist.Contains(template + cleantt))
                //{
                //geotemplatefound = true;
                //Console.WriteLine("Possible double");

                if (!geotempdict.ContainsKey(cleantt))
                    geotempdict.Add(cleantt, 1);
                else
                    geotempdict[cleantt]++;
                bool foundwithparams = false;
                //foreach (string ttt in p.GetTemplates(true, true))
                //    if (ttt.IndexOf(tt) == 0)
                //{
                foundwithparams = true;
                //Console.WriteLine("foundwithparams");
                if (cleantt == "coord")
                {
                    Console.WriteLine("found {{coord}}");
                    string coordstring = tt;
                    if (coordstring.Length > 10)
                    {
                        double newlat = coordlat(coordstring);
                        double newlon = coordlong(coordstring);
                        if (newlat + newlon < 720.0)
                        {
                            if (ncoord == 0)
                            {
                                lat = newlat;
                                lon = newlon;
                            }
                            else if ((Math.Abs(newlat - lat) + Math.Abs(newlon - lon) > 0.01)) //two different coordinates in article; skip!
                            {
                                lat = 9999;
                                lon = 9999;
                                ncoord = 9999;
                                break;
                            }
                            else
                            {
                                lat = newlat;
                                lon = newlon;
                            }
                        }
                        if (lat + lon < 720.0)
                            ncoord++;
                        if (ncoord > 3)
                            break;
                    }

                }
                else
                {
                    Dictionary<string, string> pdict = Page.ParseTemplate(tt);
                    foreach (string cp in coordparams)
                    {
                        Console.WriteLine("cp = " + cp);
                        double oldlat = lat;
                        double oldlon = lon;
                        if (pdict.ContainsKey(cp))
                        {
                            //coordfound = true;
                            Console.WriteLine("found coordparams");
                            switch (cp)
                            {
                                case "latitude":
                                case "latitud":
                                    lat = tryconvertdouble(pdict[cp]);
                                    if (pdict.ContainsKey("longitude"))
                                        lon = tryconvertdouble(pdict["longitude"]);
                                    else if (pdict.ContainsKey("longitud"))
                                        lon = tryconvertdouble(pdict["longitud"]);
                                    else
                                        Console.WriteLine("latitude but no longitude");
                                    break;
                                case "nordliggrad":
                                case "sydliggrad":
                                    lat = tryconvertdouble(pdict[cp]);
                                    if (pdict.ContainsKey("östliggrad"))
                                        lon = tryconvertdouble(pdict["östliggrad"]);
                                    else if (pdict.ContainsKey("västliggrad"))
                                        lon = tryconvertdouble(pdict["västliggrad"]);
                                    else
                                        Console.WriteLine("latitude but no longitude");
                                    break;
                                case "breddgrad":
                                    lat = tryconvertdouble(pdict[cp]);
                                    if (pdict.ContainsKey("längdgrad"))
                                        lon = tryconvertdouble(pdict["längdgrad"]);
                                    else
                                        Console.WriteLine("latitude but no longitude");
                                    break;
                                case "lat_d":
                                case "latd":
                                case "lat_g":
                                    double llat = 0.0;
                                    llat = tryconvertdouble(pdict[cp]);
                                    if (llat > 0)
                                    {
                                        lat = llat;
                                        if (pdict.ContainsKey("long_d"))
                                            lon = tryconvertdouble(pdict["long_d"]);
                                        else if (pdict.ContainsKey("longd"))
                                            lon = tryconvertdouble(pdict["longd"]);
                                        else if (pdict.ContainsKey("long_g"))
                                            lon = tryconvertdouble(pdict["long_g"]);
                                        if (pdict.ContainsKey("lat_m"))
                                            lat += tryconvertdouble(pdict["lat_m"]) / 60;
                                        if (pdict.ContainsKey("long_m"))
                                            lon += tryconvertdouble(pdict["long_m"]) / 60;
                                        if (pdict.ContainsKey("lat_s"))
                                            lat += tryconvertdouble(pdict["lat_s"]) / 3600;
                                        if (pdict.ContainsKey("long_s"))
                                            lon += tryconvertdouble(pdict["long_s"]) / 3600;
                                        if (pdict.ContainsKey("lat_NS"))
                                        {
                                            if (pdict["lat_NS"].ToUpper() == "S")
                                                lat = -lat;
                                        }
                                        if (pdict.ContainsKey("long_EW"))
                                        {
                                            if (pdict["long_EW"].ToUpper() == "W")
                                                lon = -lon;
                                        }
                                    }
                                    break;
                                case "Coord":
                                case "coord": //{{Coord|42|33|18|N|1|31|59|E|region:AD_type:city|display=title,inline}}
                                    string coordstring = pdict[cp];
                                    if (coordstring.Length > 10)
                                    {
                                        lat = coordlat(coordstring);
                                        lon = coordlong(coordstring);
                                    }
                                    break;
                                default:
                                    Console.WriteLine("coord-default:" + tt);
                                    break;


                            }
                            if (lat + lon < 720.0)
                            {
                                if ((Math.Abs(oldlat - lat) + Math.Abs(oldlon - lon) > 0.01)) //two different coordinates in article; skip!
                                {
                                    lat = 9999;
                                    lon = 9999;
                                    ncoord = 9999;
                                    break;
                                }
                            }
                            else
                            {
                                lat = oldlat;
                                lon = oldlon;
                            }

                            if (lat + lon < 720.0)
                                ncoord++;
                            if (ncoord > 3)
                                break;



                        }
                    }
                }
                //}
                if (!foundwithparams)
                    Console.WriteLine("Params not found");
                Console.WriteLine("lat = " + lat.ToString());
                Console.WriteLine("lon = " + lon.ToString());
                //}
            }

            if (ncoord > 4) //several coordinate sets, probably a list or something; return failure
                return latlong;

            latlong[0] = lat;
            latlong[1] = lon;
            return latlong;
        }

        //public static double[] get_article_coord_old(Page p)
        //{
        //    double lat = 9999.9;
        //    double lon = 9999.9;
        //    double[] latlong = { lat, lon };

        //    if (coordparams.Count == 0)
        //    {
        //        coordparams.Add("coord");
        //        coordparams.Add("lat_d");
        //        coordparams.Add("lat_g");
        //        coordparams.Add("latitude");
        //        coordparams.Add("latitud");
        //    }


        //    Dictionary<string, int> geotempdict = new Dictionary<string, int>();

        //    string template = mp(63);
        //    foreach (string tt in p.GetTemplates(false, true))
        //    {
        //        string cleantt = initialcap(tt.Replace("\n", "").Trim());
        //        Console.WriteLine("tt = |" + cleantt + "|");
        //        if (true)//(geolist.Contains(template + cleantt))
        //        {
        //            //geotemplatefound = true;
        //            Console.WriteLine("Possible double");

        //            if (!geotempdict.ContainsKey(cleantt))
        //                geotempdict.Add(cleantt, 1);
        //            else
        //                geotempdict[cleantt]++;
        //            bool foundwithparams = false;
        //            foreach (string ttt in p.GetTemplates(true, true))
        //                if (ttt.IndexOf(tt) == 0)
        //                {
        //                    foundwithparams = true;
        //                    Console.WriteLine("foundwithparams");
        //                    if (cleantt == "Coord")
        //                    {
        //                        Console.WriteLine("found {{coord}}");
        //                        string coordstring = ttt;
        //                        if (coordstring.Length > 10)
        //                        {
        //                            lat = coordlat(coordstring);
        //                            lon = coordlong(coordstring);
        //                        }

        //                    }
        //                    else
        //                    {
        //                        Dictionary<string, string> pdict = makesite.ParseTemplate(ttt);
        //                        foreach (string cp in coordparams)
        //                        {
        //                            Console.WriteLine("cp = " + cp);
        //                            if (pdict.ContainsKey(cp))
        //                            {
        //                                //coordfound = true;
        //                                Console.WriteLine("found coordparams");
        //                                switch (cp)
        //                                {
        //                                    case "latitude":
        //                                    case "latitud":
        //                                        lat = tryconvertdouble(pdict[cp]);
        //                                        if (pdict.ContainsKey("longitude"))
        //                                            lon = tryconvertdouble(pdict["longitude"]);
        //                                        else if (pdict.ContainsKey("longitud"))
        //                                            lon = tryconvertdouble(pdict["longitud"]);
        //                                        else
        //                                            Console.WriteLine("latitude but no longitude");
        //                                        break;
        //                                    case "lat_d":
        //                                    case "latd":
        //                                    case "lat_g":
        //                                        double llat = 0.0;
        //                                        llat = tryconvertdouble(pdict[cp]);
        //                                        if (llat > 0)
        //                                        {
        //                                            lat = llat;
        //                                            if (pdict.ContainsKey("long_d"))
        //                                                lon = tryconvertdouble(pdict["long_d"]);
        //                                            else if (pdict.ContainsKey("longd"))
        //                                                lon = tryconvertdouble(pdict["longd"]);
        //                                            else if (pdict.ContainsKey("long_g"))
        //                                                lon = tryconvertdouble(pdict["long_g"]);
        //                                            if (pdict.ContainsKey("lat_m"))
        //                                                lat += tryconvertdouble(pdict["lat_m"]) / 60;
        //                                            if (pdict.ContainsKey("long_m"))
        //                                                lon += tryconvertdouble(pdict["long_m"]) / 60;
        //                                            if (pdict.ContainsKey("lat_s"))
        //                                                lat += tryconvertdouble(pdict["lat_s"]) / 3600;
        //                                            if (pdict.ContainsKey("long_s"))
        //                                                lon += tryconvertdouble(pdict["long_s"]) / 3600;
        //                                            if (pdict.ContainsKey("lat_NS"))
        //                                            {
        //                                                if (pdict["lat_NS"].ToUpper() == "S")
        //                                                    lat = -lat;
        //                                            }
        //                                            if (pdict.ContainsKey("long_EW"))
        //                                            {
        //                                                if (pdict["long_EW"].ToUpper() == "W")
        //                                                    lon = -lon;
        //                                            }
        //                                        }
        //                                        break;
        //                                    case "coord": //{{Coord|42|33|18|N|1|31|59|E|region:AD_type:city|display=title,inline}}
        //                                        string coordstring = pdict["coord"];
        //                                        if (coordstring.Length > 10)
        //                                        {
        //                                            lat = coordlat(coordstring);
        //                                            lon = coordlong(coordstring);
        //                                        }
        //                                        break;
        //                                    default:
        //                                        Console.WriteLine("coord-default:" + ttt);
        //                                        break;


        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            if (!foundwithparams)
        //                Console.WriteLine("Params not found");
        //            Console.WriteLine("lat = " + lat.ToString());
        //            Console.WriteLine("lon = " + lon.ToString());
        //        }
        //    }
        //    latlong[0] = lat;
        //    latlong[1] = lon;
        //    return latlong;
        //}

        public static int get_direction_latlong(double fromlat, double fromlong, double tolat, double tolong)
        {
            double kmdeg = 40000.0 / 360.0; //km per degree at equator
            double scale = Math.Cos(fromlat * 3.1416 / 180); //latitude-dependent longitude scale
            double dlat = (tolat - fromlat) * kmdeg;

            double dlong = (tolong - fromlong) * kmdeg * scale;

            if (Math.Abs(tolong - fromlong) > 180.0)
            {
                if (tolong > fromlong)
                    dlong = (tolong - (fromlong + 360)) * kmdeg * scale;
                else
                    dlong = ((tolong + 360) - fromlong) * kmdeg * scale;
            }

            //Console.WriteLine("dlat,dlong = " + dlat.ToString() + " " + dlong.ToString());
            if (dlat * dlat > 4.0 * dlong * dlong)
            {
                if (dlat > 0) // north
                    return 1;
                else           //south
                    return 2;
            }
            else if (dlong * dlong > 4.0 * dlat * dlat)
            {
                if (dlong > 0) // east
                    return 4;
                else            //west
                    return 3;
            }
            else if (dlong > 0)
            {
                if (dlat > 0) //northeast
                    return 5;
                else           //southeast
                    return 6;
            }
            else
            {
                if (dlat > 0) //northwest
                    return 7;
                else           //southwest
                    return 8;
            }

            //      1
            //   7    5
            //  3      4
            //   8    6
            //      2
        }

        public static int get_direction(int gnidfrom, int gnidto)
        {
            double tolat = gndict[gnidto].latitude;
            double tolong = gndict[gnidto].longitude;
            double fromlat = gndict[gnidfrom].latitude;
            double fromlong = gndict[gnidfrom].longitude;
            return get_direction_latlong(fromlat, fromlong, tolat, tolong);

        }

        public static int get_pix_direction(int x, int y, int x0, int y0, double scale)
        {
            double dlat = y0 - y; //reverse sign; +y=south!
            double dlong = (x - x0) * scale;

            //Console.WriteLine("dlat,dlong = " + dlat.ToString() + " " + dlong.ToString());
            if (dlat * dlat > 4.0 * dlong * dlong)
            {
                if (dlat > 0) // north
                    return 1;
                else           //south
                    return 2;
            }
            else if (dlong * dlong > 4.0 * dlat * dlat)
            {
                if (dlong > 0) // east
                    return 4;
                else            //west
                    return 3;
            }
            else if (dlong > 0)
            {
                if (dlat > 0) //northeast
                    return 5;
                else           //southeast
                    return 6;
            }
            else
            {
                if (dlat > 0) //northwest
                    return 7;
                else           //southwest
                    return 8;
            }

            //      1
            //   7    5
            //  3      4
            //   8    6
            //      2

        }

        public static int[] getdircoord(int dir) //translate from direction codes of get_pix_direction() into +/-1 x +/-1 y
        {
            int[] rc = new int[2] { 0, 0 };
            switch (dir)
            {
                case 1:
                    rc[1] = 1;
                    break;
                case 2:
                    rc[1] = -1;
                    break;
                case 3:
                    rc[0] = -1;
                    break;
                case 4:
                    rc[0] = 1;
                    break;
                case 5:
                    rc[0] = 1;
                    rc[1] = 1;
                    break;
                case 6:
                    rc[0] = 1;
                    rc[1] = -1;
                    break;
                case 7:
                    rc[0] = -1;
                    rc[1] = 1;
                    break;
                case 8:
                    rc[0] = -1;
                    rc[1] = -1;
                    break;
            }
            return rc;
        }

        public static coordclass countrylatlong(int gnid)
        {
            coordclass cc = new coordclass();

            if (!gndict.ContainsKey(gnid))
                return cc;

            cc.lat = gndict[gnid].latitude;
            cc.lon = gndict[gnid].longitude;

            if (countrydict.ContainsKey(gnid))
            {
                if (countrydict[gnid].shape != null)
                {
                    if (countrydict[gnid].shape.metadict.ContainsKey("Centroid latitude"))
                        cc.lat = tryconvertdouble(countrydict[gnid].shape.metadict["Centroid latitude"]);
                    if (countrydict[gnid].shape.metadict.ContainsKey("Centroid longitude"))
                        cc.lon = tryconvertdouble(countrydict[gnid].shape.metadict["Centroid longitude"]);
                }
            }

            return cc;
        }

        public static int getcountrypart(int gnid)
        {
            double gnidlat = gndict[gnid].latitude;
            double gnidlong = gndict[gnid].longitude;
            coordclass cc = countrylatlong(gndict[gnid].adm[0]);
            double countrylat = cc.lat;
            double countrylong = cc.lon;
            double area = countrydict[gndict[gnid].adm[0]].area;
            double kmdeg = 40000 / 360; //km per degree at equator
            double scale = Math.Cos(0.5 * (countrylat + gnidlat) * 3.1416 / 180); //latitude-dependent longitude scale
            double dlat = (gnidlat - countrylat) * kmdeg;
            double dlong = (gnidlong - countrylong) * kmdeg * scale;

            if (countrylat < -80) //Antarctica
            {
                if (gnidlat < -86) //central part
                    return 82;

                if ((gnidlat > -61) && ((gnidlong > -47) && (gnidlong < -44))) //South Orkney Islands
                    return 84;

                if (gnidlong > -45) //East Antarctica
                    return 86;
                else                 //West Antarctica
                {
                    if ((gnidlong > -64) && (gnidlong < -52))
                    {
                        if (((gnidlong < -60) && (gnidlat > -64)) || ((gnidlong >= -60) && (gnidlat > -62.8))) //South Shetland Islands
                            return 83;
                        else
                            return 85;
                    }
                    else
                        return 85;
                }
            }

            if ((dlat * dlat + dlong * dlong) < (area / 9)) //central part
                return 82;

            if (dlat * dlat > 4 * dlong * dlong)
            {
                if (dlat > 0) // northern part
                    return 83;
                else           //southern part
                    return 84;
            }
            else if (dlong * dlong > 4 * dlat * dlat)
            {
                if (dlong > 0) // eastern part
                    return 86;
                else            //western part
                    return 85;
            }
            else if (dlong > 0)
            {
                if (dlat > 0) //northeastern
                    return 87;
                else           //southeastern
                    return 88;
            }
            else
            {
                if (dlat > 0) //northwestern
                    return 89;
                else           //southwestern
                    return 90;
            }


        }

        public static void fill_motherdict() //mother countries of colonies
        {
            if (motherdict.Count > 0)
                return;
            motherdict.Add("AN", "NL");

            motherdict.Add("AS", "US");
            motherdict.Add("AW", "NL");
            motherdict.Add("AX", "FI");
            motherdict.Add("BL", "FR");
            motherdict.Add("BV", "NO");
            motherdict.Add("CK", "NZ");
            motherdict.Add("CX", "AU");
            motherdict.Add("FK", "GB");
            motherdict.Add("FM", "US");
            motherdict.Add("FO", "DK");
            motherdict.Add("GF", "FR");
            motherdict.Add("GG", "GB");
            motherdict.Add("GI", "GB");
            motherdict.Add("GL", "DK");
            motherdict.Add("GP", "FR");
            motherdict.Add("GU", "US");
            motherdict.Add("HK", "CN");
            motherdict.Add("HM", "AU");
            motherdict.Add("IM", "GB");
            motherdict.Add("IO", "GB");
            motherdict.Add("JE", "GB");
            motherdict.Add("MF", "FR");
            motherdict.Add("MH", "US");
            motherdict.Add("MO", "CN");
            motherdict.Add("MP", "US");
            motherdict.Add("MQ", "FR");
            motherdict.Add("MS", "GB");
            motherdict.Add("NU", "NZ");
            motherdict.Add("PF", "FR");
            motherdict.Add("PM", "FR");
            motherdict.Add("PN", "GB");
            motherdict.Add("PR", "US");
            motherdict.Add("RE", "FR");
            motherdict.Add("SH", "GB");
            motherdict.Add("SJ", "NO");
            motherdict.Add("TC", "GB");
            motherdict.Add("TF", "FR");
            motherdict.Add("TK", "NZ");
            motherdict.Add("UM", "US");
            motherdict.Add("VG", "GB");
            motherdict.Add("VI", "US");
            motherdict.Add("WF", "FR");
            motherdict.Add("YT", "FR");
            motherdict.Add("SX", "NL");
            motherdict.Add("CC", "AU");
            motherdict.Add("BM", "GB");
            motherdict.Add("CW", "NL");
            motherdict.Add("GS", "GB");
            motherdict.Add("KY", "GB");
            motherdict.Add("NC", "FR");
            motherdict.Add("NF", "AU");
            Console.WriteLine("Motherdict: " + motherdict.Count.ToString());
        }

#if (DBGEOFLAG)

        public static string reorient_polygon(string text)
        {
            string s = text.Replace("POLYGON ((","").Replace("))","");

            string rs = "";

            bool first = true;
            foreach (string cs in s.Split(','))
            {
                if (first)
                {
                    first = false;
                }
                else
                    rs = ", " + rs;

                rs = cs + rs;
            }

            rs = "POLYGON ((" + rs + "))";
            //Console.WriteLine("Original  : " + text);
            //Console.WriteLine("Reoriented: " + rs);
            return rs;
        }

        public static string close_polygon(string text)
        {
            string rs = text.Replace("POLYGON ((", "").Replace("))", "");

            

            bool first = true;
            string firstpoint = "";
            string lastpoint = "";
            foreach (string cs in rs.Trim().Split(','))
            {
                if (first)
                {
                    first = false;
                    firstpoint = cs.Trim();
                }
                lastpoint = cs.Trim();
            }
            if (lastpoint != firstpoint) //Add first point at the end
            {
                Console.WriteLine("Closing polygon");
                rs += ", " + firstpoint;
            }

            rs = "POLYGON ((" + rs + "))";
            if (text != rs)
            {
                Console.WriteLine("Original  : " + text);
                Console.WriteLine("Closed: " + rs);
                Console.ReadLine();
            }
            return rs;
        }

        public static bool clockwise(string text, double cx, double cy)
        {
            //bool cw = true;

            string s = text.Replace("POLYGON ((", "").Replace("))", "");

            double anglesum = 0;

            bool first = true;
            double oldx = 0;
            double oldy = 0;
            double oldangle = 0;
            foreach (string cs in s.Split(','))
            {
                //Console.WriteLine(cs);
                double x = tryconvertdouble(cs.Trim().Split(' ')[0]);
                double y = tryconvertdouble(cs.Trim().Split(' ')[1]);
                //Console.WriteLine(x + " " + y);
                double angle = Math.Atan2(y - cy, x - cx);
                //Console.WriteLine("angle = " + angle);
                if (first)
                {
                    first = false;
                }
                else
                {
                    double anglechange = angle - oldangle;
                    if (anglechange > Math.PI)
                        anglechange = anglechange - 2 * Math.PI;
                    else if (anglechange < -Math.PI)
                        anglechange = anglechange + 2 * Math.PI;
                    //Console.WriteLine("anglechange = " + anglechange);

                    anglesum += anglechange;
                }
                oldx = x;
                oldy = y;
                oldangle = angle;
            }

            return (anglesum < 0);
        }

        public static DbGeography tryfromtext(string text)
        {
             

            try
            {
                DbGeography dg = DbGeography.FromText(text);
                return dg;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
                //Console.WriteLine(e.InnerException.Message);
                try
                {
                    DbGeography dg = DbGeography.FromText(reorient_polygon(text));
                    return dg;
                }
                catch (Exception e2)
                {
                    Console.WriteLine(e2.Message);
                    Console.WriteLine(e2.InnerException.Message);
                    return null;
                }
            }

            //return null;
        }

        public static string strip_polygon(string text,bool naked)
        {
            if ( naked ) //keep no parentheses
                return text.Replace("POLYGON ((", "").Replace("))", "");
            else //keep single parentheses
                return text.Replace("POLYGON ((", "(").Replace("))", ")");
        }

        public static void convert_shapelist(string filename)
        {
            List<shapeclass> shapelist = read_shapelist(filename + ".shp.txt");

            write_shapelist(filename + ".multipoly.txt", shapelist);

        }

        public static void read_lakeshapes()
        {
            Console.WriteLine("read_lakeshapes: glwd_1.multipoly.txt");
            foreach (shapeclass sc in read_shapelist("glwd_1.multipoly.txt"))
            {
                int glwd_id = -1;
                if (sc.metadict.ContainsKey("glwd_id"))
                {
                    glwd_id = tryconvert(sc.metadict["glwd_id"]);
                    lakeshapedict.Add(glwd_id, sc);
                }
            }
            Console.WriteLine("read_lakeshapes: glwd_2.multipoly.txt");
            foreach (shapeclass sc in read_shapelist("glwd_2.multipoly.txt"))
            {
                int glwd_id = -1;
                if (sc.metadict.ContainsKey("glwd_id"))
                {
                    glwd_id = tryconvert(sc.metadict["glwd_id"]);
                    lakeshapedict.Add(glwd_id, sc);
                }
            }

            Console.WriteLine("read_lakeshapes: glwd-countries.txt");

            using (StreamReader sr = new StreamReader(geonamesfolder + "glwd-countries.txt"))
            {
                while (!sr.EndOfStream)
                {

                    string line = sr.ReadLine();
                    string[] words = line.Split(tabchar);
                    if (words.Length < 2)
                        continue;
                    if ( !countryid.ContainsKey(words[0]))
                        continue;
                    int gnid = countryid[words[0]];

                    if (!countrylakedict.ContainsKey(gnid))
                    {
                        List<int> ll = new List<int>();
                        countrylakedict.Add(gnid, ll);
                    }
                    for (int i = 1; i < words.Length; i++)
                    {
                        int glwd_id = tryconvert(words[i]);
                        if ((glwd_id > 0 ) && (lakeshapedict.ContainsKey(glwd_id)))
                            countrylakedict[gnid].Add(glwd_id);
                    }

                }
            }
        }

        public static void make_glwd_countries()
        {

            foreach (shapeclass sc in read_shapelist("glwd_1.multipoly.txt"))
            {
                int glwd_id = -1;
                if (sc.metadict.ContainsKey("glwd_id"))
                {
                    glwd_id = tryconvert(sc.metadict["glwd_id"]);
                    lakeshapedict.Add(glwd_id, sc);

                    List<int> countries = new List<int>();
                    foreach (DbGeography dlake in sc.shapes)
                    {
                        foreach (int gnid in countrydict.Keys)
                        {
                            bool found = false;
                            if (countrydict[gnid].shape != null)
                            {
                                foreach (DbGeography dcountry in countrydict[gnid].shape.shapes)
                                {
                                    if (dcountry.Intersects(dlake))
                                    {
                                        found = true;
                                        DbGeography dis = dcountry.Intersection(dlake);
                                        Console.WriteLine("Intersection : " + dis.Area + ", " + dlake.Area);
                                    }
                                    else
                                    {
                                        Console.Write("No intersection : ");
                                        DbGeography dis = dcountry.Intersection(dlake);
                                        if (dis == null)
                                            Console.WriteLine("dis = null");
                                        else if (dis.IsEmpty)
                                            Console.WriteLine("dis.IsEmpty");
                                        else
                                            Console.WriteLine("dis.Area = "+dis.Area);
                                    }
                                    Console.ReadLine();
                                }
                            }
                            if (found)
                                countries.Add(gnid);
                        }
                    }

                    Console.Write("Countries matching: ");
                    foreach (int gnid in countries)
                    {
                        Console.Write(countrydict[gnid].Name);
                        if ( !countrylakedict.ContainsKey(gnid))
                        {
                            List<int> ll = new List<int>();
                            countrylakedict.Add(gnid,ll);
                        }
                        countrylakedict[gnid].Add(glwd_id);
                    }
                    Console.WriteLine();
                    Console.Write("Countries in lake file: ");
                    if (sc.metadict.ContainsKey("country"))
                        Console.Write(sc.metadict["country"]);
                    if (sc.metadict.ContainsKey("sec_cntry"))
                        Console.Write(sc.metadict["sec_cntry"]);
                    Console.WriteLine();
                }
            }

            foreach (shapeclass sc in read_shapelist("glwd_2.multipoly.txt"))
            {
                int glwd_id = -1;
                if (sc.metadict.ContainsKey("glwd_id"))
                {
                    glwd_id = tryconvert(sc.metadict["glwd_id"]);
                    lakeshapedict.Add(glwd_id, sc);

                    List<int> countries = new List<int>();
                    foreach (DbGeography dlake in sc.shapes)
                    {
                        foreach (int gnid in countrydict.Keys)
                        {
                            bool found = false;
                            if (countrydict[gnid].shape != null)
                            {
                                foreach (DbGeography dcountry in countrydict[gnid].shape.shapes)
                                {
                                    if (dcountry.Intersects(dlake))
                                    {
                                        found = true;
                                    }
                                }
                            }
                            if (found)
                                countries.Add(gnid);
                        }
                    }

                    Console.Write("Countries matching: ");
                    foreach (int gnid in countries)
                    {
                        Console.Write(countrydict[gnid].Name);
                        if (!countrylakedict.ContainsKey(gnid))
                        {
                            List<int> ll = new List<int>();
                            countrylakedict.Add(gnid, ll);
                        }
                        countrylakedict[gnid].Add(glwd_id);
                    }
                    Console.WriteLine();
                    Console.Write("Countries in lake file: ");
                    if (sc.metadict.ContainsKey("country"))
                        Console.Write(sc.metadict["country"]);
                    if (sc.metadict.ContainsKey("sec_cntry"))
                        Console.Write(sc.metadict["sec_cntry"]);
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Writing glwd-countries.txt...");
            using (StreamWriter sw = new StreamWriter("glwd-countries.txt"))
            {
                foreach (int gnid in countrylakedict.Keys)
                {
                    sw.Write(countrydict[gnid].iso);
                    foreach (int glwd_id in countrylakedict[gnid])
                        sw.Write("\t"+glwd_id);
                    sw.WriteLine();
                }
            }
            Console.WriteLine("End of lakeshapes");
            Console.ReadLine();
        }

        public static List<shapeclass> read_shapelist(string filename)
        {
            int n = 0;

            //Testing:

            //DbGeography dg1 = DbGeography.FromText("POINT (30 10)");
            //dg1 = DbGeography.FromText("POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))");
            //Console.WriteLine("Test 1 ok");
            //dg1 = DbGeography.FromText("POLYGON ((30 10, 40.5 40.5, 20 40, 10 20, 30 10))");
            //Console.WriteLine("Test 2 ok");
            //dg1 = DbGeography.FromText("POLYGON ((-2 0,-1 -1,0 0,1 -1,2 0,0 2,-2 0))");
            //Console.WriteLine("Test 3 ok");
            //dg1 = DbGeography.FromText("POLYGON ((-2 0,-1.5 -1.5,0 0,1 -1,2 0,0 2,-2 0))");
            //Console.WriteLine("Test 3 ok again");
            //dg1 = tryfromtext("POLYGON ((-30 10,40.5 40.5,20 -40, 10 20,-30 10))");
            //Console.WriteLine("Test 4 ok");
            //dg1 = tryfromtext("POLYGON ((-69.9969376289999 12.577582098, -69.936390754 12.531724351, -69.924672004 12.519232489, -69.9157608709999 12.4970156920001, -69.8801977199999 12.453558661, -69.8768204419999 12.4273949240001, -69.888091601 12.417669989, -69.9088028639999 12.4177920590001, -69.9305313789999 12.4259707700001, -69.9451391269999 12.4403750670001, -69.924672004 12.4403750670001, -69.924672004 12.4472110050001, -69.9585668609999 12.4632022160001, -70.027658658 12.5229352890001, -70.0480850899999 12.5311546900001, -70.0580948559999 12.5371768250001, -70.0624080069999 12.54682038, -70.060373502 12.5569522160001, -70.0510961579999 12.5740420590001, -70.0487361319999 12.5837263040001, -70.052642382 12.600002346, -70.0596410799999 12.614243882, -70.0611059239999 12.6253929710001, -70.0487361319999 12.6321475280001, -70.0071508449999 12.5855166690001, -69.9969376289999 12.577582098))");
            //Console.WriteLine("Test 5 ok");

            //string s = "POLYGON ((10 10,10 20,20 20,20 10,10 10))";
            //Console.WriteLine("Clockwise = "+clockwise(s,15,15));
            //Console.WriteLine("Clockwise(reoriented) = " + clockwise(reorient_polygon(s), 15, 15));
            //Console.ReadLine();

            //        public class shapeclass
            //{
            //    public Dictionary<string, string> metadict = new Dictionary<string, string>();
            //    public List<DbGeography> shapes = new List<DbGeography>();
            //}

            List<shapeclass> shapelist = new List<shapeclass>();

            bool mpfile = filename.Contains("multipoly");
            int ngood = 0;
            int nfail = 0;
            int nccwsgood = 0;
            int nccwsfail = 9999;
            int ncwsgood = 0;
            int ncwsfail = 9999;
            int mpgood = 0;
            int mpfail = 9999;


            using (StreamReader sr = new StreamReader(geonamesfolder + filename))
            {
                while (!sr.EndOfStream)
                {
                    shapeclass sc = new shapeclass();
                    double areasum = 0.0;
                    double xwsum = 0.0;
                    double ywsum = 0.0;

                    Dictionary<string, DbGeometry> clockwiselist = new Dictionary<string, DbGeometry>();
                    Dictionary<string, DbGeometry> counterclockwiselist = new Dictionary<string, DbGeometry>();
                    Dictionary<string, List<string>> multipoly = new Dictionary<string, List<string>>();
                    string largest = "";
                    double largestarea = -1;

                    while (true)
                    {
                        String line = sr.ReadLine();
                        //Console.WriteLine(line);
                        if (line[0] == '#')
                        {
                            //shapelist.Add(sc);
                            //Console.WriteLine("break");
                            break;
                        }

                        //if (n > 250)
                        //    Console.WriteLine(line);


                        if (line.IndexOf("POLYGON") == 0)
                        {
                            //Console.WriteLine("POLYGON");

                            if (mpfile)
                                sc.shapes.Add(DbGeography.FromText(line));
                            else
                            {
                                DbGeometry dm = DbGeometry.FromText(line);
                                DbGeometry dmc = dm.Centroid;

                                //bool cw1 = clockwise(line, (double)dmc.XCoordinate, (double)dmc.YCoordinate);
                                //if ( !filename.Contains("glwd"))
                                line = reorient_polygon(line);
                                line = close_polygon(line);
                                bool cw2 = clockwise(line, (double)dmc.XCoordinate, (double)dmc.YCoordinate);
                                if (cw2)
                                    clockwiselist.Add(line, dm);
                                else
                                    counterclockwiselist.Add(line, dm);
                                //Console.WriteLine("Clockwise              = " + cw1);
                                //Console.WriteLine("Clockwise (reoriented) = " + cw2);
                                //if (cw1 == cw2)
                                //{
                                //    Console.WriteLine(line);
                                //    Console.ReadLine();
                                //}

                                DbGeography dg = tryfromtext(line);
                                if (dg == null)
                                {
                                    Console.WriteLine("null dg");
                                    break;
                                }
                                double area = -1;
                                
                                if (dg.Area != null)
                                    area = (double)dg.Area;
                                if (cw2)
                                    area = -area;

                                if (area > largestarea)
                                {
                                    largestarea = area;
                                    largest = line;
                                }
                                //Console.WriteLine("Area = " + area/1000000);
                                areasum += area;
                                xwsum += area * (double)dmc.XCoordinate;
                                ywsum += area * (double)dmc.YCoordinate;

                                //sc.shapes.Add(dg);
                            }
                        }
                        else if (line.IndexOf("POINT") == 0)
                        {
                            sc.shapes.Add(DbGeography.FromText(line));
                        }
                        else if (line.IndexOf("MULTIPOLYGON") == 0)
                        {
                            sc.shapes.Add(DbGeography.FromText(line));
                        }
                        else
                        {
                            string[] words = line.Split(tabchar);
                            if (words.Count() > 1)
                            {
                                if ( !sc.metadict.ContainsKey(words[0]))
                                    sc.metadict.Add(words[0], words[1]);
                                else
                                    Console.WriteLine(line);
                            }
                        }
                    }

                    if (!mpfile)
                    {
                        foreach (string cws in clockwiselist.Keys)
                        {
                            //bool found = false;
                            foreach (string ccws in counterclockwiselist.Keys)
                            {
                                if (clockwiselist[cws].Within(counterclockwiselist[ccws]))
                                {
                                    //found = true;
                                    if (!multipoly.ContainsKey(ccws))
                                    {
                                        List<string> ls = new List<string>();
                                        multipoly.Add(ccws, ls);
                                    }
                                    multipoly[ccws].Add(cws);
                                    break;
                                }
                            }
                        }

                        Console.WriteLine("ccws: " + counterclockwiselist.Count);
                        Console.WriteLine("cws:  " + clockwiselist.Count);
                        Console.WriteLine("multipoly: " + multipoly.Count);
                        //if ( clockwiselist.Count > 0 )
                        //Console.ReadLine();

                        string ms = "";
                        int np = 0;
                        if (counterclockwiselist.Count == 1)
                        {
                            ms = "POLYGON ()";
                        }
                        else if (counterclockwiselist.Count > 1)
                        {
                            ms = "MULTIPOLYGON (())";
                        }
                        else
                            continue;
                        
                        int mpmax = 0;
                        foreach (string ccws in counterclockwiselist.Keys)
                        {
                            string cstrip = strip_polygon(ccws, false); ;
                            if (multipoly.ContainsKey(ccws))
                            {
                                
                                foreach (string cws in multipoly[ccws])
                                {
                                    cstrip += ", " + strip_polygon(cws, false);
                                }
                                if (multipoly[ccws].Count > mpmax)
                                    mpmax = multipoly[ccws].Count;
                            }

                            if (np == 0)
                                ms = ms.Replace("()", "(" + cstrip + ")#"); //# marking where to insert next
                            else
                            {
                                ms = ms.Replace("#)", ", (" + cstrip + ")#)");
                            }
                            np++;
                        }

                        //Console.WriteLine(ms);

                        ms = ms.Replace("#", "");

                        DbGeography dmp = tryfromtext(ms);
                        if (dmp == null)
                        {
                            Console.WriteLine(ms);
                            Console.WriteLine(ms.Substring(0, 50));
                            Console.WriteLine("dmp=null");
                            if (sc.metadict.ContainsKey("lake_name"))
                                Console.WriteLine(sc.metadict["lake_name"]);
                            if (sc.metadict.ContainsKey("name_long"))
                                Console.WriteLine(sc.metadict["name_long"]);

                            //Console.ReadLine();

                            Console.WriteLine("try with just largest piece:");
                            dmp = tryfromtext(largest); //try with just largest piece
                            Console.WriteLine("Largest area = " + largestarea/1000000);
                            Console.WriteLine("Areasum = " + areasum / 1000000);
                            //Console.ReadLine();

                            if (dmp == null)
                            {
                                nfail++;
                                if (nccwsfail > counterclockwiselist.Count)
                                    nccwsfail = counterclockwiselist.Count;
                                if (ncwsfail > clockwiselist.Count)
                                    ncwsfail = clockwiselist.Count;
                                if (mpfail > mpmax)
                                    mpfail = mpmax;
                                continue;
                            }
                        }
                        else
                        {
                            Console.WriteLine(ms.Substring(0, 30));
                            if (mpgood < mpmax)
                                mpgood = mpmax;

                        }


                        sc.shapes.Add(dmp);

                        double clat = ywsum / areasum;
                        double clon = xwsum / areasum;
                        sc.metadict.Add("Centroid latitude", clat.ToString());
                        sc.metadict.Add("Centroid longitude", clon.ToString());
                        sc.metadict.Add("Areasum", areasum.ToString());
                    }

                    if (sc.metadict.ContainsKey("lake_name"))
                        Console.WriteLine(sc.metadict["lake_name"]);
                    if (sc.metadict.ContainsKey("name_long"))
                        Console.WriteLine(sc.metadict["name_long"]);
                    shapelist.Add(sc);
                    ngood++;
                    if (nccwsgood < counterclockwiselist.Count)
                        nccwsgood = counterclockwiselist.Count;
                    if (ncwsgood < clockwiselist.Count)
                        ncwsgood = clockwiselist.Count;
                    if (mpgood < multipoly.Count)
                        mpgood = multipoly.Count;

                    //Console.ReadLine();

                    n++;
                }

            }

            Console.WriteLine("Shapes done, n = "+n);
            Console.WriteLine("Shapes good, ngood = " + ngood);
            Console.WriteLine("Shapes bad, nfail = " + nfail);
            Console.WriteLine("nccwsmax good, ngood = " + nccwsgood);
            Console.WriteLine("nccwsmin bad, nfail = " + nccwsfail);
            Console.WriteLine("ncwsmax good, ngood = " + ncwsgood);
            Console.WriteLine("ncwsmin bad, nfail = " + ncwsfail);
            Console.WriteLine("mpmaxmin good, nfail = " + mpgood);
            Console.WriteLine("mpmaxmin bad, nfail = " + mpfail);
            //Console.ReadLine();
            return shapelist;
        }

        public static void write_shapelist(string filename, List<shapeclass> shapelist)
        {
            //        public class shapeclass
            //{
            //    public Dictionary<string, string> metadict = new Dictionary<string, string>();
            //    public List<DbGeography> shapes = new List<DbGeography>();
            //}

            using (StreamWriter sw = new StreamWriter(geonamesfolder + filename))
            {
                foreach (shapeclass sc in shapelist)
                {
                    foreach (string s in sc.metadict.Keys)
                        sw.WriteLine(s + "\t" + sc.metadict[s]);
                    foreach (DbGeography dg in sc.shapes)
                    {
                        sw.WriteLine(dg.AsText());
                    }
                    sw.WriteLine("#");
                }
            }

        }



#endif

        public static void read_country_info()
        {
            int n = 0;


            using (StreamReader sr = new StreamReader(geonamesfolder + "countryInfo.txt"))
            {
                int makelangcol = -1;
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();

                    if (line[0] == '#')
                        continue;

                    //if (n > 250)
                    //    Console.WriteLine(line);

                    string[] words = line.Split(tabchar);

                    //foreach (string s in words)
                    //    Console.WriteLine(s);

                    //Console.WriteLine(words[0] + "|" + words[1]);

                    if (words[0] == "ISO") //headline
                    {
                        for (int i = 1; i < words.Length; i++)
                        {
                            if (words[i] == makelang)
                                makelangcol = i;
                        }
                        continue;
                    }

                    int geonameid = -1;

                    countryclass country = new countryclass();

                    country.Name = words[4];
                    geonameid = tryconvert(words[16]);
                    country.iso = words[0];
                    country.iso3 = words[1];
                    country.isonumber = tryconvert(words[2]);
                    country.fips = words[3];
                    country.capital = words[5];
                    country.area = tryconvertdouble(words[6]);
                    country.population = tryconvertlong(words[7]);
                    country.continent = words[8];
                    country.tld = words[9];
                    country.currencycode = words[10];
                    country.currencyname = words[11];
                    country.phone = words[12];
                    country.postalcode = words[13];
                    foreach (string ll in words[15].Split(','))
                    {
                        //Console.WriteLine("ll.Split('-')[0] = " + ll.Split('-')[0]);
                        string lcode = ll.Split('-')[0];
                        if (String.IsNullOrEmpty(country.nativewiki))
                            country.nativewiki = lcode;
                        if (langtoint.ContainsKey(lcode))
                            country.languages.Add(langtoint[lcode]);
                    }
                    foreach (string ll in words[17].Split(','))
                        country.bordering.Add(ll);

                    if (makelangcol > 0)
                    {
                        country.Name_ml = words[makelangcol];
                    }
                    else
                    {
                        country.Name_ml = country.Name;
                    }
                    countryml.Add(country.Name, country.Name_ml);
                    countryiso.Add(country.Name, country.iso);

                    if (geonameid > 0)
                    {
                        countryid.Add(country.iso, geonameid);

                        countrydict.Add(geonameid, country);
                        //Console.WriteLine(country.iso+":"+geonameid.ToString());
                    }

                    n++;
                    if ((n % 10) == 0)
                    {
                        Console.WriteLine("n (country_info)   = " + n.ToString());

                    }

                }

                Console.WriteLine("n    (country_info)= " + n.ToString());

                if (savewikilinks)
                {
                    Page pt = new Page(makesite, mp(13) + botname + "/countrylinks");
                    pt.text = "Country links used by Lsjbot\n\n";
                    foreach (string cn in countryml.Keys)
                        pt.text += "*  [[" + countryml[cn] + "]]\n";
                    trysave(pt, 1,"Bot saving countrylinks");
                }



            }

            fill_motherdict();
            fill_nocapital();

#if (DBGEOFLAG)

            List<shapeclass> shapelist = read_shapelist("ne_10m_admin_0_countries.multipoly.txt");
            foreach (shapeclass sc in shapelist)
            {
                if (sc.metadict.ContainsKey("iso_a2") & sc.metadict.ContainsKey("name"))
                {
                    //Console.WriteLine(sc.metadict["iso_a2"] + " " + sc.metadict["name"] + " " + sc.metadict["Centroid latitude"] + " " + sc.metadict["Centroid longitude"] + " " + sc.metadict["Areasum"]);
                    if (countryid.ContainsKey(sc.metadict["iso_a2"]))
                    {
                        int gnid = countryid[sc.metadict["iso_a2"]];
                        if (countrydict.ContainsKey(gnid))
                            countrydict[gnid].shape = sc;
                    }
                }

            }

            //write_shapelist("ne_10m_admin_0_countries.multipoly.txt",shapelist);

            //Console.ReadLine();
#endif
        }

        public static void fill_nocapital()
        {
            nocapital.Add("HK");
            nocapital.Add("IO");
            nocapital.Add("GI");
            nocapital.Add("MC");
            nocapital.Add("MO");
            nocapital.Add("SG");
            nocapital.Add("VA");
        }

        public static string countrytitle(int gnid)
        {
            string rs = "";
            string iso = "";
            if (countrydict.ContainsKey(gnid))
                iso = countrydict[gnid].iso;

            if (makelang != "sv")
                return rs;

            switch (iso)
            {
                case "MT":
                case "IE":
                case "IS":
                    rs = "republiken ";
                    break;
                case "FK":
                    rs = "territoriet ";
                    break;
                default:
                    rs = "";
                    break;

            }

            return rs;

        }

        public static string linkcountry(int gnid)
        {
            if (countrydict.ContainsKey(gnid))
                return linkcountry(countrydict[gnid].iso);
            else
                return "";
        }

        public static string linkcountry(string ciso)
        {
            if (!countryid.ContainsKey(ciso))
                return ciso;
            int gnid = countryid[ciso];
            string rt = "[[" + countryml[countrydict[gnid].Name] + "]]";
            if (motherdict.ContainsKey(ciso))
            {
                //int mothergnid = countryid[motherdict[ciso]];
                string mama = linkcountry(motherdict[ciso]);
                if (((motherdict[ciso] == "DK") || (motherdict[ciso] == "NL")) && (makelang == "sv"))
                    mama = mama.Replace("[[", "[[Kungariket ");
                rt += " (" + mama + ")";
            }

            return rt;
        }

        public static void get_country_iw(string langcode)
        {
            //Console.WriteLine("get country iw " + langcode);
            //using (StreamWriter sw = new StreamWriter("countrynames-" + langcode + ".csv"))
            //{

            //    foreach (int gnid in countrydict.Keys)
            //    {
            //        string langname = countrydict[gnid].Name;
            //        List<string> iwlist = Interwiki(wdsite, countrydict[gnid].Name);
            //        foreach (string iws in iwlist)
            //        {
            //            string[] ss = iws.Split(':');
            //            string iwcode = ss[0];
            //            string iwtitle = ss[1];
            //            //Console.WriteLine("iw - " + iwcode + ":" + iwtitle);
            //            if (iwcode == langcode)
            //                langname = iwtitle;
            //        }
            //        sw.WriteLine(countrydict[gnid].Name + ";" + langname);
            //        Console.WriteLine(countrydict[gnid].Name + ";" + langname);


            //    }
            //}
        }

        public static List<string> Interwiki(Site site, string title)

        //Borrowed from http://sv.wikipedia.org/wiki/Wikipedia:Projekt_DotNetWikiBot_Framework/Innocent_bot/Addbotkopia
        {
            List<string> r = new List<string>();
            XmlDocument doc = new XmlDocument();

            string url = "action=wbgetentities&sites=enwiki&titles=" + HttpUtility.UrlEncode(title) + "&languages=en&format=xml";
            //string tmpStr = site.PostDataAndGetResultHTM(site.site+"/w/api.php", url);
            try
            {
                //string tmpStr = site.PostDataAndGetResultHTM(site.site + "/w/api.php", url);
                string tmpStr = site.PostDataAndGetResult(site.address + "/w/api.php", url);
                doc.LoadXml(tmpStr);
                for (int i = 0; i < doc.GetElementsByTagName("sitelink").Count; i++)
                {
                    string s = doc.GetElementsByTagName("sitelink")[i].Attributes.GetNamedItem("site").Value;
                    string t = doc.GetElementsByTagName("sitelink")[i].Attributes.GetNamedItem("title").Value;
                    s = s.Replace("_", "-");
                    string t2 = s.Substring(0, s.Length - 4) + ":" + t;
                    //Console.WriteLine(t2);
                    r.Add(t2);
                }
            }
            catch (WebException e)
            {
                string message = e.Message;
                Console.Error.WriteLine(message);
            }

            return r;
        }

        public static string fnum(double x)
        {
            string rt = "{{formatnum:";
            if (x < 1.0)
                rt += x.ToString("F2", culture_en);
            else if (x < 30.0)
                rt += x.ToString("F1", culture_en);
            else
                rt += x.ToString("F0", culture_en);
            rt += "}}";
            return rt;
        }

        public static string fnum(long i)
        {
            return "{{formatnum:" + i.ToString() + "}}";
        }

        public static string fnum(int i)
        {
            return "{{formatnum:" + i.ToString() + "}}";
        }

        public static void fill_kids_features()
        {
            foreach (int gnid in gndict.Keys)
            {
                int parent = 0;
                for (int i = 0; i < 5; i++)
                    if ((gndict[gnid].adm[i] > 0) && (gndict[gnid].adm[i] != gnid))
                        parent = gndict[gnid].adm[i];
                if ((gndict[gnid].featureclass == 'A') && (gndict[gnid].featurecode.Contains("ADM")) && (!gndict[gnid].featurecode.Contains("ADMD")) && (!gndict[gnid].featurecode.Contains("H")))
                {
                    if (gndict.ContainsKey(parent))
                        gndict[parent].children.Add(gnid);
                }
                else
                {
                    if (gndict.ContainsKey(parent))
                        gndict[parent].features.Add(gnid);
                }

            }
        }

        public static void make_mapimage(int[,] sourcemap, string mapfile)
        {
            int mapsize = sourcemap.GetLength(0);
            Bitmap map = new Bitmap(mapsize, mapsize);

            for (int x = 0; x < mapsize; x++)
                for (int y = 0; y < mapsize; y++)
                {
                    Color col = Color.White;

                    if (sourcemap[x, y] <= 0)
                        col = Color.Blue;
                    else if (sourcemap[x, y] > 10000)
                        col = Color.Red;
                    else
                        col = Color.Green;
                    map.SetPixel(x, y, col);
                }

            map.Save(mapfile, ImageFormat.Jpeg);
            map.Dispose();
        }

        public static void makeworldmap()
        {
            Bitmap map = new Bitmap(3600, 1800);

            int n = 0;

            string filename = geonamesfolder;

            filename += "allCountries.txt";

            using (StreamReader sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();

                    if (line[0] == '#')
                        continue;

                    //if (n > 250)
                    //    Console.WriteLine(line);

                    string[] words = line.Split(tabchar);
                    double lat = tryconvertdouble(words[4]);
                    double lon = tryconvertdouble(words[5]);

                    double scale = 0.5 + 0.5 * Math.Cos(lat * 3.1416 / 180); //latitude-dependent longitude scale

                    int x = Convert.ToInt32((lon * scale + 180) * 10);
                    int y = 1800 - Convert.ToInt32((lat + 90) * 10);

                    if ((x >= 0) && (x < 3600) && (y >= 0) && (y < 1800))
                    {

                        Color oldcol = map.GetPixel(x, y);
                        Color newcol = Color.White;
                        if (oldcol.GetBrightness() < 1.0)
                        {
                            int nargb = oldcol.ToArgb() + 0x00030303;
                            newcol = Color.FromArgb(nargb);
                        }
                        map.SetPixel(x, y, newcol);
                    }
                    else
                        Console.WriteLine("lat,lon,x,y = " + lat.ToString() + " " + lon.ToString() + " " + x.ToString() + " " + y.ToString());
                    n++;
                    if ((n % 10000) == 0)
                    {
                        Console.WriteLine("n (world map)   = " + n.ToString());
                        //if (n >= 500000)
                        //    break;

                    }
                }
            }



            map.Save(geonamesfolder + "worldmap.jpg", ImageFormat.Jpeg);
            map.Dispose();
        }

        public static void read_languageiso()
        {
            //public class langclass
            //{
            //    public string iso3 = "";
            //    public string iso2 = "";
            //    public Dictionary<string,string> name = new Dictionary<string,string>(); //name of language in different language. Iso -> name.
            //}

            //public static Dictionary<int,langclass> langdict = new Dictionary<int,langclass>(); //main language table
            //public static Dictionary<string, int> langtoint = new Dictionary<string, int>(); //from iso to integer code. Both iso2 and iso3 used as keys to the same int
            int n = 0;


            using (StreamReader sr = new StreamReader(geonamesfolder + "language-iso.txt"))
            {
                //int makelangcol = -1;

                String headline = sr.ReadLine();
                string[] heads = headline.Split(tabchar);
                Dictionary<int, string> ld = new Dictionary<int, string>();
                for (int i = 0; i < heads.Length; i++)
                {
                    if ((heads[i].Length == 2) || (heads[i].Length == 3))
                        ld.Add(i, heads[i]);

                }

                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();

                    if (line[0] == '#')
                        continue;


                    //if (n > 250)
                    //Console.WriteLine(line);

                    string[] words = line.Split(tabchar);

                    if (words.Length < 3)
                        continue;

                    n++;

                    //foreach (string s in words)
                    //    Console.WriteLine(s);

                    //Console.WriteLine(words[0] + "|" + words[1]);

                    langclass lc = new langclass();
                    lc.iso3 = words[0].Trim();
                    lc.iso2 = words[1].Trim();

                    for (int i = 2; i < words.Length; i++)
                    {
                        if (!String.IsNullOrEmpty(words[i].Trim()))
                        {
                            if (ld.ContainsKey(i))
                            {
                                if (!lc.name.ContainsKey(ld[i]))
                                    lc.name.Add(ld[i], words[i].Trim());

                            }
                        }
                    }

                    if (!String.IsNullOrEmpty(lc.iso3))
                        if (!langtoint.ContainsKey(lc.iso3))
                            langtoint.Add(lc.iso3, n);
                    if (!String.IsNullOrEmpty(lc.iso2))
                        if (!langtoint.ContainsKey(lc.iso2))
                            langtoint.Add(lc.iso2, n);
                    langdict.Add(n, lc);

                    if ((n % 100) == 0)
                    {
                        Console.WriteLine("n (language-iso)   = " + n.ToString());
                    }

                }

                Console.WriteLine("n    (language-iso)= " + n.ToString());




            }

            using (StreamReader sr = new StreamReader(geonamesfolder + "langnames-" + makelang + ".txt"))
            {
                n = 0;
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();

                    string[] words = line.Split(tabchar);

                    if (words.Length < 2)
                        continue;

                    string iso = words[0];
                    string langname = words[1];
                    if (makelang == "sv")
                        langname = langname.ToLower();
                    if (langtoint.ContainsKey(iso))
                    {
                        int langcode = langtoint[iso];
                        if ((langdict.ContainsKey(langcode)) && (!langdict[langcode].name.ContainsKey(makelang)))
                            langdict[langcode].name.Add(makelang, langname);
                    }

                    n++;
                    if ((n % 100) == 0)
                    {
                        Console.WriteLine("n (langname-makelang)   = " + n.ToString());

                    }


                }
                Console.WriteLine("n    (langname-makelang)= " + n.ToString());
            }

            if (savewikilinks)
            {
                Page pt = new Page(makesite, mp(13) + botname + "/languagelinks");
                pt.text = "Language links used by Lsjbot\n\n";
                foreach (int ilang in langdict.Keys)
                    if (langdict[ilang].name.ContainsKey(makelang))
                        pt.text += "* " + langdict[ilang].iso3 + " [[" + langdict[ilang].name[makelang] + "]]\n";
                    else
                        pt.text += "* " + langdict[ilang].iso3 + "\n";
                trysave(pt, 1,"Bot saving language links");
            }


        }


        public static string addref(string rn, string rstring)
        {
            if (String.IsNullOrEmpty(rn) || String.IsNullOrEmpty(rstring))
                return "";

            string refname = "\"" + rn + "\"";
            if (!refnamelist.Contains(refname))
            {
                refnamelist.Add(refname);

                string refref = "<ref name = " + refname + ">" + rstring + "</ref>";
                reflist += "\n" + refref;
            }
            string shortref = "<ref name = " + refname + "/>";
            return shortref;

        }

        public static string addnote(string notestring)
        {
            //if (makelang != "sv")
            //    return "";
            hasnotes = true;
            //Console.WriteLine("addnote:" + notestring);
            return mp(174) + notestring + "}}";

        }



        public static void get_lang_iw(string langcode)
        {
            Console.WriteLine("get lang iw " + langcode);

            using (StreamWriter sw = new StreamWriter("langnames-" + langcode + ".csv"))
            {

                foreach (int gnid in langdict.Keys)
                {

                    string langname = langdict[gnid].name["en"];
                    string[] names = langname.Split(';');
                    foreach (string ln in names)
                    {
                        Page ep = new Page(ensite, ln);
                        tryload(ep, 2);
                        if (!ep.Exists())
                            continue;
                        if (ep.IsRedirect())
                        {
                            ep.title = ep.RedirectsTo();
                            tryload(ep, 2);
                            if (!ep.Exists())
                                continue;
                        }
                        langname = ln;

                        List<string> iwlist = ep.GetInterLanguageLinks();
                        foreach (string iws in iwlist)
                        {
                            string[] ss = iws.Split(':');
                            string iwcode = ss[0];
                            string iwtitle = ss[1];
                            //Console.WriteLine("iw - " + iwcode + ":" + iwtitle);
                            if (iwcode == langcode)
                                langname = iwtitle;
                        }
                        sw.WriteLine(langdict[gnid].iso3 + ";" + langname);
                        Console.WriteLine(ln + ";" + langname);
                    }

                }
            }
        }

        public static string cleanup_text(string text)
        {
            Dictionary<string, string> cleanstring = new Dictionary<string, string>();
            cleanstring.Add(".  ", ". ");
            cleanstring.Add(",  ", ", ");
            cleanstring.Add("\n ", "\n");
            cleanstring.Add("\n\n\n", "\n\n");

            string rs = text;
            foreach (string cs in cleanstring.Keys)
            {
                while (rs.Contains(cs))
                    rs = rs.Replace(cs, cleanstring[cs]);
            }
            return rs;

        }

        public static string fill_geobox(int gnid)
        {
            string fc = gndict[gnid].featurecode;
            int icountry = gndict[gnid].adm[0];
            List<string> allnames = new List<string>();

            string boxtype = "alla";
            if (geoboxdict.ContainsKey(fc))
                boxtype = geoboxdict[fc];
            if (!geoboxtemplates.ContainsKey(boxtype))
            {
                Console.WriteLine("XXXXXXXXX Bad box type: " + boxtype);
                return "";
            }

            //creates dummy page, in order to use DNWB tools for template handling
            Page dummy = new Page(makesite, "dummy");
            dummy.text = geoboxtemplates[boxtype];

            //Console.WriteLine("Före:"+dummy.text.Substring(0,30));

            dummy.SetTemplateParameter("geobox", "name", gndict[gnid].Name_ml, true);
            //Console.WriteLine("1:" + dummy.text.Substring(0, 30));
            allnames.Add(gndict[gnid].Name_ml);

            int othernames = 0;
            if (gndict[gnid].Name != gndict[gnid].Name_ml)
            {
                dummy.SetTemplateParameter("geobox", "other_name", gndict[gnid].Name, true);
                allnames.Add(gndict[gnid].Name);
                othernames++;
            }

            if (altdict.ContainsKey(gnid))
            {
                int nativenames = 0;
                foreach (altnameclass ac in altdict[gnid])
                {
                    if (!allnames.Contains(ac.altname))
                    {
                        if (countrydict[icountry].languages.Contains(ac.ilang))
                        {
                            nativenames++;
                            if (ac.official)
                            {
                                dummy.SetTemplateParameter("geobox", "official_name", ac.altname, true);
                                allnames.Add(ac.altname);
                            }
                        }
                        if (ac.colloquial)
                        {
                            dummy.SetTemplateParameter("geobox", "nickname", ac.altname, true);
                            allnames.Add(ac.altname);
                        }
                    }
                }

                if (nativenames > 0)
                {
                    //bool nativeset = false;
                    foreach (altnameclass ac in altdict[gnid])
                    {
                        if (!allnames.Contains(ac.altname))
                        {

                            if (countrydict[icountry].languages.Contains(ac.ilang))
                            {
                                dummy.SetTemplateParameter("geobox", "native_name", ac.altname, true);
                                allnames.Add(ac.altname);
                                break; //set only once
                            }
                        }
                    }
                    foreach (altnameclass ac in altdict[gnid])
                    {

                        if (!allnames.Contains(ac.altname))

                        //if ( !countrydict[icountry].languages.Contains(ac.ilang))
                        {
                            string order = "";
                            if (othernames > 0)
                                order = othernames.ToString();
                            if ((!String.IsNullOrEmpty(ac.altname)) && (!allnames.Contains(ac.altname)))
                            {
                                dummy.SetTemplateParameter("geobox", "other_name" + order, ac.altname, true);
                                allnames.Add(ac.altname);
                                othernames++;
                            }
                        }
                    }
                }
                else
                {
                    foreach (altnameclass ac in altdict[gnid])
                    {
                        if (!allnames.Contains(ac.altname))
                        {

                            string order = "";
                            if (othernames > 0)
                                order = othernames.ToString();
                            if (!allnames.Contains(ac.altname))
                            {
                                dummy.SetTemplateParameter("geobox", "other_name" + order, ac.altname, true);
                                allnames.Add(ac.altname);
                                othernames++;
                            }
                        }
                    }

                }


            }


            //Console.WriteLine("2:" + dummy.text.Substring(0, 30));

            string latstring = gndict[gnid].latitude.ToString(culture_en);
            if (!latstring.Contains("."))
                latstring += ".0";
            string lonstring = gndict[gnid].longitude.ToString(culture_en);
            if (!lonstring.Contains("."))
                lonstring += ".0";
            dummy.SetTemplateParameter("geobox", "lat_d", latstring, true);
            dummy.SetTemplateParameter("geobox", "long_d", lonstring, true);

            string cat = initialcap(getfeaturelabel(countrydict[icountry].iso, gndict[gnid].featurecode, gnid));
            dummy.SetTemplateParameter("geobox", "category", cat, true);

            string countrynameml = countrydict[icountry].Name;
            if (countryml.ContainsKey(countrynameml))
                countrynameml = countryml[countrynameml];

            if (makelang == "sv")
            {

                dummy.SetTemplateParameter("geobox", "country", countrynameml, true);
                dummy.SetTemplateParameter("geobox", "country_flag", "true", true);
            }
            else
            {
                dummy.SetTemplateParameter("geobox", "country", "{{flag|" + countrynameml + "}}", true);
            }

            int nc = 0;

            int mamagnid = -1;
            if (motherdict.ContainsKey(makecountry))
            {
                string mama = motherdict[makecountry];
                mamagnid = countryid[mama];
                nc++;
                string acml = countrydict[mamagnid].Name;
                if (countryml.ContainsKey(acml))
                    acml = countryml[acml];
                if (makelang == "sv")
                {
                    dummy.SetTemplateParameter("geobox", "country" + nc.ToString(), acml, true);
                    dummy.SetTemplateParameter("geobox", "country_flag" + nc.ToString(), "true", true);

                }
                else
                {
                    dummy.SetTemplateParameter("geobox", "country" + nc.ToString(), "{{flag|" + acml + "}}", true);
                }
                dummy.SetTemplateParameter("geobox", "country_type", mp(294), true);
            }

            foreach (int ic in gndict[gnid].altcountry)
            {
                nc++;
                if (ic == mamagnid)
                    continue;
                string acml = countrydict[ic].Name;
                if (countryml.ContainsKey(acml))
                    acml = countryml[acml];
                if (makelang == "sv")
                {
                    dummy.SetTemplateParameter("geobox", "country" + nc.ToString(), acml, true);
                    dummy.SetTemplateParameter("geobox", "country_flag" + nc.ToString(), "true", true);
                }
                else
                {
                    dummy.SetTemplateParameter("geobox", "country" + nc.ToString(), "{{flag|" + acml + "}}", true);
                }

            }

            if (!string.IsNullOrEmpty(getgnidname(gndict[gnid].adm[1])))
            {
                dummy.SetTemplateParameter("geobox", "state", makegnidlink(gndict[gnid].adm[1]), true);
                dummy.SetTemplateParameter("geobox", "state_type", initialcap(removearticle(getadmlabel(makecountry, 1, gndict[gnid].adm[1]))), true);
            }
            if (!string.IsNullOrEmpty(getgnidname(gndict[gnid].adm[2])))
            {
                dummy.SetTemplateParameter("geobox", "region", makegnidlink(gndict[gnid].adm[2]), true);
                dummy.SetTemplateParameter("geobox", "region_type", initialcap(getadmlabel(makecountry, 2, gndict[gnid].adm[2])), true);
            }
            if (!string.IsNullOrEmpty(getgnidname(gndict[gnid].adm[3])))
            {
                dummy.SetTemplateParameter("geobox", "district", makegnidlink(gndict[gnid].adm[3]), true);
                dummy.SetTemplateParameter("geobox", "district_type", initialcap(getadmlabel(makecountry, 3, gndict[gnid].adm[3])), true);
            }
            if (!string.IsNullOrEmpty(getgnidname(gndict[gnid].adm[4])))
            {
                dummy.SetTemplateParameter("geobox", "municipality", makegnidlink(gndict[gnid].adm[4]), true);
                dummy.SetTemplateParameter("geobox", "municipality_type", initialcap(getadmlabel(makecountry, 4, gndict[gnid].adm[4])), true);
            }

            int elev = gndict[gnid].elevation;
            if (elev < 0)
                elev = gndict[gnid].dem;
            string category = "";
            if (categorydict.ContainsKey(fc))
                category = categorydict[fc];
            if ((category == "oceans") || (category == "seabed") || (category == "reefs") || (category == "bays") || (category == "navigation"))
                elev = -9999;


            if (elev > 0)
            {
                dummy.SetTemplateParameter("geobox", "elevation", elev.ToString("N0", nfi_en), true);
                if (is_height(gndict[gnid].featurecode))
                {
                    double width = 0;
                    int prom = get_prominence(gnid, out width);

                    if (prom <= 0)
                    {
                        //int nearhigh = -1;
                        int altitude = -1;
                        double nbradius = 3.0;
                        List<int> farlist = getneighbors(gnid, nbradius);
                        bool otherpeak = false;
                        Console.WriteLine("farlist = " + farlist.Count.ToString());
                        foreach (int nbgnid in farlist)
                            if (nbgnid != gnid)
                                if (is_height(gndict[nbgnid].featurecode) && (gndict[nbgnid].elevation > gndict[gnid].elevation))
                                {
                                    otherpeak = true;
                                    Console.WriteLine(gndict[nbgnid].Name);
                                }

                        if (!otherpeak)
                        {
                            Console.WriteLine("No other peak");
                            nbradius = 2.0;
                            double slat = 9999.9;
                            double slon = 9999.9;
                            altitude = get_summit(gnid, out slat, out slon);

                            Console.WriteLine("get summit " + slat.ToString() + " " + slon.ToString() + ": " + altitude.ToString());

                            double nhdist = get_distance_latlong(gndict[gnid].latitude, gndict[gnid].longitude, slat, slon);
                            Console.WriteLine("nhdist = " + nhdist.ToString());
                            if ((nhdist < nbradius) && (nhdist > 0.1) && (altitude > elev))
                            {
                                gndict[gnid].latitude = slat;
                                gndict[gnid].longitude = slon;
                                gndict[gnid].elevation = altitude;
                                gndict[gnid].elevation_vp = altitude;
                                elev = altitude;

                                latstring = gndict[gnid].latitude.ToString("F4", culture_en);
                                if (!latstring.Contains("."))
                                    latstring += ".0";
                                lonstring = gndict[gnid].longitude.ToString("F4", culture_en);
                                if (!lonstring.Contains("."))
                                    lonstring += ".0";
                                dummy.SetTemplateParameter("geobox", "elevation", elev.ToString("N0", nfi_en), true);
                                dummy.SetTemplateParameter("geobox", "lat_d", latstring, true);
                                dummy.SetTemplateParameter("geobox", "long_d", lonstring, true);
                                dummy.SetTemplateParameter("geobox", "coordinates_note", addnote(mp(213) + addref("vp", viewfinder_ref()) + " " + mp(200)), true);

                                prom = get_prominence(gnid, out width);
                            }
                        }

                    }

                    if (prom > minimum_prominence)
                    {
                        dummy.SetTemplateParameter("geobox", "height", prom.ToString("N0", nfi_en), true);
                        dummy.SetTemplateParameter("geobox", "width", fnum(width), true);
                        dummy.SetTemplateParameter("geobox", "width_unit", "km", true);
                        dummy.SetTemplateParameter("geobox", "highest_elevation", elev.ToString("N0", nfi_en), true);
                        dummy.SetTemplateParameter("geobox", "elevation", (elev - prom).ToString("N0", nfi_en), true);
                        gndict[gnid].prominence = prom;
                        gndict[gnid].width = width;
                        if (gndict.ContainsKey(gndict[gnid].inrange))
                            dummy.SetTemplateParameter("geobox", "range", makegnidlink(gndict[gnid].inrange), true);
                    }
                }
            }

            bool haspop = false;

            if (prefergeonamespop)
            {
                if ((makecountry == "CN") && (chinese_pop_dict2.ContainsKey(gnid)))
                {
                    dummy.SetTemplateParameter("geobox", "population", chinese_pop_dict2[gnid].pop.ToString(), true);
                    haspop = true;
                    dummy.SetTemplateParameter("geobox", "population_note", chinapopref(), true);
                    dummy.SetTemplateParameter("geobox", "population_date", "2010", true);

                }
                else if (gndict[gnid].population > minimum_population)
                {
                    dummy.SetTemplateParameter("geobox", "population", gndict[gnid].population.ToString(), true);
                    haspop = true;
                    if (gndict[gnid].population == gndict[gnid].population_wd)
                        dummy.SetTemplateParameter("geobox", "population_note", "<sup>" + mp(131) + " " + gndict[gnid].population_wd_iw + "wiki</sup>", true);
                    else
                    {
                        dummy.SetTemplateParameter("geobox", "population_note", geonameref(gnid), true);
                        dummy.SetTemplateParameter("geobox", "population_date", gndict[gnid].moddate, true);
                    }

                }
                else if ((wdid > 0) && (!String.IsNullOrEmpty(get_wd_prop(propdict["population"], currentxml))))
                {
                    dummy.SetTemplateParameter("geobox", "population", wdlink("population"), true);
                    haspop = true;
                    dummy.SetTemplateParameter("geobox", "population_note", "<sup>" + mp(131) + " Wikidata</sup>", true);
                    long wdpop = tryconvert(get_wd_prop(propdict["population"], currentxml));
                    if (wdpop > 0)
                    {
                        gndict[gnid].population_wd = wdpop;
                        if ((gndict[gnid].population < minimum_population) || (!prefergeonamespop))
                            gndict[gnid].population = wdpop;
                        gndict[gnid].population_wd_iw = "Wikidata";
                    }
                }
                else if (gndict[gnid].population_wd > minimum_population)
                {
                    dummy.SetTemplateParameter("geobox", "population", gndict[gnid].population_wd.ToString(), true);
                    haspop = true;
                    //dummy.SetTemplateParameter("geobox", "population_date", gndict[gnid].moddate, true);
                    dummy.SetTemplateParameter("geobox", "population_note", "<sup>" + mp(131) + " " + gndict[gnid].population_wd_iw + "wiki</sup>", true);
                }
            }
            else
            {
                if ((wdid > 0) && (!String.IsNullOrEmpty(get_wd_prop(propdict["population"], currentxml))))
                {
                    dummy.SetTemplateParameter("geobox", "population", wdlink("population"), true);
                    haspop = true;
                    dummy.SetTemplateParameter("geobox", "population_note", "<sup>" + mp(131) + " Wikidata</sup>", true);
                    long wdpop = tryconvert(get_wd_prop(propdict["population"], currentxml));
                    if (wdpop > 0)
                    {
                        gndict[gnid].population_wd = wdpop;
                        if ((gndict[gnid].population < minimum_population) || (!prefergeonamespop))
                            gndict[gnid].population = wdpop;
                        gndict[gnid].population_wd_iw = "Wikidata";
                    }
                }
                else if (gndict[gnid].population_wd > minimum_population)
                {
                    dummy.SetTemplateParameter("geobox", "population", gndict[gnid].population_wd.ToString(), true);
                    haspop = true;
                    //dummy.SetTemplateParameter("geobox", "population_date", gndict[gnid].moddate, true);
                    dummy.SetTemplateParameter("geobox", "population_note", "<sup>" + mp(131) + " " + gndict[gnid].population_wd_iw + "wiki</sup>", true);
                }
                else if (gndict[gnid].population > minimum_population)
                {
                    dummy.SetTemplateParameter("geobox", "population", gndict[gnid].population.ToString(), true);
                    haspop = true;
                    dummy.SetTemplateParameter("geobox", "population_date", gndict[gnid].moddate, true);
                    dummy.SetTemplateParameter("geobox", "population_note", geonameref(gnid), true);
                }
            }

            if (gndict[gnid].area > minimum_area)
            {
                dummy.SetTemplateParameter("geobox", "area", gndict[gnid].area.ToString("N2", nfi_en), true);
                if (haspop)
                    dummy.SetTemplateParameter("geobox", "population_density", "auto", true);
            }

            if (tzdict.ContainsKey(gndict[gnid].tz))
            {
                dummy.SetTemplateParameter("geobox", "timezone_label", gndict[gnid].tz, true);
                dummy.SetTemplateParameter("geobox", "utc_offset", tzdict[gndict[gnid].tz].offset, true);
                if (tzdict[gndict[gnid].tz].summeroffset != tzdict[gndict[gnid].tz].offset)
                    dummy.SetTemplateParameter("geobox", "utc_offset_DST", tzdict[gndict[gnid].tz].summeroffset, true);
                if (!String.IsNullOrEmpty(tzdict[gndict[gnid].tz].tzname))
                {
                    dummy.SetTemplateParameter("geobox", "timezone", "[[" + tzdict[gndict[gnid].tz].tzfull + "|" + tzdict[gndict[gnid].tz].tzname + "]]", true);
                    if (tzdict[gndict[gnid].tz].summeroffset != tzdict[gndict[gnid].tz].offset)
                        dummy.SetTemplateParameter("geobox", "timezone_DST", "[[" + tzdict[gndict[gnid].tz].tzfullsummer + "|" + tzdict[gndict[gnid].tz].tzsummer + "]]", true);
                }
            }

            if (wdid > 0) //get various stuff from Wikidata:
            {
                Console.WriteLine("Filling geobox from wikidata");
                if (!String.IsNullOrEmpty(get_wd_prop(propdict["coat of arms"], currentxml)))
                {
                    string imagename = get_wd_prop(propdict["coat of arms"], currentxml);
                    if (exists_at_commons(imagename))
                        dummy.SetTemplateParameter("geobox", "symbol", imagename, true);
                }
                if (!String.IsNullOrEmpty(get_wd_prop(propdict["flag"], currentxml)))
                    dummy.SetTemplateParameter("geobox", "flag", get_wd_prop(propdict["flag"], currentxml), true);
                //if (!String.IsNullOrEmpty(get_wd_prop(propdict("capital", currentxml))))
                //    dummy.SetTemplateParameter("geobox", "capital", wdlink("capital"));
                if (!String.IsNullOrEmpty(get_wd_prop(propdict["locatormap"], currentxml)))
                    dummy.SetTemplateParameter("geobox", "map2", get_wd_prop(propdict["locatormap"], currentxml), true);
                if (!String.IsNullOrEmpty(get_wd_prop(propdict["iso"], currentxml)))
                    dummy.SetTemplateParameter("geobox", "iso_code", wdlink("iso"), true);
                //if (!String.IsNullOrEmpty(get_wd_prop(propdict("head of government", currentxml))))
                //    dummy.SetTemplateParameter("geobox", "leader", wdlink("head of government"));
                if (!String.IsNullOrEmpty(get_wd_prop(propdict["postal code"], currentxml)))
                    dummy.SetTemplateParameter("geobox", "postal_code", wdlink("postal code"), true);
                if (!String.IsNullOrEmpty(get_wd_prop(propdict["image"], currentxml)))
                {
                    string imagename = get_wd_prop(propdict["image"], currentxml);
                    if (exists_at_commons(imagename))
                        dummy.SetTemplateParameter("geobox", "image", imagename, true);
                }
                else if (!String.IsNullOrEmpty(get_wd_prop(propdict["banner"], currentxml)))
                {
                    string imagename = get_wd_prop(propdict["banner"], currentxml);
                    if (exists_at_commons(imagename))
                        dummy.SetTemplateParameter("geobox", "image", imagename, true);
                }
                foreach (int ic in get_wd_prop_idlist(propdict["capital"], currentxml))
                    dummy.SetTemplateParameter("geobox", "capital", get_wd_name(ic), true);
                foreach (int ic in get_wd_prop_idlist(propdict["head of government"], currentxml))
                    dummy.SetTemplateParameter("geobox", "leader", get_wd_name(ic), true);

                if (String.IsNullOrEmpty(get_wd_prop(propdict["gnid"], currentxml))) //if gnid NOT in wikidata, set it manually
                    dummy.SetTemplateParameter("geobox", "geonames", gnid.ToString(), true);

            }
            else //wdid missing, set geonames ID in template
            {
                dummy.SetTemplateParameter("geobox", "geonames", gnid.ToString(), true);
            }

            if (gndict[gnid].inrange > 0)
            {
                dummy.SetTemplateParameter("geobox", "range", makegnidlink(gndict[gnid].inrange), true);
            }
            else if (rangedict.ContainsKey(gnid))
            {
                //public class rangeclass //data for each MTS/HLLS
                //{
                //    public double length = 0;
                //    public string orientation = "....";
                //    public double angle = 0; //polar angle of long axis (radians). 0 or pi = EW, pi/2 or 3pi/2 = NS etc.
                //    public double kmew = 0;
                //    public double kmns = 0;
                //    public int maxheight = 0; //highest point; gnid of peak if negative, height if positive
                //    public double hlat = 999; //latitude/longitude of highest point
                //    public double hlon = 999;
                //    public List<int> inrange = new List<int>(); //list of GeoNames id of mountains in the range.
                //}

                dummy.SetTemplateParameter("geobox", "length", rangedict[gnid].length.ToString("N0", nfi_en), true);
                dummy.SetTemplateParameter("geobox", "length_orientation", get_nsew(rangedict[gnid].angle), true);
                int hmax = rangedict[gnid].maxheight;
                int hgnid = -1;
                if (hmax < 0)
                    if (gndict.ContainsKey(-hmax))
                    {
                        hgnid = -hmax;
                        hmax = gndict[hgnid].elevation;
                    }
                if (hmax > 0)
                {
                    string hlatstring = rangedict[gnid].hlat.ToString(culture_en);
                    if (!hlatstring.Contains("."))
                        hlatstring += ".0";
                    string hlonstring = rangedict[gnid].hlon.ToString(culture_en);
                    if (!hlonstring.Contains("."))
                        hlonstring += ".0";
                    if (hgnid > 0)
                        dummy.SetTemplateParameter("geobox", "highest_location", makegnidlink(hgnid), true);
                    dummy.SetTemplateParameter("geobox", "highest_elevation", hmax.ToString("N0", nfi_en), true);
                    dummy.SetTemplateParameter("geobox", "highest_lat_d", hlatstring, true);
                    dummy.SetTemplateParameter("geobox", "highest_long_d", hlonstring, true);
                }
            }

            //Airport codes:
            if (makelang == "sv")
            {
                if (iatadict.ContainsKey(gnid))
                    dummy.SetTemplateParameter("geobox", "IATA-kod", iatadict[gnid], true);
                if (icaodict.ContainsKey(gnid))
                    dummy.SetTemplateParameter("geobox", "ICAO-kod", icaodict[gnid], true);
            }
            else
            {
                if (iatadict.ContainsKey(gnid))
                {
                    dummy.SetTemplateParameter("geobox", "free", iatadict[gnid], true);
                    dummy.SetTemplateParameter("geobox", "free_type", "[[IATA]]", true);
                }
                if (icaodict.ContainsKey(gnid))
                {
                    dummy.SetTemplateParameter("geobox", "free1", icaodict[gnid], true);
                    dummy.SetTemplateParameter("geobox", "free1_type", "[[ICAO]]", true);
                }
            }


            //Console.WriteLine("3:" + dummy.text.Substring(0, 30));

            if (locatoringeobox) //only works in Swedish!
            {
                string countryname = countrydict[icountry].Name;

                //if (String.IsNullOrEmpty(locatordict[countryname].locatorimage))
                //{
                //string templatename = mp(63,null)+mp(72).Replace("{{", "") + " " + locatordict[countryname];
                //Console.WriteLine(templatename);
                //string imagename = "";
                //Page ltp = new Page(makesite, templatename);
                //tryload(ltp, 2);
                //if (ltp.Exists())
                //{
                //    imagename = get_pictureparam(ltp);
                //}

                //if (!String.IsNullOrEmpty(imagename))
                //{
                //    locatordict[countryname].locatorimage = imagename;
                //}
                //}

                if (locatordict.ContainsKey(countryname))
                {
                    string templatestart = mp(72) + " " + locatordict[countryname].get_locator(gndict[gnid].latitude, gndict[gnid].longitude);
                    string imagename = "{{xxx|reliefkarta_om_den_finns}}".Replace("{{xxx", templatestart).Replace("bild", mp(171));
                    //string imagename = "{{#if:xxx|bild1}}|xxx|bild1}}|xxx|bild}}}}".Replace("xxx", templatestart).Replace("bild", mp(171));
                    string[] p143 = new string[1] { countrynameml };
                    dummy.SetTemplateParameter("geobox", "map", imagename, true);
                    dummy.SetTemplateParameter("geobox", "map_locator", locatordict[countryname].get_locator(gndict[gnid].latitude, gndict[gnid].longitude), true);
                    dummy.SetTemplateParameter("geobox", "map_caption", mp(143, p143), true);
                }
            }

            //Console.WriteLine("Efter:" + dummy.text.Substring(0, 30));


            return dummy.text;

        }

        public static string get_pictureparam(Page ltp)
        {
            string imagename = "";
            string[] param = ltp.text.Split('|');
            foreach (string par in param)
            {
                if (((par.Trim().ToLower().IndexOf("bild") == 0) || (par.Trim().ToLower().IndexOf("image") == 0)) && (par.Contains("=")))
                {
                    imagename = par.Split('=')[1].Trim();
                    if (imagename.Contains("}}"))
                        imagename = imagename.Remove(imagename.IndexOf("}}")).Trim();
                    //Console.WriteLine("imagename = " + imagename);
                    break;
                }
            }
            return imagename;
        }

        public static double get_edgeparam(Page ltp, string edgepar)
        {
            string imagename = "";
            string[] param = ltp.text.Split('|');
            foreach (string par in param)
            {
                if ((par.Trim().ToLower().IndexOf(edgepar) == 0) && (par.Contains("=")))
                {
                    imagename = par.Split('=')[1].Trim();
                    if (imagename.Contains("}}"))
                        imagename = imagename.Remove(imagename.IndexOf("}}")).Trim();
                    //Console.WriteLine("imagename = " + imagename);
                    break;
                }
            }
            return tryconvertdouble(imagename);
        }

        public static bool exists_at_commons(string imagename)
        {
            string res = cmsite.indexPath + "?title=" +
                        HttpUtility.UrlEncode("File:" + imagename);
            //Console.WriteLine("commonsres = " + res);
            string src = "";
            try
            {
                src = cmsite.GetWebPage(res); // cmsite.GetPageHTM(res);
                Console.WriteLine("Found at Commons: " + imagename);
                return true;
            }
            catch (WebException e)
            {
                //newpix[newpic] = "/// NOT FOUND ON COMMONS";
                string message = e.Message;
                if (message.Contains(": (404) "))
                {		// Not Found
                    Console.Error.WriteLine(Bot.Msg("Page \"{0}\" doesn't exist."), imagename);
                    Console.WriteLine("Image not found: " + imagename);
                    //continue;
                }
                else
                {
                    Console.Error.WriteLine(message);
                    //continue;
                }
                return false;
            }

        }

        public static void fix_positionmaps()
        {
            //https://tools.wmflabs.org/magnus-toolserver/commonsapi.php?image=Bhutan_location_map.svg

            Dictionary<string, string> replacedict1 = new Dictionary<string, string>();
            Dictionary<string, string> replacedict2 = new Dictionary<string, string>();
            replacedict1.Add("|topp", "| topp");
            replacedict1.Add(" topp=", " topp =");
            replacedict1.Add("|botten", "| botten");
            replacedict1.Add(" botten=", " botten =");
            replacedict1.Add("|vänster", "| vänster");
            replacedict1.Add(" vänster=", " vänster =");
            replacedict1.Add("|höger", "| höger");
            replacedict1.Add(" höger=", " höger =");
            replacedict1.Add("|bild", "| bild");
            replacedict1.Add(" bild=", " bild =");

            replacedict2.Add("| topp ", "| topp|top ");
            replacedict2.Add("| botten ", "| botten|bottom ");
            replacedict2.Add("| vänster ", "| vänster|left ");
            replacedict2.Add("| höger ", "| höger|right ");


            foreach (string countryname in locatordict.Keys)
            {
                Console.WriteLine("countryname = " + countryname);
                string templatename = mp(63) + mp(72).Replace("{{", "") + " " + locatordict[countryname].locatorname;
                Console.WriteLine(templatename);
                string imagename = "";
                Page ltp = new Page(makesite, templatename);
                tryload(ltp, 2);
                if (ltp.Exists())
                {
                    if (ltp.text.Contains("topp|top"))
                        continue;
                    imagename = get_pictureparam(ltp);
                    if (!String.IsNullOrEmpty(imagename))
                    {

                        foreach (KeyValuePair<string, string> replacepair in replacedict1)
                        {
                            ltp.text = ltp.text.Replace(replacepair.Key, replacepair.Value);
                        }
                        foreach (KeyValuePair<string, string> replacepair in replacedict2)
                        {
                            ltp.text = ltp.text.Replace(replacepair.Key, replacepair.Value);
                        }

                        XmlDocument xd = new XmlDocument();
                        string cmurl = "https://tools.wmflabs.org/magnus-toolserver/commonsapi.php?image=" + imagename;
                        string s = get_webpage(cmurl);
                        if (!String.IsNullOrEmpty(s))
                        {
                            //Console.WriteLine(s);
                            xd.LoadXml(s);
                            XmlNodeList elemlist1 = xd.GetElementsByTagName("width");
                            double width = -1;
                            foreach (XmlNode ee in elemlist1)
                                width = tryconvertdouble(ee.InnerXml);
                            XmlNodeList elemlist2 = xd.GetElementsByTagName("height");
                            double height = -1;
                            foreach (XmlNode ee in elemlist2)
                                height = tryconvertdouble(ee.InnerXml);
                            Console.WriteLine("w,h = " + width.ToString() + ", " + height.ToString());
                            double ratio = height / width;
                            if (!ltp.text.Contains("ratio"))
                                ltp.text = ltp.text.Replace("| bild ", "| ratio = " + ratio.ToString(culture_en) + "\n| bild ");

                        }

                    }
                    trysave(ltp, 2,mp(60,null)+" "+mp(63,null));
                    string redirectname = mp(63) + "Geobox locator " + locatordict[countryname].locatorname;
                    make_redirect(redirectname, templatename, "Geobox locator|" + locatordict[countryname].locatorname, -1);

                    //Mall:Geobox locator Andorra
                    //#OMDIRIGERING[[Mall:Kartposition Andorra]]
                    //[[Kategori:Geobox locator|Andorra]]


                    //Console.WriteLine("<ret>");
                    //Console.ReadLine();
                }

            }
            Console.WriteLine("Done!");
            Console.ReadLine();

        }

        public static void get_page_area_pop_height(Page p, out double areaout, out long popout, out int heightout)
        {
            areaout = -1;
            popout = -1;
            heightout = -1;

            List<string> popparams = new List<string>();
            List<string> urbanparams = new List<string>();
            List<string> areaparams = new List<string>();
            List<string> heightparams = new List<string>();

            popparams.Add("population");
            popparams.Add("population_total");
            popparams.Add("population_urban");
            popparams.Add("population_metro");
            popparams.Add("befolkning");

            urbanparams.Add("population_urban");
            urbanparams.Add("population_metro");
            urbanparams.Add("población_urb");

            areaparams.Add("area_total_km2");
            areaparams.Add("area");
            areaparams.Add("fläche");
            areaparams.Add("superficie");
            areaparams.Add("yta");

            heightparams.Add("highest_elevation");
            heightparams.Add("elevation");

            bool foundpop = false;
            bool foundurban = false;
            bool foundarea = false;
            //bool foundheight = false;
            bool foundhighest = false;
            bool preferurban = true;

            long popwdurban = -1;

            foreach (string ttt in p.GetTemplates(true, true))
            {
                //Console.WriteLine(ttt);
                Dictionary<string, string> pdict = Page.ParseTemplate(ttt);
                foreach (string param in pdict.Keys)
                {
                    if (popparams.Contains(param) && !foundpop)
                    {
                        long popwd = tryconvertlong(pdict[param]);
                        if (popwd <= 0)
                        {
                            popwd = tryconvertlong(pdict[param].Split()[0]);
                        }
                        if (popwd > 0)
                        {
                            foundpop = true;
                            popout = popwd;
                        }
                    }
                    if (urbanparams.Contains(param) && !foundurban)
                    {
                        long popwd = tryconvertlong(pdict[param]);
                        if (popwd > 0)
                        {
                            foundurban = true;
                            popwdurban = popwd;
                        }
                    }
                    if (areaparams.Contains(param) && !foundarea)
                    {
                        double areawd = tryconvertdouble(pdict[param]);
                        if (areawd <= 0)
                        {
                            areawd = tryconvertdouble(pdict[param].Split()[0]);
                        }
                        Console.WriteLine("areaparam = " + pdict[param]);
                        Console.WriteLine("areawd = " + areawd.ToString());
                        if (areawd > 0)
                        {
                            foundarea = true;
                            areaout = areawd;
                        }
                    }

                    if (heightparams.Contains(param) && !foundhighest)
                    {
                        int heightwd = tryconvert(pdict[param]);
                        if (heightwd <= 0)
                        {
                            heightwd = tryconvert(pdict[param].Split()[0]);
                        }
                        Console.WriteLine("heightparam = " + param);
                        Console.WriteLine("heightwd = " + heightwd.ToString());
                        if (heightwd > 0)
                        {
                            //foundheight = true;
                            heightout = heightwd;
                            if (param.Contains("highest"))
                                foundhighest = true;
                        }
                    }


                }

            }

            if (preferurban && foundurban)
            {
                popout = popwdurban;
            }
        }

        public static void get_wd_area_pop(int wdid, XmlDocument cx, out double areawdout, out long popwdout, out string iwsout, bool preferurban)
        {
            areawdout = -1.0;
            popwdout = -1;
            iwsout = "";
            long popwdurban = -1;
            string iwsurban = "";

            List<string> popparams = new List<string>();
            List<string> urbanparams = new List<string>();
            List<string> areaparams = new List<string>();
            List<string> paramlangs = new List<string>();

            popparams.Add("population");
            popparams.Add("population_total");
            popparams.Add("population_urban");
            popparams.Add("population_metro");
            popparams.Add("poblacion");
            popparams.Add("población");
            popparams.Add("población_urb");
            popparams.Add("einwohner");

            urbanparams.Add("population_urban");
            urbanparams.Add("population_metro");
            urbanparams.Add("población_urb");

            areaparams.Add("area_total_km2");
            areaparams.Add("area");
            areaparams.Add("fläche");
            areaparams.Add("superficie");

            paramlangs.Add("en");
            paramlangs.Add("de");
            paramlangs.Add("fr");
            paramlangs.Add("es");
            if (!paramlangs.Contains(countrydict[countryid[makecountry]].nativewiki) && !String.IsNullOrEmpty(countrydict[countryid[makecountry]].nativewiki))
                paramlangs.Add(countrydict[countryid[makecountry]].nativewiki);

            string badlang = "";
            foreach (string iwl in paramlangs)
            {
                if ((!iwsites.ContainsKey(iwl)) && (iwl != makelang))
                {
                    Console.WriteLine(iwl);
                    try
                    {
                        Site ssite = new Site("https://" + iwl + ".wikipedia.org", botname, password);
                        iwsites.Add(iwl, ssite);
                    }
                    catch (WebException e)
                    {
                        string message = e.Message;
                        Console.Error.WriteLine(message);
                        badlang = iwl;
                    }
                    catch (WikiBotException e)
                    {
                        string message = e.Message;
                        Console.Error.WriteLine(message);
                        badlang = iwl;
                    }
                }
            }
            if (!String.IsNullOrEmpty(badlang))
                paramlangs.Remove(badlang);

            if (cx == null)
                cx = get_wd_xml(wdid);
            if (cx == null)
            {
                Console.WriteLine("cx = null");
                return;
            }
            Dictionary<string, string> iwdict = get_wd_sitelinks(cx);

            bool foundpop = false;
            bool foundurban = false;
            bool foundarea = false;

            foreach (string iws in iwdict.Keys)
            {
                string iwss = iws.Replace("wiki", "");
                Console.WriteLine(iwss + ":" + iwdict[iws]);
                if ((paramlangs.Contains(iwss)) && (iwsites.ContainsKey(iwss)))
                {
                    Console.WriteLine(iws + ":" + iwdict[iws] + " Paramlang!");
                    Page iwpage = new Page(iwsites[iwss], iwdict[iws]);
                    if (tryload(iwpage, 1))
                        if (iwpage.Exists())
                        {
                            foreach (string ttt in iwpage.GetTemplates(true, true))
                            {
                                //Console.WriteLine(ttt);
                                Dictionary<string, string> pdict = Page.ParseTemplate(ttt);// iwsites[iwss].ParseTemplate(ttt);
                                foreach (string param in pdict.Keys)
                                {
                                    if (popparams.Contains(param) && !foundpop)
                                    {
                                        long popwd = tryconvertlong(pdict[param]);
                                        if (popwd > 0)
                                        {
                                            foundpop = true;
                                            popwdout = popwd;
                                            iwsout = iwss;
                                        }
                                    }
                                    if (urbanparams.Contains(param) && !foundurban)
                                    {
                                        long popwd = tryconvertlong(pdict[param]);
                                        if (popwd > 0)
                                        {
                                            foundurban = true;
                                            popwdurban = popwd;
                                            iwsurban = iwss;
                                        }
                                    }
                                    if (areaparams.Contains(param) && !foundarea)
                                    {
                                        double areawd = tryconvertdouble(pdict[param]);
                                        if (areawd <= 0)
                                        {
                                            areawd = tryconvertdouble(pdict[param].Split()[0]);
                                        }
                                        Console.WriteLine("areaparam = " + pdict[param]);
                                        Console.WriteLine("areawd = " + areawd.ToString());
                                        if (areawd > 0)
                                        {
                                            foundarea = true;
                                            areawdout = areawd;
                                        }
                                    }
                                }

                            }
                            if (foundarea && foundpop && (foundurban || !preferurban))
                                break;

                        }
                }

            }

            if (preferurban && foundurban)
            {
                popwdout = popwdurban;
                iwsout = iwsurban;
            }

        }


        public static void check_wikidata()
        {
            int nwd = 0;
            int npop = 0;
            int narea = 0;


            using (StreamWriter sw = new StreamWriter("wikidata-" + makecountry + ".txt"))
            {

                int ngnid = gndict.Count;

                foreach (int gnid in gndict.Keys)
                {
                    Console.WriteLine("=====" + makecountry + "======== " + ngnid.ToString() + " remaining. ===========");
                    ngnid--;
                    if ((ngnid % 1000) == 0)
                    {
                        Console.WriteLine("Garbage collection:");
                        GC.Collect();
                    }

                    //wdid = get_wd_item(gnid);
                    if (gndict[gnid].wdid <= 0)
                        continue;
                    else
                        wdid = gndict[gnid].wdid;

                    Console.WriteLine(gndict[gnid].Name + ": " + wdid.ToString());
                    if (wdid > 0)
                    {
                        nwd++;

                        double areawd = -1.0;
                        long popwd = -1;
                        string iwss = "";

                        bool preferurban = (gndict[gnid].featureclass == 'P');
                        get_wd_area_pop(wdid, null, out areawd, out popwd, out iwss, preferurban);

                        if (popwd > 0)
                        {
                            Console.WriteLine("popwd = " + popwd.ToString());
                            gndict[gnid].population_wd = popwd;
                            gndict[gnid].population_wd_iw = iwss;
                            npop++;
                        }

                        if (areawd > 0)
                        {
                            gndict[gnid].area = areawd;
                            narea++;
                        }


                        //Console.WriteLine("<ret>");
                        //Console.ReadLine();
                        sw.WriteLine(gnid.ToString() + tabstring + wdid.ToString() + tabstring + gndict[gnid].area.ToString() + tabstring + gndict[gnid].population_wd.ToString() + tabstring + gndict[gnid].population_wd_iw);
                    }
                }
                Console.WriteLine("nwd = " + nwd.ToString());
                Console.WriteLine("npop = " + npop.ToString());
                Console.WriteLine("narea = " + narea.ToString());
                Console.WriteLine("gndict = " + gndict.Count.ToString());
                nwdhist.PrintSHist();

            }
        }

        public static void read_good_wd_file()
        {
            Console.WriteLine("read_good_wd_file");
            int nwdtot = 0;
            if (!File.Exists("wikidata-good.nt"))
                return;

            using (StreamReader sr = new StreamReader(geonamesfolder + "wikidata-good.nt"))
            {
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();
                    string[] words = line.Split(tabchar);

                    if (words.Length < 2)
                        continue;

                    nwdtot++;

                    int gnid = tryconvert(words[0]);
                    if (!gndict.ContainsKey(gnid))
                        continue;

                    int wdid = tryconvert(words[1]);
                    if (wdid <= 0)
                        continue;

                    gndict[gnid].wdid = wdid;

                    if (!wdgniddict.ContainsKey(wdid))
                        wdgniddict.Add(wdid, gnid);
                    else if (wdgniddict[wdid] != gnid)
                    {// negative numbers count how many duplicates
                        if (wdgniddict[wdid] > 0)
                            wdgniddict[wdid] = -2;
                        else
                            wdgniddict[wdid]--;
                    }
                }
                if ((nwdtot % 10000) == 0)
                    Console.WriteLine("nwdtot = " + nwdtot.ToString());
            }

            Console.WriteLine("nwdtot = " + nwdtot.ToString());

        }



        public static Dictionary<int, int> read_wd_dict(string wdcountry)
        {
            Dictionary<int, int> rdict = new Dictionary<int, int>();
            string filename = geonamesfolder + "wikidata\\wikidata-" + wdcountry + ".txt";
            string filename_override = geonamesfolder + "wikidata\\wikidata-" + wdcountry + "-override.txt";

            if (!File.Exists(filename))
            {
                Console.WriteLine("No file " + filename);
                return rdict;
            }

            Dictionary<int, int> overridedict = new Dictionary<int, int>();
            if (File.Exists(filename_override)) //use to override wd assignments in case of systematic errors in main wd run
            {
                int nover = 0;
                using (StreamReader sr = new StreamReader(filename_override))
                {
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        string[] words = line.Split(tabchar);
                        if (words.Length < 2)
                            continue;
                        int gnid0 = tryconvert(words[0]);
                        int gnid1 = tryconvert(words[1]);
                        if ((gnid0 <= 0) || (gnid1 <= 0))
                            continue;

                        if (overridedict.ContainsKey(gnid0))
                            continue;

                        overridedict.Add(gnid0, gnid1);
                        nover++;
                    }
                }
                Console.WriteLine("noverride = " + nover.ToString());
            }

            try
            {
                List<int> withwd = new List<int>();

                //first pass, in order to doublecheck overridedict
                if (overridedict.Count > 0)
                {
                    Console.WriteLine("First pass...");
                    using (StreamReader sr = new StreamReader(filename))
                    {
                        while (!sr.EndOfStream)
                        {
                            String line = sr.ReadLine();
                            string[] words = line.Split(tabchar);
                            //sw.WriteLine(gnid.ToString() + tabstring + wdid.ToString() + tabstring + gndict[gnid].area.ToString() + tabstring + gndict[gnid].population_wd.ToString() + tabstring + gndict[gnid].population_wd_iw);

                            if (words.Length < 4)
                                continue;

                            int gnid = tryconvert(words[0]);
                            withwd.Add(gnid);
                        }
                    }
                    foreach (int gnid in withwd) //remove from overridedict those where both have wd match
                    {
                        if (overridedict.ContainsKey(gnid))
                            if (withwd.Contains(overridedict[gnid]))
                                overridedict.Remove(gnid);
                    }
                    Console.WriteLine("Remaining in overridedict: " + overridedict.Count.ToString());
                }


                using (StreamReader sr = new StreamReader(filename))
                {
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        string[] words = line.Split(tabchar);
                        //sw.WriteLine(gnid.ToString() + tabstring + wdid.ToString() + tabstring + gndict[gnid].area.ToString() + tabstring + gndict[gnid].population_wd.ToString() + tabstring + gndict[gnid].population_wd_iw);

                        if (words.Length < 4)
                            continue;

                        int gnid = tryconvert(words[0]);

                        if (overridedict.ContainsKey(gnid))
                        {
                            Console.WriteLine("Overriding " + gnid.ToString() + " with " + overridedict[gnid].ToString());
                            gnid = overridedict[gnid];
                        }

                        int wdid = tryconvert(words[1]);
                        if (wdid <= 0)
                            continue;

                        nwdtot++;
                        rdict.Add(gnid, wdid);
                    }
                }
            }
            catch (IOException e)
            {
                string message = e.Message;
                Console.Error.WriteLine(message);
            }

            return rdict;
        }


        public static void read_wd_file(string wdcountry)
        {
            string filename = geonamesfolder + "wikidata\\wikidata-" + wdcountry + ".txt";
            string filename_override = geonamesfolder + "wikidata\\wikidata-" + wdcountry + "-override.txt";

            if (!File.Exists(filename))
            {
                Console.WriteLine("No file " + filename);
                return;
            }

            Dictionary<int, int> overridedict = new Dictionary<int, int>();
            if (File.Exists(filename_override)) //use to override wd assignments in case of systematic errors in main wd run
            {
                int nover = 0;
                using (StreamReader sr = new StreamReader(filename_override))
                {
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        string[] words = line.Split(tabchar);
                        if (words.Length < 2)
                            continue;
                        int gnid0 = tryconvert(words[0]);
                        int gnid1 = tryconvert(words[1]);
                        if ((gnid0 <= 0) || (gnid1 <= 0))
                            continue;

                        if (overridedict.ContainsKey(gnid0))
                            continue;

                        overridedict.Add(gnid0, gnid1);
                        nover++;
                    }
                }
                Console.WriteLine("noverride = " + nover.ToString());
            }

            try
            {
                wdtime = File.GetCreationTime(@filename);
                //Console.WriteLine("wdtime = "+wdtime.ToString());
                //Console.WriteLine("<ret>");
                //Console.ReadLine();

                List<int> withwd = new List<int>();

                //first pass, in order to doublecheck overridedict
                if (overridedict.Count > 0)
                {
                    Console.WriteLine("First pass...");
                    using (StreamReader sr = new StreamReader(filename))
                    {
                        while (!sr.EndOfStream)
                        {
                            String line = sr.ReadLine();
                            string[] words = line.Split(tabchar);
                            //sw.WriteLine(gnid.ToString() + tabstring + wdid.ToString() + tabstring + gndict[gnid].area.ToString() + tabstring + gndict[gnid].population_wd.ToString() + tabstring + gndict[gnid].population_wd_iw);

                            if (words.Length < 4)
                                continue;

                            int gnid = tryconvert(words[0]);
                            withwd.Add(gnid);
                        }
                    }
                    foreach (int gnid in withwd) //remove from overridedict those where both have wd match
                    {
                        if (overridedict.ContainsKey(gnid))
                            if (withwd.Contains(overridedict[gnid]))
                                overridedict.Remove(gnid);
                    }
                    Console.WriteLine("Remaining in overridedict: " + overridedict.Count.ToString());
                }


                using (StreamReader sr = new StreamReader(filename))
                {
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        string[] words = line.Split(tabchar);
                        //sw.WriteLine(gnid.ToString() + tabstring + wdid.ToString() + tabstring + gndict[gnid].area.ToString() + tabstring + gndict[gnid].population_wd.ToString() + tabstring + gndict[gnid].population_wd_iw);

                        if (words.Length < 4)
                            continue;

                        int gnid = tryconvert(words[0]);

                        if (overridedict.ContainsKey(gnid))
                        {
                            Console.WriteLine("Overriding " + gnid.ToString() + " with " + overridedict[gnid].ToString());
                            gnid = overridedict[gnid];
                        }

                        if (!gndict.ContainsKey(gnid))
                            continue;

                        int wdid = tryconvert(words[1]);
                        if (wdid <= 0)
                            continue;

                        nwdtot++;
                        gndict[gnid].wdid = wdid;


                        if (!wdgniddict.ContainsKey(wdid))
                            wdgniddict.Add(wdid, gnid);
                        else if (wdgniddict[wdid] != gnid)
                        {// negative numbers count how many duplicates
                            if (wdgniddict[wdid] > 0)
                                wdgniddict[wdid] = -2;
                            else
                                wdgniddict[wdid]--;
                        }


                        double area = tryconvertdouble(words[2]);
                        if (area > 0)
                        {
                            //Console.WriteLine("area>0");
                            if (verifygeonames && (gndict[gnid].area > 0))
                            {
                                //Console.WriteLine("gnid-area>0");
                                double eps = 0;
                                while (areavsarea.ContainsKey(area + eps))
                                    eps += 0.00001;
                                areavsarea.Add(area + eps, gndict[gnid].area + eps);
                            }
                            gndict[gnid].area = area;
                        }

                        long pop = tryconvertlong(words[3]);
                        if (pop > 0)
                        {
                            if (verifygeonames && (gndict[gnid].population > 10))
                            {
                                Console.WriteLine("pop.vs.pop:" + gndict[gnid].population.ToString() + tabstring + pop.ToString());
                                int j = 0;
                                while (popvspop.ContainsKey(pop + j))
                                    j++;
                                popvspop.Add(pop + j, gndict[gnid].population + j);
                            }
                            if (((gndict[gnid].population < minimum_population) || !prefergeonamespop) && !verifygeonames)
                                gndict[gnid].population = pop;
                            gndict[gnid].population_wd = pop;
                            if (words.Length >= 5)
                                gndict[gnid].population_wd_iw = words[4];

                            //Console.WriteLine(gndict[gnid].Name + ": " + gndict[gnid].population_wd.ToString() + gndict[gnid].population_wd_iw);
                        }
                        //public static Dictionary<int, int> wdgnid = new Dictionary<int, int>(); //from wikidata id to geonames id; negative counts duplicates
                        //public static Dictionary<long, long> popvspop = new Dictionary<long, long>(); //comparing population for same place, wd vs gn
                        //public static Dictionary<double, double> areavsarea = new Dictionary<double, double>(); //comparing area for same place, wd vs gn


                    }
                }
            }
            catch (IOException e)
            {
                string message = e.Message;
                Console.Error.WriteLine(message);
            }


        }

        public static void read_wd_files(string countrycode)
        {
            if (countrycode == "")
            {
                foreach (int gnid in countrydict.Keys)
                    read_wd_file(countrydict[gnid].iso);
            }
            else
                read_wd_file(countrycode);
        }

        public static String stripNonValidXMLCharacters(string textIn)
        {
            StringBuilder textOut = new StringBuilder(); // Used to hold the output.
            char current; // Used to reference the current character.


            if (textIn == null || textIn == string.Empty) return string.Empty; // vacancy test.
            for (int i = 0; i < textIn.Length; i++)
            {
                current = textIn[i];


                if ((current == 0x9 || current == 0xA || current == 0xD) ||
                    ((current >= 0x20) && (current <= 0xD7FF)) ||
                    ((current >= 0xE000) && (current <= 0xFFFD)))
                //||                ((current >= 0x10000) && (current <= 0x10FFFF)))
                {
                    textOut.Append(current);
                }
            }
            return textOut.ToString();
        }



        public static string get_webpage(string url)
        {
            WebClient client = new WebClient();

            // Add a user agent header in case the
            // requested URI contains a query.

            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            try
            {
                Stream data = client.OpenRead(url);
                StreamReader reader = new StreamReader(data);
                string s = reader.ReadToEnd();
                data.Close();
                reader.Close();
                return s;
            }
            catch (WebException e)
            {
                string message = e.Message;
                Console.Error.WriteLine(message);
            }
            return "";
        }


        public static string getxmlpar(string par, XmlNode node)
        {

            foreach (XmlNode xn in node.ChildNodes)
            {
                if (xn.Name == par)
                {
                    string s = xn.InnerXml;
                    return s;
                }


            }

            return "";
        }

        public static List<int> get_wd_prop_idlist(int propid, XmlDocument cx)
        {
            List<int> rl = new List<int>();
            XmlDocument propx = new XmlDocument();
            XmlNode propnode = get_property_node(propid, cx);
            if (propnode == null)
                return rl;

            propx.AppendChild(propx.ImportNode(propnode, true));
            //  teamDoc.AppendChild(teamDoc.ImportNode(teamNode, true));
            XmlNodeList mslist = propx.GetElementsByTagName("mainsnak");
            foreach (XmlNode msn in mslist)
            {
                XmlDocument msx = new XmlDocument();
                msx.AppendChild(msx.ImportNode(msn, true));
                XmlNodeList elemlist = msx.GetElementsByTagName("value");
                foreach (XmlNode ee in elemlist)
                {
                    try
                    {
                        int rs = tryconvert(ee.Attributes.GetNamedItem("numeric-id").Value);
                        if (rs > 0)
                            rl.Add(rs);
                    }
                    catch (NullReferenceException e)
                    {
                        Console.Error.WriteLine(e.Message);
                    }
                }
            }

            return rl;

        }


        public static string get_wd_prop(int propid, XmlDocument cx)
        {
            XmlNode propnode = get_property_node(propid, cx);
            if (propnode == null)
                return "";

            //Console.WriteLine("propnode = " + propnode.ToString());

            XmlDocument propx = new XmlDocument();
            propx.AppendChild(propx.ImportNode(propnode, true));

            XmlNodeList elemlist = propx.GetElementsByTagName("datavalue");
            string rs = "";
            foreach (XmlNode ee in elemlist)
            {
                try
                {
                    rs = ee.Attributes.GetNamedItem("value").Value;
                }
                catch (NullReferenceException e)
                {
                    Console.Error.WriteLine(e.Message);
                }
            }

            Console.WriteLine("get_wd_prop:rs: " + rs);
            return rs;

        }

        public static double[] get_wd_position(XmlDocument cx)
        {
            double[] rl = { 9999.0, 9999.0 };

            XmlNode propnode = get_property_node(propdict["position"], cx);
            if (propnode == null)
                return rl;

            //Console.WriteLine("propnode = " + propnode.ToString());

            XmlDocument propx = new XmlDocument();
            propx.AppendChild(propx.ImportNode(propnode, true));

            XmlNodeList elemlist = propx.GetElementsByTagName("value");
            //string rs = "";
            foreach (XmlNode ee in elemlist)
            {
                try
                {
                    rl[0] = tryconvertdouble(ee.Attributes.GetNamedItem("latitude").Value);
                    rl[1] = tryconvertdouble(ee.Attributes.GetNamedItem("longitude").Value);
                }
                catch (NullReferenceException e)
                {
                    Console.Error.WriteLine(e.Message);
                }
            }

            Console.WriteLine("get_wd_prop:rl: " + rl[0].ToString() + " | " + rl[1].ToString());
            return rl;

        }


        public static string wdlink(string prop)
        {
            if (!propdict.ContainsKey(prop))
            {
                Console.WriteLine("Invalid property: " + prop);
                return "";
            }

            string s = "{{#property:P" + propdict[prop].ToString() + "}}";
            return s;
        }

        public static XmlDocument get_wd_xml(int wdid)
        {
            string url = "https://www.wikidata.org/w/api.php?action=wbgetentities&format=xml&ids=Q" + wdid.ToString() + "&redirects=yes";
            XmlDocument xd = new XmlDocument();
            string s = get_webpage(url);
            if (String.IsNullOrEmpty(s))
                return null;
            //Console.WriteLine(s);
            try
            {
                xd.LoadXml(s);
            }
            catch (XmlException e)
            {
                string message = e.Message;
                Console.Error.WriteLine("tl we " + message);
                return null;
            }

            return xd;
        }

        public static int get_wd_gnid(int wdid)
        {
            XmlDocument cx = get_wd_xml(wdid);
            if (cx == null)
                return -1;
            else
                return tryconvert(get_wd_prop(propdict["gnid"], cx));

        }

        public static XmlNode get_property_node(int propid, XmlDocument cx)
        {
            XmlNodeList elemlist = cx.GetElementsByTagName("property");
            //Console.WriteLine("get_property_node: elemlist: " + elemlist.Count.ToString());
            foreach (XmlNode ee in elemlist)
            {
                try
                {
                    string id = ee.Attributes.GetNamedItem("id").Value;
                    //Console.WriteLine("id = " + id);
                    if (id == "P" + propid.ToString())
                    {
                        //Console.WriteLine("get_property_node: found!");
                        return ee;
                    }
                }
                catch (NullReferenceException e)
                {
                    Console.Error.WriteLine(e.Message);
                }
            }

            //Console.WriteLine("get_property_node: not found");
            return null;

        }


        public static List<int> get_wd_kids(XmlDocument cx)
        {
            List<int> rl = new List<int>();
            List<int> rldouble = new List<int>();
            XmlDocument ee = new XmlDocument();
            XmlNode propnode = get_property_node(150, cx);
            if (propnode == null)
                return rl;

            ee.AppendChild(ee.ImportNode(propnode, true));

            //ee.DocumentElement.AppendChild(propnode);

            //XmlNode ee = get_property_node(150, cx);
            if (ee == null)
                return rl;

            XmlNodeList elemlist = ee.GetElementsByTagName("value");
            Console.WriteLine("get-wd_kids: elemlist " + elemlist.Count.ToString());

            foreach (XmlNode eee in elemlist)
            {
                Console.WriteLine("eee.Attributes: " + eee.Attributes.ToString());
                try
                {
                    string etype = eee.Attributes.GetNamedItem("entity-type").Value;
                    Console.WriteLine("etype = " + etype);
                    if (etype == "item")
                    {
                        string id = eee.Attributes.GetNamedItem("numeric-id").Value;
                        Console.WriteLine("id = " + id);
                        int iid = tryconvert(id);
                        if (iid > 0)
                        {
                            if (!rl.Contains(iid))
                                rl.Add(iid);
                            else if (!rldouble.Contains(iid))
                                rldouble.Add(iid);
                        }
                    }

                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine(e);
                }
            }

            foreach (int ii in rldouble)
                rl.Remove(ii);

            return rl;
        }

        public static Dictionary<string, string> get_wd_sitelinks(XmlDocument cx)
        {
            Dictionary<string, string> rd = new Dictionary<string, string>();

            XmlNodeList elemlist = cx.GetElementsByTagName("sitelink");
            foreach (XmlNode ee in elemlist)
            {
                try
                {
                    string lang = ee.Attributes.GetNamedItem("site").Value;
                    string value = ee.Attributes.GetNamedItem("title").Value;
                    //Console.WriteLine("get_wd_sitelinks: lang,value : " + lang + " " + value);
                    if (!rd.ContainsKey(lang))
                    {
                        rd.Add(lang, value);
                    }
                }
                catch (NullReferenceException e)
                {
                    eglob = e;
                }
            }

            return rd;
        }

        public static string iwlinks(XmlDocument cx)
        {
            Dictionary<string, string> rd = get_wd_sitelinks(cx);

            string iws = "\n\n";

            foreach (string sw in rd.Keys)
            {
                string s = sw.Replace("wiki", "");
                if (s == makelang)
                    return "Exists already:" + rd[sw];
                if ((s.Length == 2) || (s.Length == 3))
                    iws += "[[" + s + ":" + rd[sw] + "]]\n";
            }
            //Console.WriteLine("iwlinks: " + iws);

            return iws;
        }


        public static Dictionary<string, string> get_wd_name_dictionary(XmlDocument cx)
        {

            Dictionary<string, string> rd = new Dictionary<string, string>();

            XmlNodeList elemlist = cx.GetElementsByTagName("label");
            foreach (XmlNode ee in elemlist)
            {
                try
                {
                    string lang = ee.Attributes.GetNamedItem("language").Value;
                    string value = ee.Attributes.GetNamedItem("value").Value;
                    if (!rd.ContainsKey(lang))
                    {
                        rd.Add(lang, value);
                    }
                }
                catch (NullReferenceException e)
                {
                    Console.Error.WriteLine(e.Message);
                }
            }

            return rd;
        }

        public static string get_wd_name_from_xml(XmlDocument cx)
        {
            return get_wd_name_from_xml(cx, makelang);
        }

        public static string get_wd_name_from_xml(XmlDocument cx, string mainlang)
        {

            Dictionary<string, string> rd = new Dictionary<string, string>();

            if (cx == null)
                return "";

            XmlNodeList elemlist = cx.GetElementsByTagName("label");
            Console.WriteLine("elemlist " + elemlist.Count.ToString());
            foreach (XmlNode ee in elemlist)
            {
                try
                {
                    string lang = ee.Attributes.GetNamedItem("language").Value;
                    string value = ee.Attributes.GetNamedItem("value").Value;
                    //Console.WriteLine("lang,value = " + lang +"|"+ value);
                    if (!rd.ContainsKey(lang))
                    {
                        if (lang == mainlang)
                            return value;
                        else
                            rd.Add(lang, value);
                    }
                }
                catch (NullReferenceException e)
                {
                    //Console.Error.WriteLine(e.Message);
                    eglob = e;
                }
            }

            //Pick the most common form:
            Dictionary<string, int> namestats = new Dictionary<string, int>();
            foreach (string lang in rd.Keys)
            {
                if (!namestats.ContainsKey(rd[lang]))
                    namestats.Add(rd[lang], 0);
                namestats[rd[lang]]++;
            }
            string name = "";
            int maxuse = 0;
            foreach (string nn in namestats.Keys)
            {
                Console.WriteLine(nn);
                if (namestats[nn] > maxuse)
                {
                    maxuse = namestats[nn];
                    name = nn;
                    Console.WriteLine("maxuse = " + maxuse.ToString());
                }
            }

            Console.WriteLine("get_wd_name_from_xml " + name);

            return name;

        }

        public static string get_wd_name(int wdid)
        {
            string url = "https://www.wikidata.org/w/api.php?action=wbgetentities&ids=Q" + wdid.ToString() + "&props=labels&format=xml";
            string xmlitem = get_webpage(url);
            //Console.WriteLine(xmlitem);
            if (String.IsNullOrEmpty(xmlitem))
                return "";

            XmlDocument cx = new XmlDocument();
            cx.LoadXml(xmlitem);

            return get_wd_name_from_xml(cx);
        }

        public static int get_wd_item_direct(int gnid)
        {

            //http://wdq.wmflabs.org/api?q=string[1566:"2715459"]&format=xml
            string url0 = "http://wdq.wmflabs.org/api?q=string[1566:\"" + gnid.ToString() + "\"]";
            string hit = get_webpage(url0);
            string s = "";
            if (hit.IndexOf("items\":[") > 0)
            {
                s = hit.Substring(hit.IndexOf("items\":[") + 8);
                if (s.IndexOf("]") > 0)
                {
                    s = s.Substring(0, s.IndexOf("]"));
                    //Console.WriteLine("get_wd_item; s = " + s);
                    string[] items0 = s.Split(',');
                    //Console.WriteLine("Direct gnid query");
                    foreach (string item in items0)
                    {
                        if (tryconvert(item) > 0)
                        {
                            nwdhist.Add("direct gnid");
                            if (!wdgniddict.ContainsKey(tryconvert(item)))
                                wdgniddict.Add(tryconvert(item), gnid);
                            return tryconvert(item);
                        }
                    }
                }
            }
            return -1;
        }

        public static int get_wd_item(int gnid)
        {
            int wdid = get_wd_item_direct(gnid);
            if (wdid > 0)
                return wdid;


            string url1 = "http://wdq.wmflabs.org/api?q=around[625," + gndict[gnid].latitude.ToString(culture_en) + "," + gndict[gnid].longitude.ToString(culture_en) + ",2]";
            string around = get_webpage(url1);
            string s = "";
            if (around.IndexOf("items\":[") >= 0)
            {
                s = around.Substring(around.IndexOf("items\":[") + 8);
                if (s.IndexOf("]") > 0)
                {
                    s = s.Substring(0, s.IndexOf("]"));
                    string[] items = s.Split(',');

                    //List<string> withgnid = new List<string>();
                    //Console.WriteLine("Search by location and gnid");
                    //foreach (string item in items)
                    //{
                    //    Console.WriteLine("item = " + item);
                    //    string url2 = "https://www.wikidata.org/w/api.php?action=wbgetclaims&entity=Q" + item + "&format=xml&property=p1566";
                    //    string xmlitem = get_webpage(url2);

                    //    if (String.IsNullOrEmpty(xmlitem))
                    //        continue;

                    //    XmlDocument cx = new XmlDocument();
                    //    cx.LoadXml(xmlitem);

                    //    XmlNodeList elemlist = cx.GetElementsByTagName("datavalue");
                    //    foreach (XmlNode ee in elemlist)
                    //    {
                    //        try
                    //        {
                    //            string value = ee.Attributes.GetNamedItem("value").Value;
                    //            Console.WriteLine("value = " + value);
                    //            if (tryconvert(value) == gnid)
                    //            {
                    //                nwdhist.Add("loc&gnid");
                    //                if (!wdgniddict.ContainsKey(wdid))
                    //                    wdgniddict.Add(tryconvert(item), gnid);

                    //                return tryconvert(item);
                    //            }
                    //            else if (tryconvert(value) > 0)
                    //                withgnid.Add(item);
                    //        }
                    //        catch (NullReferenceException e)
                    //        {
                    //        }
                    //    }


                    //}

                    Console.WriteLine("Search by name at location");
                    foreach (string item in items)
                    {
                        //if (withgnid.Contains(item))
                        //    continue;

                        XmlDocument cx = get_wd_xml(tryconvert(item));
                        if (cx == null)
                            continue;
                        XmlNodeList elemlist = cx.GetElementsByTagName("label");
                        foreach (XmlNode ee in elemlist)
                        {
                            try
                            {
                                string value = ee.Attributes.GetNamedItem("value").Value;
                                //Console.WriteLine("value = " + value);
                                if (value == gndict[gnid].Name)
                                {
                                    nwdhist.Add("name at loc");
                                    if (!wdgniddict.ContainsKey(tryconvert(item)))
                                        wdgniddict.Add(tryconvert(item), gnid);
                                    return tryconvert(item);
                                }
                            }
                            catch (NullReferenceException e)
                            {
                                Console.Error.WriteLine(e.Message);
                            }
                        }

                    }
                }
            }

            Console.WriteLine("Search by article name in iwlangs");

            string sites = "";
            foreach (string iws in iwlang)
            {
                if (!String.IsNullOrEmpty(sites))
                    sites += "|";
                if (iws != makelang)
                    sites += iws + "wiki";
                else if (!String.IsNullOrEmpty(countrydict[countryid[makecountry]].nativewiki))
                    sites += countrydict[countryid[makecountry]].nativewiki + "wiki";
            }

            List<string> titlist = new List<string>();
            titlist.Add(gndict[gnid].Name);
            if (!titlist.Contains(gndict[gnid].Name_ml))
                titlist.Add(gndict[gnid].Name_ml);
            foreach (string an in gndict[gnid].altnames)
                if (!titlist.Contains(an))
                    titlist.Add(an);
            string titles = "";
            foreach (string tit in titlist)
            {
                if (!String.IsNullOrEmpty(tit))
                {
                    if (!String.IsNullOrEmpty(titles))
                        titles += "|";
                    titles += tit;
                }
            }

            //https://www.wikidata.org/w/api.php?action=wbgetentities&format=xml&sites=enwiki&titles=Austurland&redirects=yes&props=labels

            int entid = get_wdid_by_name(sites, titles, gnid);

            if (entid > 0)
                return entid;

            //string url3 = "https://www.wikidata.org/w/api.php?action=wbgetentities&format=xml&sites="+sites+"&titles="+titles+"&redirects=yes&props=claims";

            ////Console.WriteLine(url3);

            //string xmlitem3 = get_webpage(url3);

            //if (!String.IsNullOrEmpty(xmlitem3))
            //{
            //    XmlDocument cx = new XmlDocument();
            //    cx.LoadXml(xmlitem3);

            //    XmlNodeList elemlist = cx.GetElementsByTagName("entity");
            //    foreach (XmlNode ee in elemlist)
            //    {
            //        try
            //        {
            //            int entid = tryconvert(ee.Attributes.GetNamedItem("id").Value.Replace("Q",""));
            //            Console.WriteLine("entid = " + entid.ToString());
            //            if (entid > 0)
            //            {
            //                XmlDocument ex = new XmlDocument();
            //                ex.AppendChild(ex.ImportNode(ee, true));
            //                double[] latlong = get_wd_position(ex);
            //                if (latlong[0] + latlong[1] > 360.0)
            //                    continue;

            //                double dist = get_distance_latlong(latlong[0], latlong[1], gndict[gnid].latitude, gndict[gnid].longitude);

            //                Console.WriteLine("dist = " + dist.ToString());
            //                Console.WriteLine("gnid-latlong = " + gndict[gnid].latitude.ToString() + " | " + gndict[gnid].longitude.ToString());
            //                //Console.WriteLine("<ret>");
            //                //Console.ReadLine();

            //                if (dist < 100.0)
            //                {
            //                    nwdhist.Add("iw");
            //                    if (!wdgniddict.ContainsKey(entid))
            //                        wdgniddict.Add(entid, gnid);
            //                    return entid;
            //                }

            //            }

            //        }
            //        catch (NullReferenceException e)
            //        {
            //            Console.Error.WriteLine(e.Message);
            //        }
            //    }

            //}

            Console.WriteLine("Nothing found");
            return -1;
        }

        public static int get_wdid_by_name(string sites, string titles, int gnid)
        {
            string url3 = "https://www.wikidata.org/w/api.php?action=wbgetentities&format=xml&sites=" + sites + "&titles=" + titles + "&redirects=yes&props=claims";

            //Console.WriteLine(url3);

            string xmlitem3 = get_webpage(url3);

            if (!String.IsNullOrEmpty(xmlitem3))
            {
                XmlDocument cx = new XmlDocument();
                cx.LoadXml(xmlitem3);

                XmlNodeList elemlist = cx.GetElementsByTagName("entity");
                foreach (XmlNode ee in elemlist)
                {
                    try
                    {
                        int entid = tryconvert(ee.Attributes.GetNamedItem("id").Value.Replace("Q", ""));
                        Console.WriteLine("entid = " + entid.ToString());
                        if (entid > 0)
                        {
                            XmlDocument ex = new XmlDocument();
                            ex.AppendChild(ex.ImportNode(ee, true));
                            double[] latlong = get_wd_position(ex);
                            if (latlong[0] + latlong[1] > 360.0)
                                continue;

                            double dist = get_distance_latlong(latlong[0], latlong[1], gndict[gnid].latitude, gndict[gnid].longitude);

                            Console.WriteLine("dist = " + dist.ToString());
                            Console.WriteLine("gnid-latlong = " + gndict[gnid].latitude.ToString() + " | " + gndict[gnid].longitude.ToString());
                            //Console.WriteLine("<ret>");
                            //Console.ReadLine();

                            if (dist < 100.0)
                            {
                                nwdhist.Add("iw");
                                if (!wdgniddict.ContainsKey(entid))
                                    wdgniddict.Add(entid, gnid);
                                return entid;
                            }

                        }

                    }
                    catch (NullReferenceException e)
                    {
                        Console.Error.WriteLine(e.Message);
                    }
                }

            }
            return -1;
        }

        public static string get_name_from_wdid(int wdid)
        {
            int nbgnid = -1;
            if (wdgniddict.ContainsKey(wdid))
                nbgnid = wdgniddict[wdid];
            else
                nbgnid = get_wd_gnid(wdid);

            if (gndict.ContainsKey(nbgnid))
                return makegnidlink(nbgnid);
            else
                return get_wd_name(wdid);

        }

        public static bool check_wd_instance(int gnid, int wdid)
        {
            XmlDocument cx = get_wd_xml(wdid);
            bool foundtarget = false;

            if (cx != null)
            {
                List<int> instances = get_wd_prop_idlist(propdict["instance"], cx);

                Console.WriteLine("check_wd_instance " + instances.Count);

                //foreach (int inst in instances)
                {
                    string fcode = gndict[gnid].featurecode;

                    int target = 0;
                    string category = "default";
                    if (categorydict.ContainsKey(fcode))
                        category = categorydict[fcode];
                    if (category == "subdivisions")
                    {
                        target = catwdclass["subdivision1"];
                        foreach (int inst in instances)
                            if (search_rdf_tree(target, inst, 0))
                            {
                                foundtarget = true;
                                break;
                            }
                        if (!foundtarget)
                        {
                            target = catwdclass["subdivision2"];
                            foreach (int inst in instances)
                                if (search_rdf_tree(target, inst, 0))
                                {
                                    foundtarget = true;
                                    break;
                                }
                            if (!foundtarget)
                            {
                                target = catwdclass["subdivision3"];
                                foreach (int inst in instances)
                                    if (search_rdf_tree(target, inst, 0))
                                    {
                                        foundtarget = true;
                                        break;
                                    }
                            }
                        }
                    }
                    else if (category == "populated places")
                    {
                        //First do regular search...
                        target = catwdclass[category];
                        foreach (int inst in instances)
                            if (search_rdf_tree(target, inst, 0))
                            {
                                foundtarget = true;
                                break;
                            }

                        //...then use VETO on subdivision!
                        if (foundtarget)
                        {
                            bool foundsub = false;
                            target = catwdclass["subdivision1"];
                            foreach (int inst in instances)
                                if (search_rdf_tree(target, inst, 0))
                                {
                                    foundsub = true;
                                    break;
                                }
                            if (!foundsub)
                            {
                                target = catwdclass["subdivision2"];
                                foreach (int inst in instances)
                                    if (search_rdf_tree(target, inst, 0))
                                    {
                                        foundsub = true;
                                        break;
                                    }
                                if (!foundsub)
                                {
                                    target = catwdclass["subdivision3"];
                                    foreach (int inst in instances)
                                        if (search_rdf_tree(target, inst, 0))
                                        {
                                            foundsub = true;
                                            break;
                                        }
                                }
                            }
                            if (foundsub)
                                foundtarget = false;
                        }
                    }
                    else if (category == "mountains")
                    {
                        target = catwdclass["mountains1"];
                        foreach (int inst in instances)
                            if (search_rdf_tree(target, inst, 0))
                            {
                                foundtarget = true;
                                break;
                            }
                        if (!foundtarget)
                        {
                            target = catwdclass["mountains2"];
                            foreach (int inst in instances)
                                if (search_rdf_tree(target, inst, 0))
                                {
                                    foundtarget = true;
                                    break;
                                }
                        }
                    }
                    else
                    {
                        if (!catwdclass.ContainsKey(category))
                            category = "default";
                        target = catwdclass[category];
                        foreach (int inst in instances)
                            if (search_rdf_tree(target, inst, 0))
                            {
                                foundtarget = true;
                                break;
                            }
                    }


                }
            }

            return foundtarget;
        }

        public static void verify_wd()
        {
            read_rdf_tree();

            Dictionary<string, int> latlongobjects = new Dictionary<string, int>();
            List<int> withprop = new List<int>();
            int maxreadrdf = 10000000;

            int n = 0;
            //int n1566 = 0;
            int n625 = 0;

            //public class wdminiclass //minimal wikidata entry needed for verifying Geonames-links
            //{
            //    public int gnid = 0;
            //    public double latitude = 9999.9;
            //    public double longitude = 9999.9;
            //    public List<int> instance_of = new List<int>();
            //public double dist = 9999.9;
            //public bool okdist = false;
            //public bool okclass = false;
            //public bool goodmatch = false;

            //}

            Dictionary<int, wdminiclass> wdminidict = new Dictionary<int, wdminiclass>();

            //Console.WriteLine("First pass");
            //using (StreamReader sr = new StreamReader("wikidata-simple-statements.nt"))
            using (StreamReader sr = new StreamReader(geonamesfolder + "wikidata-only1566.nt"))
            {
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();
                    if (line.Contains("P1566"))
                        Console.WriteLine(line);
                    rdfclass rc = rdf_parse(line);
                    n++;
                    if ((n % 10000) == 0)
                        Console.WriteLine("n = " + n.ToString());
                    if (n > maxreadrdf)
                        break;

                    if (rc.obj > 0)
                    {
                        if (!wdminidict.ContainsKey(rc.obj))
                        {
                            wdminiclass wdm = new wdminiclass();
                            wdminidict.Add(rc.obj, wdm);
                        }
                        if (rc.prop == propdict["gnid"])
                        {
                            wdminidict[rc.obj].gnid = tryconvert(rc.value);
                            Console.WriteLine("gnid = " + wdminidict[rc.obj].gnid.ToString());
                        }
                        else if (rc.prop == propdict["coordinates"])
                        {
                            if (!latlongobjects.ContainsKey(rc.value))
                            {
                                latlongobjects.Add(rc.value, rc.obj);
                                n625++;
                            }
                            //else
                            //    Console.WriteLine("Repeated latlong " + rc.value);
                        }
                        else if (rc.prop == propdict["instance"])
                        {
                            if (!wdminidict[rc.obj].instance_of.Contains(rc.objlink))
                                wdminidict[rc.obj].instance_of.Add(rc.objlink);
                        }


                    }
                    else
                    {
                        if (latlongobjects.ContainsKey(rc.objstring))
                        {
                            if (!wdminidict.ContainsKey(latlongobjects[rc.objstring]))
                            {
                                wdminiclass wdm = new wdminiclass();
                                wdminidict.Add(latlongobjects[rc.objstring], wdm);
                            }
                            if (rc.prop == 6250001)
                            {
                                wdminidict[latlongobjects[rc.objstring]].latitude = tryconvertdouble(rc.value);
                            }
                            else if (rc.prop == 6250002)
                            {
                                wdminidict[latlongobjects[rc.objstring]].longitude = tryconvertdouble(rc.value);
                            }
                        }
                    }

                }
            }

            Console.WriteLine("wdminidict: " + wdminidict.Count.ToString());

            Dictionary<int, List<int>> wddoubles = new Dictionary<int, List<int>>();
            Dictionary<int, int> gnidwddict = new Dictionary<int, int>();

            int nmountains = 0;
            int nranges = 0;
            int nrangesgood = 0;

            foreach (int wdid in wdminidict.Keys)
            {
                Console.WriteLine(wdid.ToString() + "; " + wdminidict[wdid].gnid.ToString() + "; " + wdminidict[wdid].latitude.ToString() + "; " + wdminidict[wdid].longitude.ToString());
                int gnid = wdminidict[wdid].gnid;
                if (gndict.ContainsKey(gnid))
                {
                    if (!wdgniddict.ContainsKey(wdid))
                        wdgniddict.Add(wdid, gnid);
                    else if (wdgniddict[wdid] != gnid)
                    {// negative numbers count how many duplicates
                        if (wdgniddict[wdid] > 0)
                            wdgniddict[wdid] = -2;
                        else
                            wdgniddict[wdid]--;
                    }

                    if (!gnidwddict.ContainsKey(gnid))
                        gnidwddict.Add(gnid, wdid);
                    else if (gnidwddict[gnid] != wdid)
                    {
                        if (!wddoubles.ContainsKey(gnid))
                        {
                            List<int> dlist = new List<int>();
                            wddoubles.Add(gnid, dlist);
                        }
                        if (gnidwddict[gnid] > 0)
                            wddoubles[gnid].Add(gnidwddict[gnid]);
                        wddoubles[gnid].Add(wdid);
                        Console.WriteLine("Double!");
                        if (gnidwddict[gnid] > 0)
                            gnidwddict[gnid] = -2;
                        else
                            gnidwddict[gnid]--;
                    }

                    wdminidict[wdid].dist = get_distance_latlong(gndict[gnid].latitude, gndict[gnid].longitude, wdminidict[wdid].latitude, wdminidict[wdid].longitude);
                    Console.WriteLine("dist = " + wdminidict[wdid].dist.ToString("F2"));

                    string fcode = gndict[gnid].featurecode;
                    double maxdist = 10.0; //maximum acceptable distance for a match
                    if (!featurepointdict[fcode]) //... hundred times larger for non-point features
                        maxdist = 300 * maxdist;

                    wdminidict[wdid].okdist = (wdminidict[wdid].dist < maxdist);
                    Console.WriteLine("dist = " + wdminidict[wdid].dist.ToString("F2") + ", " + wdminidict[wdid].okdist.ToString());

                    int target = 0;
                    bool foundtarget = false;
                    string category = "default";
                    if (categorydict.ContainsKey(fcode))
                        category = categorydict[fcode];
                    Console.WriteLine("category = " + category + ", instances: " + wdminidict[wdid].instance_of.Count.ToString());
                    if (category == "subdivisions")
                    {
                        target = catwdclass["subdivision1"];
                        foreach (int inst in wdminidict[wdid].instance_of)
                            if (search_rdf_tree(target, inst, 0))
                            {
                                foundtarget = true;
                                break;
                            }
                        if (!foundtarget)
                        {
                            target = catwdclass["subdivision2"];
                            foreach (int inst in wdminidict[wdid].instance_of)
                                if (search_rdf_tree(target, inst, 0))
                                {
                                    foundtarget = true;
                                    break;
                                }
                            if (!foundtarget)
                            {
                                target = catwdclass["subdivision3"];
                                foreach (int inst in wdminidict[wdid].instance_of)
                                    if (search_rdf_tree(target, inst, 0))
                                    {
                                        foundtarget = true;
                                        break;
                                    }
                            }
                        }
                    }
                    else if (category == "mountains")
                    {
                        Console.WriteLine("mountains " + fcode);
                        nmountains++;
                        if (fcode == "MTS")
                            nranges++;
                        target = catwdclass["mountains1"];
                        foreach (int inst in wdminidict[wdid].instance_of)
                            if (search_rdf_tree(target, inst, 0))
                            {
                                foundtarget = true;
                                break;
                            }
                        if (!foundtarget)
                        {
                            target = catwdclass["mountains2"];
                            foreach (int inst in wdminidict[wdid].instance_of)
                                if (search_rdf_tree(target, inst, 0))
                                {
                                    foundtarget = true;
                                    break;
                                }
                        }
                        if (foundtarget)
                            nrangesgood++;
                    }
                    else
                    {
                        if (!catwdclass.ContainsKey(category))
                            category = "default";
                        target = catwdclass[category];
                        foreach (int inst in wdminidict[wdid].instance_of)
                            if (search_rdf_tree(target, inst, 0))
                            {
                                foundtarget = true;
                                break;
                            }
                    }

                    wdminidict[wdid].okclass = foundtarget;
                }
                else
                    Console.WriteLine("gnid not found");
            }

            int ngood = 0;
            foreach (int wdid in wdminidict.Keys)
            {
                wdminidict[wdid].goodmatch = (wdminidict[wdid].okclass && wdminidict[wdid].okdist);
            }

            foreach (int gnid in wddoubles.Keys)
            {
                int nokclass = 0;
                int idok = -1;
                double bestdist = 9999.9;
                int idbestdist = -1;
                foreach (int wdid in wddoubles[gnid])
                {
                    if (wdminidict[wdid].okclass)
                    {
                        nokclass++;
                        idok = wdid;
                        if (wdminidict[wdid].okdist && (wdminidict[wdid].dist < bestdist))
                        {
                            idbestdist = wdid;
                            bestdist = wdminidict[wdid].dist;
                        }
                    }
                }
                if (nokclass > 1)
                    idok = idbestdist;
                foreach (int wdid in wddoubles[gnid])
                {
                    wdminidict[wdid].goodmatch = (wdid == idok);
                }
            }

            Console.WriteLine("wdminidict: " + wdminidict.Count.ToString());

            using (StreamWriter sw = new StreamWriter("wikidata-good.nt"))
            {

                foreach (int wdid in wdminidict.Keys)
                {
                    if (wdminidict[wdid].goodmatch)
                    {
                        sw.WriteLine(wdminidict[wdid].gnid.ToString() + tabstring + wdid.ToString());
                        ngood++;
                    }
                }
                Console.WriteLine("ngood = " + ngood.ToString());
            }

            Page pbad = new Page(makesite, "Användare:Lsjbot/Bad P1566 in Wikidata");
            pbad.text = "This is a list of wikidata items with dubious links to GeoNames (P1566)\n\n";

            pbad.text += "== Wrong type of object ==\n";
            pbad.text += "Feature code on GeoNames does not match InstanceOf (P31) on Wikidata\n\n";

            int nwrongtype = 0;
            foreach (int wdid in wdminidict.Keys)
            {
                if ((!wdminidict[wdid].okclass) && (wdminidict[wdid].okdist))
                {
                    pbad.text += "* [[:d:Q" + wdid.ToString() + "]]\n";
                    nwrongtype++;
                }
            }
            Console.WriteLine("nwrongtype = " + nwrongtype.ToString());

            pbad.text += "== Position mismatch ==\n";
            pbad.text += "Latitude/longitude on GeoNames does not match latitude/longitude (P625) on Wikidata\n\n";

            int nwrongpos = 0;
            foreach (int wdid in wdminidict.Keys)
            {
                if ((!wdminidict[wdid].okdist) && (wdminidict[wdid].okclass))
                {
                    pbad.text += "* [[:d:Q" + wdid.ToString() + "]]\n";
                    nwrongpos++;
                }

            }
            Console.WriteLine("nwrongpos = " + nwrongpos.ToString());

            pbad.text += "== Both position and type mismatch ==\n";
            pbad.text += "Latitude/longitude on GeoNames does not match latitude/longitude (P625) on Wikidata ''and'' ";
            pbad.text += "feature code on GeoNames does not match InstanceOf (P31) on Wikidata\n\n";

            int nwrongboth = 0;
            foreach (int wdid in wdminidict.Keys)
            {
                if ((!wdminidict[wdid].okdist) && (!wdminidict[wdid].okclass))
                {
                    pbad.text += "* [[:d:Q" + wdid.ToString() + "]]\n";
                    nwrongboth++;
                }
            }
            Console.WriteLine("nwrongboth = " + nwrongboth.ToString());

            pbad.text += "== Duplicate entries ==\n";
            pbad.text += "Several Wikidata objects have P1566 pointing to same GeoNames entry\n\n";

            int ndup = 0;
            foreach (int gnid in wddoubles.Keys)
            {
                pbad.text += "* GeoNames ID " + gnid.ToString() + "\n";
                foreach (int wdid in wddoubles[gnid])
                {
                    pbad.text += "** [[:d:Q" + wdid.ToString() + "]]\n";
                }
                ndup++;
            }
            Console.WriteLine("ndup = " + ndup.ToString());

            Console.WriteLine("nmountains = " + nmountains.ToString());
            Console.WriteLine("nranges = " + nranges.ToString());
            Console.WriteLine("nrangesgood = " + nrangesgood.ToString());

            //trysave(pbad,3);
            using (StreamWriter sw = new StreamWriter("wikidata-bad.txt"))
            {
                sw.WriteLine(pbad.text);
            }


        }



        public static void verify_wd_online()
        {
            Dictionary<string, Dictionary<string, int>> wdclass = new Dictionary<string, Dictionary<string, int>>();

            foreach (int gnid in gndict.Keys)
            {
                if (gndict[gnid].wdid < 0)
                    continue;

                XmlDocument cx = get_wd_xml(gndict[gnid].wdid);

                int gnidwd = tryconvert(get_wd_prop(propdict["gnid"], cx));

                if ((gnidwd > 0) && (gnidwd != gnid))
                {
                    Console.WriteLine("Different gnid in wikidata");
                    continue;
                }

                double[] latlong = get_wd_position(cx);
                double dist = get_distance_latlong(gndict[gnid].latitude, gndict[gnid].longitude, latlong[0], latlong[1]);
                Console.WriteLine("distance = " + dist.ToString());
                string wdname = get_wd_name_from_xml(cx);
                Console.WriteLine(gndict[gnid].Name + " / " + wdname);

                List<int> instances = get_wd_prop_idlist(propdict["instance"], cx);

                foreach (int inst in instances)
                {
                    XmlDocument cxi = get_wd_xml(inst);
                    string instancename = get_wd_name_from_xml(cxi, "en");
                    if (!wdclass.ContainsKey(gndict[gnid].featurecode))
                    {
                        Dictionary<string, int> dd = new Dictionary<string, int>();
                        wdclass.Add(gndict[gnid].featurecode, dd);
                    }
                    if (!wdclass[gndict[gnid].featurecode].ContainsKey(instancename))
                        wdclass[gndict[gnid].featurecode].Add(instancename, 0);
                    wdclass[gndict[gnid].featurecode][instancename]++;

                    List<int> subclassof = new List<int>();
                    int k = 0;
                    do
                    {
                        subclassof.Clear();
                        subclassof = get_wd_prop_idlist(propdict["subclass"], cxi);
                        foreach (int sc in subclassof)
                        {
                            cxi = get_wd_xml(sc);
                            string scname = get_wd_name_from_xml(cxi, "en");
                            Console.WriteLine("scname = " + scname);
                            if (!wdclass.ContainsKey(gndict[gnid].featurecode))
                            {
                                Dictionary<string, int> dd = new Dictionary<string, int>();
                                wdclass.Add(gndict[gnid].featurecode, dd);
                            }
                            if (!wdclass[gndict[gnid].featurecode].ContainsKey(scname))
                                wdclass[gndict[gnid].featurecode].Add(scname, 0);
                            wdclass[gndict[gnid].featurecode][scname]++;
                        }
                        k++;
                    }
                    while ((k < 7) && (subclassof.Count > 0));
                }
            }

            using (StreamWriter sw = new StreamWriter("gnvswiki-instance.txt"))
            {

                foreach (string fc in wdclass.Keys)
                {
                    sw.WriteLine(fc);
                    foreach (string sc in wdclass[fc].Keys)
                        sw.WriteLine("   " + sc + " " + wdclass[fc][sc].ToString());
                }
            }

            //propdict.Add("instance", 31);
            //propdict.Add("subclass", 279);

        }

        public static bool climb_wd_class_tree(int wdid, string targetclass, int depth)
        {
            if (depth > 7)
                return false;

            //bool found = false;

            XmlDocument cxi = get_wd_xml(wdid);
            string scname = get_wd_name_from_xml(cxi, "en");
            if (scname == targetclass)
                return true;
            else
            {
                List<int> subclassof = get_wd_prop_idlist(propdict["subclass"], cxi);
                foreach (int sc in subclassof)
                {
                    if (climb_wd_class_tree(sc, targetclass, depth + 1))
                    {
                        return true;
                    }
                }
            }

            return false;


        }




        public static string get_terrain_type2(List<int> farlist, int gnid)
        {
            string terrain_type = "unknown";

            //int n = 0;
            //int nelev = 0;
            //double elevationsum = 0.0;
            //double elevationvar = 0.0;
            //double elevationsquare = 0.0;
            //double elevationmean = 0.0;

            //double[] elevdirsum = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            //double[] elevdirmean = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            //int[] nelevdir = { 0, 0, 0, 0, 0, 0, 0, 0 };

            //foreach (int nb in farlist)
            //{
            //    if (!gndict.ContainsKey(nb))
            //        continue;

            //    n++;

            //    if (gndict[nb].elevation > 0)
            //    {
            //        nelev++;
            //        elevationsum += gndict[nb].elevation;
            //        elevationsquare += gndict[nb].elevation * gndict[nb].elevation;
            //        int dir = get_direction(gnid, nb);
            //        if (dir > 0)
            //        {
            //            nelevdir[dir - 1]++;
            //            elevdirsum[dir - 1] += gndict[nb].elevation;
            //        }
            //    }

            //}


            //if (nelev > 10)
            //{
            //    elevationmean = elevationsum / nelev;
            //    elevationvar = elevationsquare / nelev - elevationmean * elevationmean;


            //    Console.WriteLine("elevation mean, var = " + elevationmean.ToString() + ", " + elevationvar.ToString());

            //    if (statisticsonly)
            //        evarhist.Add(elevationvar);

            //    terrain_type = terrain_label(elevationvar,elevationmean);



            //    if (gndict[gnid].elevation > 0)
            //    {
            //        if (elevationmean - gndict[gnid].elevation > 500)
            //            terrain_type += " valley";
            //        else if (elevationmean - gndict[gnid].elevation < -500)
            //            terrain_type += " peak";
            //    }

            //    //      1
            //    //   7    5
            //    //  3      4
            //    //   8    6
            //    //      2

            //    int ndir = 0;
            //    if (nelev > 20)
            //    {
            //        for (int i = 0; i < 8; i++)
            //        {
            //            if (nelevdir[i] > 0)
            //            {
            //                elevdirmean[i] = elevdirsum[i] / nelevdir[i];
            //                ndir++;
            //            }
            //            else
            //                elevdirmean[i] = -1.0;
            //        }
            //    }

            //    if (statisticsonly)
            //        ndirhist.Add(ndir);


            //}

            //if (statisticsonly)
            //{
            //    Console.WriteLine(gndict[gnid].Name + ": " + terrain_type);
            //    terrainhist.Add(terrain_type);
            //}
            return terrain_type;
        }

        public static int get_altitude(int gnid)
        {
            string dir = extractdir;
            double lat = gndict[gnid].latitude;
            double lon = gndict[gnid].longitude;
            string filename = make_hgt_filename(lat, lon);

            int[,] map = get_hgt_array(dir + filename);
            int mapsize = map.GetLength(0);

            double xfraction = lon - Math.Floor(lon);
            int pixx = Convert.ToInt32(mapsize * xfraction); //x counted in positive longitude direction
            double yfraction = lat - Math.Floor(lat);
            int pixy = mapsize - Convert.ToInt32(mapsize * yfraction); //y counted in negative latitude direction
            if (pixx >= mapsize)
                pixx = mapsize - 1;
            if (pixy >= mapsize)
                pixy = mapsize - 1;
            //Console.WriteLine(gnid.ToString() + ": " + pixx.ToString() + ", " + pixy.ToString());
            int alt = map[pixx, pixy];
            if (alt == 32768) //bad pixel
                alt = 0;
            return alt;
        }

        public static string classify_terrain(double elevationvar, double elevationmean)
        {
            string terrain_type = "";

            if (elevationvar < 2500.0) //rms < 50
            {
                terrain_type = "flat";
                if (elevationvar < 100) //rms < 10
                    terrain_type = "very " + terrain_type;
                if (elevationmean > 1500) //average higher than 1500
                    terrain_type += " high";
            }
            else if (elevationvar < 62500.0) //rms < 250
            {
                terrain_type = "hilly";
                if (elevationvar < 20000) //rms < 140
                    terrain_type = "somewhat " + terrain_type;
                if (elevationmean > 1500) //average higher than 1500
                    terrain_type += " high";
            }
            else if (elevationvar < 122500.0) //rms < 350
                terrain_type = "low-mountains";
            else if (elevationvar < 250000.0) //rms < 500
                terrain_type = "medium-mountains";
            else
                terrain_type = "high-mountains";

            return terrain_type;
        }

        public static int next_dir(int dir)
        {
            //      1
            //   7    5
            //  3      4
            //   8    6
            //      2

            switch (dir)
            {
                case 1:
                    return 5;
                case 5:
                    return 4;
                case 4:
                    return 6;
                case 6:
                    return 2;
                case 2:
                    return 8;
                case 8:
                    return 3;
                case 3:
                    return 7;
                case 7:
                    return 1;
                default:
                    return -1;
            }

        }

        public static void put_mountains_on_map(ref int[,] map, double lat, double lon)
        {
            int mapsize = map.GetLength(0);
            List<int> farlist = getneighbors(lat, lon, 20.0);
            foreach (int nb in farlist)
            {
                if (is_height(gndict[nb].featurecode))
                {
                    int xnb = get_x_pixel(gndict[nb].longitude, lon);
                    if ((xnb < 0) || (xnb >= mapsize))
                        continue;
                    int ynb = get_y_pixel(gndict[nb].latitude, lat);
                    if ((ynb < 0) || (ynb >= mapsize))
                        continue;
                    map[xnb, ynb] = nb;
                }
            }
        }


        public static void put_category_on_map(ref int[,] map, double lat, double lon, string category, double marksize) //puts gnid on map pixel
        {
            int mapsize = map.GetLength(0);

            double scale = Math.Cos(lat * 3.1416 / 180);
            double pixkmx = scale * 40000 / (360 * 1200);
            double pixkmy = 40000.0 / (360.0 * 1200.0);
            int markx = Convert.ToInt32(marksize / pixkmx);
            int marky = Convert.ToInt32(marksize / pixkmy);
            Console.WriteLine("markx,marky = " + markx + " " + marky);

            int nput = 0;
            Console.WriteLine(gndict.Count + " in gndict");
            foreach (int gnid in gndict.Keys)
            {
                //Console.WriteLine("gnid = " + gnid);
                if (categorydict.ContainsKey(gndict[gnid].featurecode) && (categorydict[gndict[gnid].featurecode] == category))
                {
                    int xgnid = get_x_pixel(gndict[gnid].longitude, lon, mapsize / 3);
                    int ygnid = get_y_pixel(gndict[gnid].latitude, lat, mapsize / 3);
                    if (in_map(xgnid, ygnid, mapsize))
                    {
                        for (int u = -markx; u < markx; u++)
                            for (int v = -marky; v < marky; v++)
                            {
                                if (in_map(xgnid + u, ygnid + v, mapsize))
                                {
                                    map[xgnid + u, ygnid + v] = gnid;
                                }
                            }
                        nput++;
                        //Console.WriteLine("put gnid " + gnid);
                    }

                }
            }
            Console.WriteLine("nput = " + nput);
        }

        public static List<int> list_category_in_map(int mapsize, double lat, double lon, string category) //puts gnid on map pixel, and returns list of put items
        {
            List<int> gnidlist = new List<int>();

            foreach (int gnid in gndict.Keys)
            {
                if (categorydict.ContainsKey(gndict[gnid].featurecode) && (categorydict[gndict[gnid].featurecode] == category))
                {
                    int xgnid = get_x_pixel(gndict[gnid].longitude, lon, mapsize / 3);
                    int ygnid = get_y_pixel(gndict[gnid].latitude, lat, mapsize / 3);
                    if (in_map(xgnid, ygnid, mapsize))
                        gnidlist.Add(gnid);
                }
            }

            return gnidlist;
        }





        public static int get_summit(int gnid, out double slat, out double slon) //seeks proper DEM summit of a mountain.
        {
            Console.WriteLine("get_summit");
            double lat = gndict[gnid].latitude;
            double lon = gndict[gnid].longitude;
            int[,] map = get_3x3map(lat, lon);
            int mapsize = map.GetLength(0);
            //double scale = Math.Cos(lat * 3.1416 / 180);
            //double pixkmx = scale * 40000 / (360 * 1200);
            //double pixkmy = 40000.0 / (360.0 * 1200.0);

            int x0 = get_x_pixel(lon, lon);
            int y0 = get_y_pixel(lat, lat);

            int[,] donemap = new int[mapsize, mapsize];

            for (int i = 0; i < mapsize; i++)
                for (int j = 0; j < mapsize; j++)
                    donemap[i, j] = 0;
            put_mountains_on_map(ref donemap, lat, lon);

            donemap[x0, y0] = -1; //negative for done, positive for mountain gnid

            int tolerance = -1; //maximum dip before going up

            int xhout = -1;
            int yhout = -1;

            //Console.WriteLine("x0,yo = " + x0.ToString() + " " + y0.ToString());
            int mtgnid = seek_highest(ref map, ref donemap, x0, y0, tolerance, out xhout, out yhout);
            //Console.WriteLine("xh,yh = " + xhout.ToString() + " " + yhout.ToString());

            double one1200 = 1.0 / 1200.0;
            double dlon = (xhout - x0) * one1200;
            double dlat = -(yhout - y0) * one1200; //reverse sign because higher pixel number is lower latitude
            slat = gndict[gnid].latitude + dlat;
            slon = gndict[gnid].longitude + dlon;

            if (mtgnid > 0) //Another mountain on summit
                return -1;
            else if (xhout < 0)
                return -1;
            else
                return map[xhout, yhout];

        }


        public static int seek_highest(ref int[,] map, ref int[,] donemap, int x0, int y0, int tolerance, out int xhout, out int yhout)
        {
            //Find nearest mountain from x0,y0. 
            //Assumes donemap contains gnid of mountains in appropriate cells.
            //Donemap =  0: untouched empty cell
            //Donemap = -1: cell touched here
            //Donemap = -2: cell to skip
            //Tolerance = permitted dip before terrain turns upwards (negative!)

            Console.WriteLine("seek_highest");

            int mapsize = map.GetLength(0);

            int maxx = x0;
            int maxy = y0;
            int minx = x0;
            int miny = y0;
            int xhigh = x0;
            int yhigh = y0;
            int xhighest = -1;
            int yhighest = -1;
            xhout = 0;
            yhout = 0;

            int x = x0;
            int y = y0;
            int newhigh = map[x0, y0];
            int highesthigh = newhigh;
            int nsame = 0;
            int nround = 0;
            int maxround = 1000;

            int maxsame = 1000;
            int[] xh = new int[maxsame];
            int[] yh = new int[maxsame];


            while (((newhigh - map[x0, y0]) >= tolerance) && (newhigh - highesthigh >= 5 * tolerance))
            {
                nround++;
                //Console.WriteLine("nround = " + nround.ToString());
                if (nround > maxround)
                    break;

                newhigh = tolerance - 1;
                for (int i = minx; i <= maxx; i++)
                    for (int j = miny; j <= maxy; j++)
                    {
                        if (donemap[i, j] == -1)
                        {
                            //Console.WriteLine("i,j=" + i.ToString() +","+ j.ToString());
                            for (int u = -1; u <= 1; u++)
                                if ((i + u > 0) && (i + u < mapsize))
                                    for (int v = -1; v <= 1; v++)
                                        if ((j + v > 0) && (j + v < mapsize))
                                            if (donemap[i + u, j + v] >= 0)
                                            {
                                                if (map[i + u, j + v] > newhigh)
                                                {
                                                    newhigh = map[i + u, j + v];
                                                    xhigh = i + u;
                                                    yhigh = j + v;
                                                    nsame = 0;
                                                }
                                                else if (map[i + u, j + v] == newhigh)
                                                {
                                                    if (nsame < maxsame)
                                                    {
                                                        xh[nsame] = i + u;
                                                        yh[nsame] = j + v;
                                                        nsame++;
                                                        //xyh.Add(Tuple.Create(i + u,j+v));
                                                    }
                                                }

                                            }
                        }
                    }

                //Console.WriteLine("newhigh = " + newhigh.ToString());
                if (newhigh > highesthigh)
                {
                    highesthigh = newhigh;
                    xhighest = xhigh;
                    yhighest = yhigh;
                    Console.WriteLine("seek_highest: highesthigh " + highesthigh.ToString() + " xh,yh = " + xhighest.ToString() + ", " + yhighest.ToString());
                }

                if ((newhigh - map[x0, y0]) > tolerance)
                {
                    if (donemap[xhigh, yhigh] > 0)
                        break;

                    donemap[xhigh, yhigh] = -1;

                    if (nsame > 0)
                    {
                        //Console.WriteLine("seek_highest: nsame = " + nsame.ToString());
                        //foreach (Tuple xy in xyh)
                        //    donemap[xy.Item1,xy.Item2] = nround;
                        for (int isame = 0; isame < nsame; isame++)
                            donemap[xh[isame], yh[isame]] = -1;
                    }


                    if (xhigh > maxx)
                    {
                        maxx = xhigh;
                        Console.WriteLine("maxx = " + maxx.ToString());
                        if (maxx >= mapsize)
                            newhigh = -9999;
                        if (maxx >= x0 + 500)
                            newhigh = -9999;
                    }
                    if (xhigh < minx)
                    {
                        minx = xhigh;
                        Console.WriteLine("minx = " + minx.ToString());
                        if (minx <= 0)
                            newhigh = -9999;
                        if (minx <= x0 - 500)
                            newhigh = -9999;
                    }
                    if (yhigh > maxy)
                    {
                        maxy = yhigh;
                        Console.WriteLine("maxy = " + maxy.ToString());
                        if (maxy >= mapsize)
                            newhigh = -9999;
                        if (maxy >= y0 + 500)
                            newhigh = -9999;
                    }
                    if (yhigh < miny)
                    {
                        miny = yhigh;
                        Console.WriteLine("miny = " + miny.ToString());
                        if (miny <= 0)
                            newhigh = -9999;
                        if (miny <= y0 - 500)
                            newhigh = -9999;
                    }
                }

                if (newhigh <= 0)
                    break;

                //Console.WriteLine("xhigh,yhigh = " + xhigh.ToString() + ", " + yhigh.ToString());
            }

            xhout = xhighest;
            yhout = yhighest;
            return donemap[xhigh, yhigh];

        }



        public static int seek_mountain(ref int[,] map, ref int[,] donemap, int x0, int y0, int tolerance)
        {
            //Find nearest mountain from x0,y0. 
            //Assumes donemap contains gnid of mountains in appropriate cells.
            //Donemap =  0: untouched empty cell
            //Donemap = -1: cell touched here
            //Donemap = -2: cell to skip
            //Tolerance = permitted dip before terrain turns upwards (negative!)

            Console.WriteLine("seek_mountain");

            int mapsize = map.GetLength(0);

            int maxx = x0;
            int maxy = y0;
            int minx = x0;
            int miny = y0;
            int xhigh = 0;
            int yhigh = 0;

            int x = x0;
            int y = y0;
            int newhigh = map[x0, y0];
            int highesthigh = newhigh;
            int nsame = 0;
            int nround = 0;
            int maxround = 10000;

            int maxsame = 1000;
            int[] xh = new int[maxsame];
            int[] yh = new int[maxsame];


            while (donemap[x, y] <= 0 && ((newhigh - map[x0, y0]) > tolerance) && (newhigh - highesthigh > 5 * tolerance))
            {
                nround++;
                if (nround > maxround)
                    break;

                newhigh = tolerance - 1;
                for (int i = minx; i <= maxx; i++)
                    for (int j = miny; j <= maxy; j++)
                    {
                        if (donemap[i, j] == -1)
                        {
                            for (int u = -1; u <= 1; u++)
                                if ((i + u > 0) && (i + u < mapsize))
                                    for (int v = -1; v <= 1; v++)
                                        if ((j + v > 0) && (j + v < mapsize))
                                            if (donemap[i + u, j + v] >= 0)
                                            {
                                                if (map[i + u, j + v] > newhigh)
                                                {
                                                    newhigh = map[i + u, j + v];
                                                    xhigh = i + u;
                                                    yhigh = j + v;
                                                    nsame = 0;
                                                }
                                                else if (map[i + u, j + v] == newhigh)
                                                {
                                                    if (nsame < maxsame)
                                                    {
                                                        xh[nsame] = i + u;
                                                        yh[nsame] = j + v;
                                                        nsame++;
                                                        //xyh.Add(Tuple.Create(i + u,j+v));
                                                    }
                                                }

                                            }
                        }
                    }

                if (newhigh > highesthigh)
                    highesthigh = newhigh;

                if ((newhigh - map[x0, y0]) > tolerance)
                {
                    if (donemap[xhigh, yhigh] > 0)
                        break;

                    donemap[xhigh, yhigh] = -1;

                    if (nsame > 0)
                    {
                        Console.WriteLine("seek_mountain: nsame = " + nsame.ToString());
                        //foreach (Tuple xy in xyh)
                        //    donemap[xy.Item1,xy.Item2] = nround;
                        for (int isame = 0; isame < nsame; isame++)
                            donemap[xh[isame], yh[isame]] = -1;
                    }


                    if (xhigh > maxx)
                    {
                        maxx = xhigh;
                        Console.WriteLine("maxx = " + maxx.ToString());
                        if (maxx >= mapsize)
                            newhigh = -9999;
                        if (maxx >= x0 + 500)
                            newhigh = -9999;
                    }
                    if (xhigh < minx)
                    {
                        minx = xhigh;
                        Console.WriteLine("minx = " + minx.ToString());
                        if (minx <= 0)
                            newhigh = -9999;
                        if (minx <= x0 - 500)
                            newhigh = -9999;
                    }
                    if (yhigh > maxy)
                    {
                        maxy = yhigh;
                        Console.WriteLine("maxy = " + maxy.ToString());
                        if (maxy >= mapsize)
                            newhigh = -9999;
                        if (maxy >= y0 + 500)
                            newhigh = -9999;
                    }
                    if (yhigh < miny)
                    {
                        miny = yhigh;
                        Console.WriteLine("miny = " + miny.ToString());
                        if (miny <= 0)
                            newhigh = -9999;
                        if (miny <= y0 - 500)
                            newhigh = -9999;
                    }
                }

                if (newhigh <= 0)
                    break;

                //Console.WriteLine("xhigh,yhigh = " + xhigh.ToString() + ", " + yhigh.ToString());
            }

            return donemap[xhigh, yhigh];

        }


        public static bool between_mountains(int gnid, out int mgnid1, out int mgnid2) //calculates whether gnid is part of a mountain. Intended for use with spurs etc.
        {
            Console.WriteLine("Between_mountains");
            double lat = gndict[gnid].latitude;
            double lon = gndict[gnid].longitude;
            int[,] map = get_3x3map(lat, lon);
            int mapsize = map.GetLength(0);
            //double scale = Math.Cos(lat * 3.1416 / 180);
            //double pixkmx = scale * 40000 / (360 * 1200);
            //double pixkmy = 40000.0 / (360.0 * 1200.0);
            mgnid1 = -1;
            mgnid2 = -1;

            int x0 = get_x_pixel(lon, lon);
            int y0 = get_y_pixel(lat, lat);

            int[,] donemap = new int[mapsize, mapsize];

            for (int i = 0; i < mapsize; i++)
                for (int j = 0; j < mapsize; j++)
                    donemap[i, j] = 0;
            put_mountains_on_map(ref donemap, lat, lon);

            if (donemap[x0, y0] > 0)
                return false;

            donemap[x0, y0] = -1; //negative for done, positive for mountain gnid

            int tolerance = -10; //maximum dip before going up

            mgnid1 = seek_mountain(ref map, ref donemap, x0, y0, tolerance);

            if (!gndict.ContainsKey(mgnid1))
                return false;

            double dlat = gndict[mgnid1].latitude - lat;
            double dlon = gndict[mgnid1].longitude - lon;

            if (Math.Abs(dlat) > Math.Abs(dlon))
            {
                if (dlat > 0) //North (smaller y!)
                {
                    for (int i = 0; i < y0; i++)
                        for (int j = 0; j < mapsize; j++)
                            donemap[j, i] = -2;
                }
                else //South (larger y!)
                {
                    for (int i = y0 + 1; i < mapsize; i++)
                        for (int j = 0; j < mapsize; j++)
                            donemap[j, i] = -2;
                }
            }
            else
            {
                if (dlon > 0) //East
                {
                    for (int i = x0 + 1; i < mapsize; i++)
                        for (int j = 0; j < mapsize; j++)
                            donemap[i, j] = -2;
                }
                else //West
                {
                    for (int i = 0; i < x0; i++)
                        for (int j = 0; j < mapsize; j++)
                            donemap[i, j] = -2;
                }
            }
            mgnid2 = seek_mountain(ref map, ref donemap, x0, y0, tolerance);

            if (!gndict.ContainsKey(mgnid2))
                return false;

            return true;
        }




        public static int attach_to_mountain(int gnid) //calculates whether gnid is part of a mountain. Intended for use with spurs etc.
        {
            Console.WriteLine("attach_to_mountain");
            double lat = gndict[gnid].latitude;
            double lon = gndict[gnid].longitude;
            int[,] map = get_3x3map(lat, lon);
            int mapsize = map.GetLength(0);
            //double scale = Math.Cos(lat * 3.1416 / 180);
            //double pixkmx = scale * 40000 / (360 * 1200);
            //double pixkmy = 40000.0 / (360.0 * 1200.0);

            int x0 = get_x_pixel(lon, lon);
            int y0 = get_y_pixel(lat, lat);

            int[,] donemap = new int[mapsize, mapsize];

            for (int i = 0; i < mapsize; i++)
                for (int j = 0; j < mapsize; j++)
                    donemap[i, j] = 0;
            put_mountains_on_map(ref donemap, lat, lon);
            if (donemap[x0, y0] > 0)
                return donemap[x0, y0];
            donemap[x0, y0] = -1; //negative for done, positive for mountain gnid

            int tolerance = -10; //maximum dip before going up

            return seek_mountain(ref map, ref donemap, x0, y0, tolerance);

        }


        public static int get_prominence(int gnid, out double width) //calculates the topographic prominence of a mountain
        {
            Console.WriteLine("get_prominence");

            double lat = gndict[gnid].latitude;
            double lon = gndict[gnid].longitude;
            //Console.WriteLine("lat, lon = " + lat.ToString() + " " + lon.ToString());
            int[,] map = get_3x3map(lat, lon);
            int mapsize = map.GetLength(0);
            double scale = Math.Cos(lat * 3.1416 / 180);
            double pixkmx = scale * 40000 / (360 * 1200);
            double pixkmy = 40000.0 / (360.0 * 1200.0);
            width = 0;


            int x0 = get_x_pixel(lon, lon);
            int y0 = get_y_pixel(lat, lat);

            int[,] donemap = new int[mapsize, mapsize];

            bool higherfound = false;
            for (int i = 0; i < mapsize; i++)
                for (int j = 0; j < mapsize; j++)
                {
                    donemap[i, j] = 0;
                    if (map[i, j] > gndict[gnid].elevation)
                        higherfound = true;
                }

            if (!higherfound) //if highest point in map, algorithm won't work
                return -1;

            donemap[x0, y0] = 1;
            int maxx = x0;
            int maxy = y0;
            int minx = x0;
            int miny = y0;

            int lowesthigh = 9999;
            int newhigh = -1;
            int badhigh = 9999;
            int xhigh = 0;
            int yhigh = 0;
            int nround = 1;
            int nroundlow = -1;
            int nsame = 0;
            int ntotal = 0;
            int maxtotal = 100000;
            //Dictionary<int,int> xyhdict = new Dictionary<int,int>();
            //List<Tuple<int, int>> xyh = new List<Tuple<int, int>>();
            int maxsame = 1000;
            int[] xh = new int[maxsame];
            int[] yh = new int[maxsame];

            while ((newhigh < gndict[gnid].elevation) || (nround < 6)) //disregards the first 5 pixels, in case of slight position mismatch
            {
                newhigh = -1;
                for (int i = minx; i <= maxx; i++)
                    for (int j = miny; j <= maxy; j++)
                    {
                        if (donemap[i, j] > 0)
                        {
                            for (int u = -1; u <= 1; u++)
                                if ((i + u > 0) && (i + u < mapsize))
                                    for (int v = -1; v <= 1; v++)
                                        if ((j + v > 0) && (j + v < mapsize))
                                            if (donemap[i + u, j + v] == 0)
                                            {
                                                if (map[i + u, j + v] > newhigh)
                                                {
                                                    newhigh = map[i + u, j + v];
                                                    xhigh = i + u;
                                                    yhigh = j + v;
                                                    nsame = 0;
                                                }
                                                else if (map[i + u, j + v] == newhigh)
                                                {
                                                    if (nsame < maxsame)
                                                    {
                                                        xh[nsame] = i + u;
                                                        yh[nsame] = j + v;
                                                        nsame++;
                                                        //xyh.Add(Tuple.Create(i + u,j+v));
                                                    }
                                                }
                                            }
                        }
                    }
                nround++;
                Console.WriteLine("get_prominence: nround,ntotal,newhigh = " + nround.ToString() + ", " + ntotal + ", " + newhigh);

                ntotal += nsame + 1;
                if (ntotal > maxtotal)
                    newhigh = badhigh;

                donemap[xhigh, yhigh] = nround;
                if (nsame > 0)
                {
                    //Console.WriteLine("get_prominence: nsame = " + nsame.ToString());
                    //foreach (Tuple xy in xyh)
                    //    donemap[xy.Item1,xy.Item2] = nround;
                    for (int isame = 0; isame < nsame; isame++)
                        donemap[xh[isame], yh[isame]] = nround;
                }

                if (newhigh < lowesthigh)
                {
                    lowesthigh = newhigh;
                    nroundlow = nround;
                }

                if (xhigh > maxx)
                {
                    maxx = xhigh;
                    if (maxx >= mapsize)
                        newhigh = badhigh;
                }
                if (xhigh < minx)
                {
                    minx = xhigh;
                    if (minx <= 0)
                        newhigh = badhigh;
                }
                if (yhigh > maxy)
                {
                    maxy = yhigh;
                    if (maxy >= mapsize)
                        newhigh = badhigh;
                }
                if (yhigh < miny)
                {
                    miny = yhigh;
                    if (miny <= 0)
                        newhigh = badhigh;
                }

                if (newhigh <= 0)
                    newhigh = badhigh;
            }

            double r2max = 0;
            int xr2max = 0;
            int yr2max = 0;
            int npix = 0;
            for (int i = minx; i <= maxx; i++)
                for (int j = miny; j <= maxy; j++)
                {
                    if ((donemap[i, j] > 0) && (donemap[i, j] < nroundlow))
                    {
                        npix++;
                        double r2 = scale * scale * (i - x0) * (i - x0) + (j - y0) * (j - y0);
                        if (r2 > r2max)
                        {
                            r2max = r2;
                            xr2max = i;
                            yr2max = j;
                        }
                    }
                }

            Console.WriteLine("get_promince: npix = " + npix.ToString());

            if (npix <= 1)
                return -1;

            if (newhigh == badhigh)
                return -1;

            r2max = 0;
            int xw = 0;
            int yw = 0;

            for (int i = minx; i <= maxx; i++)
                for (int j = miny; j <= maxy; j++)
                {
                    if ((donemap[i, j] > 0) && (donemap[i, j] < nroundlow))
                    {

                        double r2 = scale * scale * (i - xr2max) * (i - xr2max) + (j - yr2max) * (j - yr2max);
                        if (r2 > r2max)
                        {
                            r2max = r2;
                            xw = i;
                            yw = j;
                        }
                    }
                }

            width = Math.Sqrt(r2max) * pixkmy;

            Console.WriteLine("get_promince: nroundfinal = " + nround.ToString());

            if (lowesthigh < gndict[gnid].elevation)
                return gndict[gnid].elevation - lowesthigh;
            else
                return -1;
        }

        public static string get_terrain_type3(int gnid, double radius)
        {
            try
            {
                string terrain_type = get_terrain_type_latlong(ref gndict[gnid].elevation, gndict[gnid].latitude, gndict[gnid].longitude, radius);
                if (statisticsonly)
                {
                    Console.WriteLine(gndict[gnid].Name + ": " + terrain_type);
                    string[] tp = terrain_type.Split('|');
                    foreach (string ttp in tp)
                        terrainhist.Add(ttp);
                }
                return terrain_type;
            }
            catch (OutOfMemoryException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return "";
            }

        }

        public static string get_terrain_type_latlong(ref int elevation, double lat, double lon, double radius)
        {

            string terrain_type = "unknown";

            Console.WriteLine("get_terrain_type3");

            //int n = 0;
            int nelev = 0;
            int ndry = 0;
            int nocean = 0;
            int ncentral = 0;
            double elevationsum = 0.0;
            double elevationvar = 0.0;
            double elevationsquare = 0.0;
            double elevationmean = 0.0;
            double centralsum = 0.0;
            double centralvar = 0.0;
            double centralsquare = 0.0;
            double centralmean = 0.0;
            double r2ocean = 9999.9;
            int oceanmindir = -1;

            int[,] map = get_3x3map(lat, lon);
            int mapsize = map.GetLength(0);

            int x0 = get_x_pixel(lon, lon);
            int y0 = get_y_pixel(lat, lat);
            Console.WriteLine(lat.ToString() + " " + lon.ToString() + " " + x0.ToString() + " " + y0.ToString());

            if (elevation <= 0)
                elevation = map[x0, y0];
            else if (statisticsonly)
                elevdiffhist.Add(1.0 * (elevation - map[x0, y0]));


            double scale = Math.Cos(lat * 3.1416 / 180);
            double pixkmx = scale * 40000 / (360 * 1200);
            double pixkmy = 40000.0 / (360.0 * 1200.0);


            double[] elevdirsum = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            double[] elevdirmean = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            double[] elevdirsquare = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            double[] elevdirvar = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };

            int[] nelevdir = { 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] noceandir = { 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] ndrydir = { 0, 0, 0, 0, 0, 0, 0, 0 };
            double slope1sum = 0;
            double slope5sum = 0;
            double slope1mean = 0;
            double slope5mean = 0;
            int nslope = 0;

            int r = Convert.ToInt32(radius / pixkmx);
            double r2max = radius / pixkmy * radius / pixkmy;
            double r2central = r2max / 16; //central part is one quarter the radius

            for (int x = x0 - r; x < x0 + r; x++)
                if ((x > 0) && (x < mapsize - 1))
                    for (int y = y0 - r; y < y0 + r; y++)
                        if ((y > 0) && (y < mapsize - 1))
                        {
                            double r2 = scale * scale * (x - x0) * (x - x0) + (y - y0) * (y - y0);
                            if (r2 < r2max)
                            {
                                int weight = 1;
                                if (4 * r2 < r2max)
                                    weight = 4;
                                else if (3 * r2 < r2max)
                                    weight = 3;
                                else if (2 * r2 < r2max)
                                    weight = 2;
                                int dir = get_pix_direction(x, y, x0, y0, scale);
                                if (map[x, y] != 0) //not ocean
                                {
                                    if (map[x, y] != 32768) //bad pixel
                                    {
                                        nelev++;
                                        ndry += weight;
                                        elevationsum += map[x, y];
                                        elevationsquare += map[x, y] * map[x, y];
                                        if (dir > 0)
                                            ndrydir[dir - 1] += weight;
                                        if (r2 < r2central)
                                        {
                                            centralsum += map[x, y];
                                            centralsquare += map[x, y] * map[x, y];
                                            ncentral++;
                                        }
                                        else if (dir > 0)
                                        {
                                            nelevdir[dir - 1]++;
                                            elevdirsum[dir - 1] += map[x, y];
                                            elevdirsquare[dir - 1] += map[x, y] * map[x, y];
                                        }
                                        slope1sum += Math.Abs(0.001 * (map[x, y] - map[x, y - 1])) / pixkmy; //0.001 because vertical meters and horizontal km
                                        slope1sum += Math.Abs(0.001 * (map[x, y] - map[x - 1, y])) / pixkmx;
                                        if (y > 5)
                                            slope5sum += Math.Abs(0.0002 * (map[x, y] - map[x, y - 5])) / pixkmy; //0.0002 = 0.001/5 bec
                                        if (x > 5)
                                            slope5sum += Math.Abs(0.0002 * (map[x, y] - map[x - 5, y])) / pixkmx;
                                        nslope += 2;
                                    }
                                }
                                else
                                {
                                    nocean += weight;
                                    if (dir > 0)
                                    {
                                        noceandir[dir - 1] += weight;
                                    }
                                    if (r2 < r2ocean)
                                    {
                                        r2ocean = r2;
                                        oceanmindir = dir;
                                    }
                                }
                            }
                        }


            if (nelev > 10)
            {
                elevationmean = elevationsum / nelev;
                elevationvar = elevationsquare / nelev - elevationmean * elevationmean;

                slope1mean = slope1sum / nslope;
                slope5mean = slope5sum / nslope;

                if (statisticsonly)
                {
                    double sloperms = 10000 * slope1mean / (Math.Sqrt(elevationvar) + 20);
                    Console.WriteLine(sloperms.ToString());
                    slopermshist.Add(sloperms);
                }

                Console.WriteLine("elevation mean, var = " + elevationmean.ToString() + ", " + elevationvar.ToString());
                Console.WriteLine("slope mean1,mean5 = " + slope1mean.ToString() + ", " + slope5mean.ToString());

                if (statisticsonly)
                {
                    evarhist.Add(elevationvar);
                    if (elevation > 0)
                    {
                        evarhist.Add(1.0 * (elevation - map[x0, y0]));
                    }
                    slope1hist.Add(100.0 * slope1mean);
                    slope5hist.Add(100.0 * slope5mean);
                }

                terrain_type = classify_terrain(elevationvar, elevationmean);


                //      1
                //   7    5
                //  3      4
                //   8    6
                //      2

                int ndir = 0;
                string[] terrtype_sector = new string[9];

                if (ncentral > 10)
                {
                    centralmean = centralsum / ncentral;
                    centralvar = centralsquare / ncentral - centralmean * centralmean;
                    Console.WriteLine("Central elevation mean, var = " + centralmean.ToString() + ", " + centralvar.ToString());
                    terrtype_sector[8] = classify_terrain(centralvar, centralmean);

                    terrain_type += "|central " + terrtype_sector[8];
                }


                if (nelev > 20)
                {
                    //Dictionary<string, int> terrtype = new Dictionary<string, int>();
                    for (int i = 0; i < 8; i++)
                    {
                        terrtype_sector[i] = "";
                        if (nelevdir[i] > 10)
                        {
                            elevdirmean[i] = elevdirsum[i] / nelevdir[i];
                            //elevationvar = elevationsquare / nelev - elevationmean * elevationmean;
                            elevdirvar[i] = elevdirsquare[i] / nelevdir[i] - elevdirmean[i] * elevdirmean[i];
                            terrtype_sector[i] = classify_terrain(elevdirvar[i], elevdirmean[i]);
                            //if (!terrtype.ContainsKey(terrtype_sector[i]))
                            //    terrtype.Add(terrtype_sector[i], 0);
                            //terrtype[terrtype_sector[i]]++;
                            terrain_type += "|dir" + (i + 1).ToString() + " " + terrtype_sector[i];
                            ndir++;
                        }
                        else
                            elevdirmean[i] = -99999.0;
                    }
                    //if (!terrtype.ContainsKey(terrtype_sector[8]))
                    //    terrtype.Add(terrtype_sector[8], 0);
                    //terrtype[terrtype_sector[8]]++;
                    //Console.WriteLine("Types in sectors: " + terrtype.Count.ToString());
                    //if (statisticsonly)
                    //    nsameterrhist.Add(terrtype.Count.ToString());
                }



                //int[] getdircoord(int dir)

                if (statisticsonly)
                    ndirhist.Add(ndir);


            }

            int nwet = 0;
            int nbitwet = 0;
            if (nocean > 10)
            {
                int iwet = -1;
                for (int i = 0; i < 8; i++)
                {
                    if (noceandir[i] > ndrydir[i])
                    {
                        nwet++;
                        iwet = i;
                    }
                    else if (2 * noceandir[i] > ndrydir[i])
                    {
                        nbitwet++;
                    }

                }
                if (nwet > 0) //at least one sector has mostly ocean
                {
                    terrain_type += "|ocean ";
                    if ((nwet == 1) && (nbitwet == 0))
                    {
                        terrain_type += "bay " + (iwet + 1).ToString();
                    }
                    else
                    {
                        int xsum = 0;
                        int ysum = 0;
                        for (int i = 0; i < 8; i++)
                        {
                            if (noceandir[i] > ndrydir[i])
                            {
                                int[] cdir = getdircoord(i + 1);
                                xsum += cdir[0];
                                ysum += cdir[1];
                            }
                        }

                        terrain_type += "coast" + get_NSEW_from_xysum(xsum, ysum);

                    }
                }
            }

            if (nwet == 0)
            {
                double triggerdiff = Math.Sqrt(elevationvar); //enough height difference to call it a terrain feature; more needed in rugged terrain
                if (triggerdiff < 20)
                    triggerdiff = 20;
                Console.WriteLine("triggerdiff = " + triggerdiff.ToString());
                if (elevation > 0)
                {
                    if (elevationmean - elevation > triggerdiff)
                        terrain_type += "|valley ";
                    else if (elevationmean - elevation < -triggerdiff)
                        terrain_type += "|peak ";
                }

                double xsum = 0; //measures east-west slope
                double ysum = 0; //measure north-south slope
                double x0sum = 0; //centerline NS altitude
                double y0sum = 0; //centerline EW altitude
                double x2sum = 0; //periphery NS altitude
                double y2sum = 0; //periphery EW altitude
                double xysum = 0;

                for (int i = 0; i < 8; i++)
                {
                    int[] cdir = getdircoord(i + 1);
                    xsum += cdir[0] * (elevdirmean[i] - elevationmean) / 6;
                    ysum += cdir[1] * (elevdirmean[i] - elevationmean) / 6;
                    x0sum += (1 - cdir[0] * cdir[0]) * (elevdirmean[i] - elevationmean) / 2;
                    y0sum += (1 - cdir[1] * cdir[1]) * (elevdirmean[i] - elevationmean) / 2;
                    x2sum += cdir[0] * cdir[0] * (elevdirmean[i] - elevationmean) / 6;
                    y2sum += cdir[1] * cdir[1] * (elevdirmean[i] - elevationmean) / 6;
                    xysum += cdir[0] * cdir[1] * (elevdirmean[i] - elevationmean) / 4;
                }

                //xsum -= elevationmean;
                //ysum -= elevationmean;
                //x0sum -= elevationmean;
                //y0sum -= elevationmean;
                //x2sum -= elevationmean;
                //y2sum -= elevationmean;
                //xysum -= elevationmean;

                double nsridge = x0sum - x2sum; // centerline > periphery
                double ewridge = y0sum - y2sum; // centerline > periphery

                if (terrain_type.Contains("valley"))
                {
                    Console.WriteLine("xsum = " + xsum.ToString());
                    Console.WriteLine("ysum = " + ysum.ToString());
                    Console.WriteLine("x0sum = " + x0sum.ToString());
                    Console.WriteLine("y0sum = " + y0sum.ToString());
                    Console.WriteLine("x2sum = " + x2sum.ToString());
                    Console.WriteLine("y2sum = " + y2sum.ToString());
                    Console.WriteLine("xysum = " + xysum.ToString());
                    if ((nsridge < -triggerdiff) && (ewridge > -triggerdiff / 2))
                        terrain_type += "NS.."; //North-south valley
                    else if ((ewridge < -triggerdiff) && (nsridge > -triggerdiff / 2))
                        terrain_type += "EW..";
                    else if (xysum > triggerdiff)
                        terrain_type += "SWNE";
                    else if (xysum < -triggerdiff)
                        terrain_type += "SENW";
                    //Console.WriteLine(terrain_type);
                    //Console.ReadLine();
                }
                else if (terrain_type.Contains("peak"))
                {
                    if ((nsridge > triggerdiff) && (ewridge < triggerdiff / 2))
                        terrain_type += "NS.."; //North-south ridge
                    else if ((ewridge > triggerdiff) && (nsridge < triggerdiff / 2))
                        terrain_type += "EW..";
                    else if (xysum > triggerdiff)
                        terrain_type += "SENW";
                    else if (xysum < -triggerdiff)
                        terrain_type += "SWNE";
                }
                else if (Math.Abs(elevationmean - elevation) < triggerdiff / 2)
                {
                    if (Math.Abs(xsum) > 3 * Math.Abs(ysum) + triggerdiff / 5)
                    {
                        if (xsum > 0)
                            terrain_type += "|slope E"; //upwards to the East
                        else
                            terrain_type += "|slope W";
                        if (Math.Abs(xsum) > triggerdiff)
                            terrain_type += " steep";
                    }
                    else if (Math.Abs(ysum) > 3 * Math.Abs(xsum) + triggerdiff / 5)
                    {
                        if (ysum > 0)
                            terrain_type += "|slope N";
                        else
                            terrain_type += "|slope S";
                        if (Math.Abs(ysum) > triggerdiff + 100)
                            terrain_type += " steep";
                    }
                }
            }

            return terrain_type;
        }

        public static string get_terrain_type(int gnid, double radius)
        {
            //List<int> farlist = getneighbors(gnid, 20.0);
            //return get_terrain_type2(farlist,gnid);
            return get_terrain_type3(gnid, radius);
        }

        public static string get_terrain_type_island(int gnid)
        {
            string terrain_type = "unknown";

            Console.WriteLine("get_terrain_type_island");

            //int n = 0;
            int nelev = 0;
            //int nocean = 0;
            double elevationsum = 0.0;
            double elevationvar = 0.0;
            double elevationsquare = 0.0;
            double elevationmean = 0.0;
            double elevationmax = 0.0;
            //double r2ocean = 9999.9;
            //int oceanmindir = -1;

            int[,] map = get_3x3map(gndict[gnid].latitude, gndict[gnid].longitude);
            int mapsize = map.GetLength(0);

            int x0 = get_x_pixel(gndict[gnid].longitude, gndict[gnid].longitude);
            int y0 = get_y_pixel(gndict[gnid].latitude, gndict[gnid].latitude);

            if (gndict[gnid].elevation <= 0)
                gndict[gnid].elevation = map[x0, y0];
            else if (statisticsonly)
                elevdiffhist.Add(1.0 * (gndict[gnid].elevation - map[x0, y0]));

            byte[,] fillmap = new byte[mapsize, mapsize];


            for (int x = 0; x < mapsize; x++)
                for (int y = 0; y < mapsize; y++)
                    fillmap[x, y] = 1;

            floodfill(ref fillmap, ref map, x0, y0, 0, 0, false);

            if (fillmap[0, 0] == 3) //fill failure
                return terrain_type;

            double scale = Math.Cos(gndict[gnid].latitude * 3.1416 / 180);
            //double pixkmx = scale * 40000 / (360 * 1200);
            //double pixkmy = 40000.0 / (360.0 * 1200.0);


            double[] elevdirsum = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            double[] elevdirmean = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            int[] nelevdir = { 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] noceandir = { 0, 0, 0, 0, 0, 0, 0, 0 };

            //int r = Convert.ToInt32(radius / pixkmx);
            //double r2max = radius / pixkmy * radius / pixkmy;

            for (int x = 0; x < mapsize; x++)
                for (int y = 0; y < mapsize; y++)
                {
                    if (fillmap[x, y] == 2)
                    {
                        if (map[x, y] != 32768) //bad pixel
                        {
                            int dir = get_pix_direction(x, y, x0, y0, scale);
                            nelev++;
                            elevationsum += map[x, y];
                            elevationsquare += map[x, y] * map[x, y];
                            if (map[x, y] > elevationmax)
                                elevationmax = map[x, y];
                            if (dir > 0)
                            {
                                nelevdir[dir - 1]++;
                                elevdirsum[dir - 1] += map[x, y];
                            }
                        }
                    }
                }


            if (nelev > 10)
            {
                elevationmean = elevationsum / nelev;
                elevationvar = elevationsquare / nelev - elevationmean * elevationmean;


                Console.WriteLine("elevation mean, var = " + elevationmean.ToString() + ", " + elevationvar.ToString());

                if (statisticsonly)
                {
                    evarhist.Add(elevationvar);
                    if (gndict[gnid].elevation > 0)
                    {
                        evarhist.Add(1.0 * (gndict[gnid].elevation - map[x0, y0]));
                    }
                }

                //Inflate variance for very small areas, to make terrain more intuitive on islets
                if (nelev < 50)
                    elevationvar *= 16;
                else if (nelev < 600)
                {
                    double inflator = 800 / Convert.ToDouble(nelev);
                    elevationvar *= inflator;
                }

                terrain_type = classify_terrain(elevationvar, elevationmean);

                //      1
                //   7    5
                //  3      4
                //   8    6
                //      2

                int ndir = 0;
                if (nelev > 20)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (nelevdir[i] > 0)
                        {
                            elevdirmean[i] = elevdirsum[i] / nelevdir[i];
                            ndir++;
                        }
                        else
                            elevdirmean[i] = -99999.0;
                    }
                }


                //int[] getdircoord(int dir)

                if (statisticsonly)
                    ndirhist.Add(ndir);


            }

            if (elevationmax > 0)
                terrain_type += "|elevation_max " + elevationmax.ToString();


            if (statisticsonly)
            {
                Console.WriteLine(gndict[gnid].Name + ": " + terrain_type);
                terrainhist.Add(terrain_type);
            }
            return terrain_type;
        }


        public static int get_direction_from_NSEW(string NSEW)
        {
            switch (NSEW.Trim())
            {
                case "N.":
                    return 1;
                case "S.":
                    return 2;
                case ".W":
                    return 3;
                case ".E":
                    return 4;
                case "NE":
                    return 5;
                case "SE":
                    return 6;
                case "NW":
                    return 7;
                case "SW":
                    return 8;
                default:
                    return -1;
            }
        }

        public static string get_NSEW_from_xysum(int xsum, int ysum)
        {
            string rs = "";
            if (xsum > 0)
            {
                if (ysum > 0)
                    rs = " NE";
                else if (ysum < 0)
                    rs = " SE";
                else
                    rs = " .E";
            }
            else if (xsum < 0)
            {
                if (ysum > 0)
                    rs = " NW";
                else if (ysum < 0)
                    rs = " SW";
                else
                    rs = " .W";
            }
            else if (ysum > 0)
                rs = " N.";
            else if (ysum < 0)
                rs = " S.";
            else
                rs = " C.";
            return rs;
        }

        public static string terrain_label(string terr)
        {
            string rt = "";
            if (terr.Contains("flat"))
            {
                if (terr.Contains("high"))
                    rt = mp(183);
                else if (terr.Contains("very "))
                    rt = mp(182);
                else
                    rt = mp(109);
            }
            else if (terr.Contains("hilly"))
            {
                if (terr.Contains("somewhat"))
                    rt = mp(184);
                else
                    rt = mp(111);
            }
            else if (terr.Contains("high-mountains"))
                rt = mp(112);
            else if (terr.Contains("low-mountains"))
                rt = mp(185);
            else if (terr.Contains("mountains"))
                rt = mp(110);
            else
                rt = mp(186);
            return rt;
        }

        public static bool is_height(string fcode)
        {
            if (fcode == "MTS")
                return false;
            else if (fcode == "HLLS")
                return false;
            else if (fcode == "NTKS")
                return false;
            else if (fcode == "PKS")
                return false;
            else if (categorydict[fcode] == "mountains")
                return true;
            else if (categorydict[fcode] == "hills")
                return true;
            else if (categorydict[fcode] == "volcanoes")
                return true;
            else
                return false;
        }

        public static int imp_mountainpart(string fcode)
        {
            switch (fcode)
            {
                case "SPUR":
                case "PROM":
                    return 197;
                case "BNCH":
                case "CLF":
                case "RKFL":
                case "SLID":
                case "TAL":
                case "CRQ":
                case "CRQS":
                    return 203;
                default:
                    return -1;
            }
        }

        public static bool human_touched(Page p, Site site) //determines if an article has been edited by a human user with account (not ip or bot)
        {
            string xmlSrc;
            bool ht = false;
            try
            {
                xmlSrc = site.PostDataAndGetResult(site.address + "/w/api.php", "action=query&format=xml&prop=revisions&titles=" + HttpUtility.UrlEncode(p.title) + "&rvlimit=20&rvprop=user");
            }
            catch (WebException e)
            {
                string message = e.Message;
                Console.Error.WriteLine(message);
                return true;
            }

            XmlDocument xd = new XmlDocument();
            xd.LoadXml(xmlSrc);

            XmlNodeList elemlist = xd.GetElementsByTagName("rev");

            Console.WriteLine("elemlist.Count = " + elemlist.Count);
            //Console.WriteLine(xmlSrc);

            foreach (XmlNode ee in elemlist)
            {

                try
                {

                    string username = ee.Attributes.GetNamedItem("user").Value;
                    Console.WriteLine(username);
                    if (!username.ToLower().Contains("bot") && (get_alphabet(username) != "none"))
                    {
                        ht = true;
                        break;
                    }

                }
                catch (NullReferenceException e)
                {
                    string message = e.Message;
                    Console.Error.WriteLine(message);
                }
            }

            return ht;
        }


        public static string terrain_text(string terrain_type, int gnid)
        {
            if (terrain_type == "")
                return "";
            if (terrain_type == "unknown")
                return "";
            string rt = "";
            string[] p98 = { gndict[gnid].Name_ml };

            string[] words = terrain_type.Split('|');
            string main_terrain = words[0];
            Dictionary<string, int> terrsector = new Dictionary<string, int>();
            List<string> maintype = new List<string>();
            maintype.Add("flat");
            maintype.Add("hilly");
            maintype.Add("mountains");
            int nsector = 0;
            string centralterrain = "";
            string majorterrain = "";
            string minorterrain = "";
            foreach (string w in words)
            {
                if ((w.IndexOf("dir") == 0) || (w.IndexOf("central") == 0))
                {
                    foreach (string ttype in maintype)
                    {
                        if (w.Contains(ttype))
                        {
                            if (!terrsector.ContainsKey(ttype))
                                terrsector.Add(ttype, 0);
                            terrsector[ttype]++;
                            nsector++;
                            if (w.IndexOf("central") == 0)
                                centralterrain = ttype;
                        }
                    }
                }
            }

            bool allsame = false;
            bool varied = false;
            bool singlediff = false;
            int tmax = -1;
            string tmaxtype = "";
            string tmintype = "";
            //string dirterrain = "";
            if (terrsector.Count <= 1)
            {
                allsame = true;
            }
            else if (terrsector.Count == 2)
            {
                foreach (string ttype in terrsector.Keys)
                {
                    if (terrsector[ttype] > tmax)
                    {
                        tmax = terrsector[ttype];
                        tmaxtype = ttype;
                    }
                }
                if (2 * tmax >= nsector) //at least half are same type
                {
                    int xsum = 0;
                    int ysum = 0;
                    foreach (string ttype in terrsector.Keys)
                    {
                        if (ttype != tmaxtype)
                        {
                            tmintype = ttype;
                        }
                    }
                    if (nsector - tmax == 1) //a single sector different
                    {
                        singlediff = true;
                        foreach (string w in words)
                        {
                            if ((w.IndexOf("dir") == 0) && (w.Contains(tmintype)))
                            {
                                int i = tryconvert(w.Substring(3, 1));
                                int[] cdir = getdircoord(i);
                                xsum += cdir[0];
                                ysum += cdir[1];
                            }
                        }
                        minorterrain = tmintype + get_NSEW_from_xysum(xsum, ysum);
                        majorterrain = tmaxtype;
                    }
                    else //minority more than a single sector
                    {
                        xsum = 0;
                        ysum = 0;
                        foreach (string w in words)
                        {
                            if ((w.IndexOf("dir") == 0) && (w.Contains(tmaxtype)))
                            {
                                int i = tryconvert(w.Substring(3, 1));
                                int[] cdir = getdircoord(i);
                                xsum += cdir[0];
                                ysum += cdir[1];
                            }
                        }
                        string major_NSEW = get_NSEW_from_xysum(xsum, ysum);
                        int majordir = get_direction_from_NSEW(major_NSEW);
                        if (majordir <= 0)
                        {
                            varied = true;
                            main_terrain = "mixed";
                        }
                        else
                        {
                            bool rightmajor = false;
                            foreach (string w in words)
                            {
                                if (w.Contains("dir" + majordir.ToString()))
                                {
                                    if (w.Contains(tmaxtype))
                                        rightmajor = true;
                                }
                            }
                            if (!rightmajor)
                            {
                                varied = true;
                                main_terrain = "mixed";
                            }
                            else
                            {
                                majorterrain = tmaxtype + major_NSEW;
                                xsum = 0;
                                ysum = 0;
                                foreach (string w in words)
                                {
                                    if ((w.IndexOf("dir") == 0) && (w.Contains(tmintype)))
                                    {
                                        int i = tryconvert(w.Substring(3, 1));
                                        int[] cdir = getdircoord(i);
                                        xsum += cdir[0];
                                        ysum += cdir[1];
                                    }
                                }
                                minorterrain = tmintype + get_NSEW_from_xysum(xsum, ysum);
                            }
                        }
                    }
                }
                else
                {
                    varied = true;
                    main_terrain = "mixed";
                }
            }
            else
            {
                varied = true;
                main_terrain = "mixed";
            }

            Console.WriteLine("majorterrain=" + majorterrain);
            Console.WriteLine("minorterrain=" + minorterrain);

            //terrain header:

            bool peakvalley = true; //true if it should be written that something is on a peak or in a valley

            if (categorydict[gndict[gnid].featurecode] == "peninsulas")
            {
                rt = mp(170, p98) + " "; //terrain landwards from a peninsula
                peakvalley = false;
            }
            else if (categorydict[gndict[gnid].featurecode] == "islands")
            {
                rt = mp(194, p98) + " "; //terrain ON an island
                peakvalley = false;
            }
            else if (featurepointdict[gndict[gnid].featurecode])
            {
                rt = mp(98, p98) + " "; //terrain around a point
                peakvalley = !is_height(gndict[gnid].featurecode);

            }
            else
            {
                rt = mp(141, p98) + " "; //terrain in an area
                peakvalley = false;
            }

            //terrain label:

            if (allsame)
            {
                if (terrain_type.Contains("peak") || terrain_type.Contains("valley")) //add "mostly" if peak or valley. Sounds funny if combined with "flat" otherwise.
                    rt += mp(187) + " ";
                rt += terrain_label(main_terrain); //main terrain
            }
            else if (singlediff)
            {
                rt += mp(187) + " " + terrain_label(tmaxtype); //mostly
                if (minorterrain.Contains(centralterrain))
                {
                    rt += ", " + mp(188) + " " + terrain_label(centralterrain);
                }
                else
                {
                    string NSEW = minorterrain.Replace(tmintype, "").Trim();
                    int dir = get_direction_from_NSEW(NSEW);
                    if (dir > 0)
                    {
                        string[] p189 = new string[] { mp(120 + dir) };
                        rt += ", " + mp(189, p189) + " " + terrain_label(tmintype);
                    }
                }
            }
            else if (varied)
                rt += terrain_label("mixed");
            else
            {
                string major_NSEW = majorterrain.Replace(tmaxtype, "").Trim();
                string minor_NSEW = minorterrain.Replace(tmintype, "").Trim();
                int majordir = get_direction_from_NSEW(major_NSEW);
                int minordir = get_direction_from_NSEW(minor_NSEW);

                if (majordir <= 0)
                    rt += terrain_label("mixed");
                else
                {
                    rt += terrain_label(majorterrain) + " " + mp(120 + majordir); //main terrain

                    if (minordir > 0)
                    {
                        string[] p189 = new string[] { mp(120 + minordir) };
                        rt += ", " + mp(189, p189) + " " + terrain_label(minorterrain);
                    }

                }
            }

            //coast, peak/valley:

            if (featurepointdict[gndict[gnid].featurecode])
            {

                foreach (string w in words)
                {
                    if (w.Contains("ocean"))
                    {
                        if (w.Contains("coast"))
                        {
                            string NSEW = w.Substring(w.IndexOf("coast") + 6, 2);
                            int dir = get_direction_from_NSEW(NSEW);
                            if (dir > 0)
                            {
                                if (!String.IsNullOrEmpty(rt))
                                    rt += ". ";
                                string[] p144 = new string[2] { gndict[gnid].Name_ml, mp(120 + dir) };
                                rt += initialcap(mp(144, p144));
                            }
                        }
                        else if (w.Contains("bay"))
                        {
                            string NSEW = w.Substring(w.IndexOf("bay") + 4, 1);
                            int dir = tryconvert(NSEW);
                            if (dir > 0)
                            {
                                if (!String.IsNullOrEmpty(rt))
                                    rt += ". ";
                                string[] p144 = new string[2] { gndict[gnid].Name_ml, mp(120 + dir) };
                                rt += initialcap(mp(145) + " " + mp(144, p144));
                            }
                        }
                    }
                    else if ((w.Contains("peak")) || w.Contains("valley"))
                    {
                        string[] p205 = new string[1] { get_nsew(w) };
                        if (peakvalley || !String.IsNullOrEmpty(p205[0]))
                        {
                            if (w.Contains("peak"))
                            {
                                if (!String.IsNullOrEmpty(rt))
                                    rt += ". ";

                                rt += mp(154, p98);


                                if (!String.IsNullOrEmpty(p205[0]))
                                    rt += " " + mp(205, p205);

                            }
                            else if (w.Contains("valley"))
                            {
                                //if ((nsridge < -triggerdiff) && (ewridge > -triggerdiff / 2))
                                //    terrain_type += "NS.."; //North-south valley
                                //else if ((ewridge < -triggerdiff) && (nsridge > -triggerdiff / 2))
                                //    terrain_type += "EW..";
                                //else if (xysum > triggerdiff)
                                //    terrain_type += "SWNE";
                                //else if (xysum < -triggerdiff)
                                //    terrain_type += "SENW";

                                if (!String.IsNullOrEmpty(rt))
                                    rt += ". ";

                                rt += mp(149, p98);
                                //string[] p205 = new string[1] { get_nsew(w) };

                                if (!String.IsNullOrEmpty(p205[0]))
                                    rt += " " + mp(205, p205);
                            }
                        }
                    }
                    else if (w.Contains("slope"))
                    {
                        if (!String.IsNullOrEmpty(rt) && (allsame || varied))
                            rt += ", " + mp(147);
                        else
                        {
                            if (!String.IsNullOrEmpty(rt))
                                rt += ". ";
                            rt += mp(146, p98);
                        }
                        if (w.Contains("steep"))
                            rt += " " + mp(148);
                        if (w.Contains("N"))
                            rt += " " + mp(122); //reverse because NSEW-coding is upwards and text should be downwards slope
                        else if (w.Contains("S"))
                            rt += " " + mp(121);
                        else if (w.Contains("E"))
                            rt += " " + mp(123);
                        else if (w.Contains("W"))
                            rt += " " + mp(124);


                    }
                }
            }

            if (!String.IsNullOrEmpty(rt))
                rt += ".";
            return rt;
        }

        public static string get_nsew(double angle) //compass angle in radians. East = 0.
        {
            double ang = angle;
            if (ang < 0)
                ang += Math.PI;
            ang *= 180.0 / Math.PI; //convert to degrees

            if ((ang < 25) || (ang > 155))
                return get_nsew("EW..");
            else if ((ang >= 25) && (ang <= 65))
                return get_nsew("SWNE");
            else if ((ang > 65) && (ang < 115))
                return get_nsew("NS..");
            else if ((ang >= 115) && (ang <= 155))
                return get_nsew("SENW");
            else
                return "";
        }

        public static string get_nsew(string nsew)
        {
            string rt = "";
            if (nsew.Contains("NS.."))
                rt = mp(150);
            else if (nsew.Contains("EW.."))
                rt = mp(151);
            else if (nsew.Contains("SWNE"))
                rt = mp(152);
            else if (nsew.Contains("SENW"))
                rt = mp(153);
            return rt;
        }

        public static string get_overrep(double lat, double lon, double nbradius)
        {
            //double nbradius = 20.0;
            List<int> farlist = getneighbors(lat, lon, nbradius);

            int nnb = 0;
            Dictionary<string, int> nbcount = new Dictionary<string, int>();

            foreach (int nb in farlist)
            {
                if (!gndict.ContainsKey(nb))
                    continue;


                if (catnormdict.ContainsKey(categorydict[gndict[nb].featurecode]))
                {
                    nnb++;

                    if (!nbcount.ContainsKey(categorydict[gndict[nb].featurecode]))
                        nbcount.Add(categorydict[gndict[nb].featurecode], 0);
                    nbcount[categorydict[gndict[nb].featurecode]]++;
                }
            }

            Console.WriteLine("nnb = " + nnb.ToString());


            List<string> overrep = new List<string>();
            //int nbsum = 0;

            foreach (string scat in nbcount.Keys)
            {
                //nbsum += nbcount[scat];
                //Console.WriteLine(scat + ": " + (nbcount[scat] / (1.0*nnb)).ToString("F", culture) + " (" + catnormdict[scat].ToString("F", culture) + ")");
                if ((nbcount[scat] > 3 * catnormdict[scat] * nnb) && (nbcount[scat] > (catnormdict[scat] * nnb + 5)))
                {
                    Console.WriteLine("Overrepresented! " + scat);
                    overrep.Add(categoryml[scat]);
                    foverrephist.Add(scat);
                }
            }
            //Console.WriteLine("nbsum = " + nbsum);

            string overlist = "";
            if (overrep.Count > 0)
            {
                int noo = 0;
                foreach (string oo in overrep)
                {
                    noo++;
                    if (noo > 1)
                    {
                        if (noo == overrep.Count)
                            overlist += mp(97);
                        else
                            overlist += ",";
                    }
                    overlist += " " + oo;
                }
            }

            return overlist.Trim();
        }

        public static string get_overrep(int gnid, double nbradius)
        {

            string overlist = get_overrep(gndict[gnid].latitude, gndict[gnid].longitude, nbradius);
            Console.WriteLine("overlist = " + overlist);
            if (String.IsNullOrEmpty(overlist))
                return "";

            string[] p133 = { gndict[gnid].Name_ml, overlist };
            string[] p138 = { nbradius.ToString("F0") };
            string text = " " + mp(133, p133) + addnote(mp(138, p138) + geonameref(gnid));
            return text;
        }

        public static double[] get_nearhigh(int gnid, List<int> farlist, double radius, double minradius, out int nearhigh, out int altitude)
        {
            nearhigh = -1;
            altitude = 0;
            double[] latlong = { 9999.9, 9999.9 };

            if (!gndict.ContainsKey(gnid))
                return latlong;

            double emax = 0.0;
            double emin = 9999.9;
            int nmax = 0;
            int nmin = 0;

            double maxelevation = 0.0;
            double maxheight = 0.0;
            int nbmaxh = -1;
            double elevation = 0.0;

            //double minradius = 1.0; 

            foreach (int nb in farlist)
            {
                if (!gndict.ContainsKey(nb))
                    continue;


                if (gndict[nb].elevation > 0)
                {
                    if (gndict[nb].elevation > emax)
                    {
                        emax = gndict[nb].elevation;
                        nmax = nb;
                    }
                    if (gndict[nb].elevation < emin)
                    {
                        emin = gndict[nb].elevation;
                        nmin = nb;
                    }

                    if ((gndict[gnid].elevation > 0) && (gndict[gnid].elevation < gndict[nb].elevation))
                    {
                        double dist = get_distance(gnid, nb);
                        if (dist > minradius)
                        {
                            elevation = (gndict[nb].elevation - gndict[gnid].elevation) / dist;
                            if (elevation > maxelevation)
                            {
                                maxelevation = elevation;
                                nearhigh = nb;
                            }
                        }
                        else if (gndict[nb].elevation > maxheight)
                        {
                            maxheight = gndict[nb].elevation;
                            nbmaxh = nb;
                        }

                    }

                }

            }

            int[,] map = get_3x3map(gndict[gnid].latitude, gndict[gnid].longitude);
            int mapsize = map.GetLength(0);

            int x0 = get_x_pixel(gndict[gnid].longitude, gndict[gnid].longitude);
            int y0 = get_y_pixel(gndict[gnid].latitude, gndict[gnid].latitude);

            double scale = Math.Cos(gndict[gnid].latitude * 3.1416 / 180);
            double pixkmx = scale * 40000 / (360 * 1200);
            double pixkmy = 40000.0 / (360.0 * 1200.0);


            double[] elevdirsum = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            double[] elevdirmean = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            int[] nelevdir = { 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] noceandir = { 0, 0, 0, 0, 0, 0, 0, 0 };

            int r = Convert.ToInt32(radius / pixkmx);
            double r2max = radius / pixkmy * radius / pixkmy;
            double r2min = minradius / pixkmy * minradius / pixkmy;
            double maxelevationdem = 0;
            double maxheightdem = 0.0;

            for (int x = x0 - r; x < x0 + r; x++)
                if ((x > 0) && (x < mapsize))
                    for (int y = y0 - r; y < y0 + r; y++)
                        if ((y > 0) && (y < mapsize))
                        {
                            double r2 = scale * scale * (x - x0) * (x - x0) + (y - y0) * (y - y0);
                            if (r2 < r2max)
                            {
                                if (map[x, y] != 0) //not ocean
                                {
                                    if (map[x, y] != 32768) //bad pixel
                                    {

                                        if (r2 > r2min)
                                        {
                                            double dist = Math.Sqrt(r2) * pixkmy;
                                            elevation = (map[x, y] - gndict[gnid].elevation) / dist;
                                            if (elevation > maxelevationdem)
                                            {
                                                maxelevationdem = elevation;
                                                double one1200 = 1.0 / 1200.0;
                                                double dlon = (x - x0) * one1200;
                                                double dlat = -(y - y0) * one1200; //reverse sign because higher pixel number is lower latitude
                                                latlong[0] = gndict[gnid].latitude + dlat;
                                                latlong[1] = gndict[gnid].longitude + dlon;
                                                altitude = map[x, y];
                                            }
                                        }
                                        else if (map[x, y] > maxheightdem)
                                        {
                                            maxheightdem = map[x, y];
                                        }

                                    }
                                }
                            }
                        }


            if (maxelevationdem > 1.1 * maxelevation)
            {
                nearhigh = -1;
                if (maxheightdem > altitude)
                {
                    latlong[0] = 9999;
                    latlong[1] = 9999;
                }

            }
            else if (gndict.ContainsKey(nearhigh))
            {
                latlong[0] = gndict[nearhigh].latitude;
                latlong[1] = gndict[nearhigh].longitude;
                altitude = gndict[nearhigh].elevation;
                if ((maxheightdem > altitude) || (maxheight > altitude))
                {
                    latlong[0] = 9999;
                    latlong[1] = 9999;
                }
            }

            return latlong;
        }

        public static double[] get_highest(int gnid, double radius, out int altitude)
        {
            //Find highest DEM point within radius
            altitude = 0;
            double[] latlong = { 9999.9, 9999.9 };

            if (!gndict.ContainsKey(gnid))
                return latlong;

            double elevation = 0.0;

            int[,] map = get_3x3map(gndict[gnid].latitude, gndict[gnid].longitude);
            int mapsize = map.GetLength(0);

            int x0 = get_x_pixel(gndict[gnid].longitude, gndict[gnid].longitude);
            int y0 = get_y_pixel(gndict[gnid].latitude, gndict[gnid].latitude);

            double scale = Math.Cos(gndict[gnid].latitude * 3.1416 / 180);
            double pixkmx = scale * 40000 / (360 * 1200);
            double pixkmy = 40000.0 / (360.0 * 1200.0);


            double[] elevdirsum = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            double[] elevdirmean = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            int[] nelevdir = { 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] noceandir = { 0, 0, 0, 0, 0, 0, 0, 0 };

            int r = Convert.ToInt32(radius / pixkmx);
            double r2max = radius / pixkmy * radius / pixkmy;
            //double r2min = minradius / pixkmy * minradius / pixkmy;
            double maxelevationdem = 0;


            Console.WriteLine("pixkmx = " + pixkmx.ToString());
            Console.WriteLine("r = " + r.ToString());
            Console.WriteLine("r2max = " + r2max.ToString());

            for (int x = x0 - r; x < x0 + r; x++)
                if ((x > 0) && (x < mapsize))
                    for (int y = y0 - r; y < y0 + r; y++)
                        if ((y > 0) && (y < mapsize))
                        {
                            double r2 = scale * scale * (x - x0) * (x - x0) + (y - y0) * (y - y0);
                            if (r2 < r2max)
                            {
                                if (map[x, y] != 0) //not ocean
                                {
                                    if (map[x, y] != 32768) //bad pixel
                                    {
                                        //double dist = Math.Sqrt(r2) * pixkmy;
                                        elevation = map[x, y];
                                        if (elevation > maxelevationdem)
                                        {
                                            maxelevationdem = elevation;
                                            double one1200 = 1.0 / 1200.0;
                                            double dlon = (x - x0) * one1200;
                                            double dlat = -(y - y0) * one1200; //reverse sign because higher pixel number is lower latitude
                                            latlong[0] = gndict[gnid].latitude + dlat;
                                            latlong[1] = gndict[gnid].longitude + dlon;
                                            altitude = map[x, y];
                                        }
                                    }
                                }
                            }
                        }

            return latlong;
        }

        public static string make_town(int gnid)
        {
            string text = "";

            //List<int> nearlist = getneighbors(gnid, 10.0);
            double nbradius = 20.0;
            List<int> farlist = getneighbors(gnid, nbradius);

            double ttradius = 10.0;
            string terrain_type = get_terrain_type3(gnid, ttradius);

            string[] p158 = { ttradius.ToString("F0") };
            text += "\n\n" + terrain_text(terrain_type, gnid) + addnote(mp(158, p158) + addref("vp", viewfinder_ref()) + " " + mp(200));

            //Dictionary<string, int> nbcount = new Dictionary<string, int>();
            //Dictionary<string, int> catcount = new Dictionary<string, int>();

            //double nnb = 0;
            long popmax = gndict[gnid].population;
            long pop3 = 3 * gndict[gnid].population;
            long totalpop = gndict[gnid].population;
            int npopmax = 0;
            int npopnear = 0;
            int nppl = 0;
            double popmindist = 9999.9;

            foreach (int nb in farlist)
            {
                if (!gndict.ContainsKey(nb))
                    continue;

                //if (catnormdict.ContainsKey(categorydict[gndict[nb].featurecode]))
                //{
                //    nnb += 1;

                //    if (!nbcount.ContainsKey(categorydict[gndict[nb].featurecode]))
                //        nbcount.Add(categorydict[gndict[nb].featurecode], 0);
                //    nbcount[categorydict[gndict[nb].featurecode]]++;
                //}

                if (gndict[nb].featureclass == 'P')
                {
                    nppl++;
                    totalpop += gndict[nb].population;

                    if (gndict[nb].population > popmax)
                    {
                        popmax = gndict[nb].population;
                        npopmax = nb;
                    }
                    if (gndict[nb].population > pop3)
                    {
                        double dist = get_distance(gnid, nb);
                        if (dist < popmindist)
                        {
                            popmindist = dist;
                            npopnear = nb;
                        }
                    }


                }


            }

            string[] p113 = { gndict[gnid].Name_ml };

            int nhalt = 0;
            int nearhigh = -1;
            double[] nhlatlong = get_nearhigh(gnid, farlist, nbradius, city_radius(gndict[gnid].population), out nearhigh, out nhalt);

            if (nearhigh > 0)
            {
                string[] p116 = { makegnidlink(nearhigh), fnum(gndict[nearhigh].elevation) };
                text += " " + mp(116, p116) + ", " + fnum(get_distance(gnid, nearhigh)) + " " + mp(308) + " " + mp(100 + get_direction(gnid, nearhigh)) + " " + gndict[gnid].Name_ml + "." + addnote(mp(137) + geonameref(gnid));
            }
            else if (gndict[gnid].elevation > nhalt)
            {
                text += " " + mp(165, p113) + ".";
            }
            else if (nhlatlong[0] + nhlatlong[1] < 720.0)
            {
                string[] p172 = { fnum(nhalt) };
                text += " " + mp(172, p172) + ", " + fnum(get_distance_latlong(gndict[gnid].latitude, gndict[gnid].longitude, nhlatlong[0], nhlatlong[1])) + " " + mp(308) + " " + mp(100 + get_direction_latlong(gndict[gnid].latitude, gndict[gnid].longitude, nhlatlong[0], nhlatlong[1])) + " " + gndict[gnid].Name_ml + "." + addnote(mp(140) + addref("vp", viewfinder_ref()) + " " + mp(200));

            }


            //double popdensity = totalpop / (3.14 * nbradius * nbradius);
            //if (popdensity < 10.0)
            //{
            //    text += " " + mp(117) + ".";
            //}
            //else if (popdensity > 100.0)
            //{
            //    text += " " + mp(118) + ".";
            //}
            //else if (popdensity > 400.0)
            //{
            //    text += " " + mp(119) + ".";
            //}

            text += make_popdensity(gnid);

            if (!nocapital.Contains(makecountry))
            {
                if (nppl == 0)
                {
                    if (getghostneighbors(gndict[gnid].latitude, gndict[gnid].longitude, 0.5 * nbradius))
                    {
                        text += " " + mp(115) + ".";
                    }
                }
                else if (npopmax > 0)
                {
                    int nbig = npopmax;
                    if (npopnear > 0)
                    {
                        if (gndict[nbig].population < 3 * gndict[npopnear].population)
                            nbig = npopnear;
                    }
                    string[] p114 = { makegnidlink(nbig) };
                    text += " " + mp(114, p114) + ", " + fnum(get_distance(gnid, nbig)) + " " + mp(308) + " " + mp(100 + get_direction(gnid, nbig)) + " " + gndict[gnid].Name_ml + ".";
                }
                else
                {
                    text += " " + mp(113, p113) + ".";
                }
            }

            text += make_landcover(gnid);

            text += get_overrep(gnid, 20.0);

            //public static double get_distance(int gnidfrom, int gnidto)
            //public static int get_direction(int gnidfrom, int gnidto)

            //public static Dictionary<string, string> categorydict = new Dictionary<string, string>(); //from featurecode to category
            //public static Dictionary<string, string> parentcategory = new Dictionary<string, string>(); //from category to parent category
            //public static Dictionary<string, string> categoryml = new Dictionary<string, string>(); //from category to category name in makelang

            return text;
        }

        public static string viewfinder_ref()
        {
            if (makelang == "sv")
                return "{{Webbref |url= {{Viewfinderlänk}}|titel= Viewfinder Panoramas Digital elevation Model|hämtdatum= 2015-06-21|format= |verk= }}";
            else
                return "{{Cite web |url= {{Viewfinderlink}}|title= Viewfinder Panoramas Digital elevation Model|date= 2015-06-21|format= }}";

        }

        public static string make_point(int gnid) //make any pointlike place, from mountains to oases 
        {
            string text = "";

            //List<int> nearlist = getneighbors(gnid, 10.0);
            double nbradius = 20.0;
            List<int> farlist = getneighbors(gnid, nbradius);

            if (!(categorydict[gndict[gnid].featurecode] == "seabed") && !(categorydict[gndict[gnid].featurecode] == "navigation") && !(categorydict[gndict[gnid].featurecode] == "bays") && !(categorydict[gndict[gnid].featurecode] == "reefs"))
            {
                double ttradius = 10.0;
                string terrain_type = get_terrain_type3(gnid, ttradius);

                string[] p158 = { ttradius.ToString("F0") };
                text += "\n\n" + terrain_text(terrain_type, gnid) + addnote(mp(158, p158) + addref("vp", viewfinder_ref()) + " " + mp(200));
            }
            //Dictionary<string, int> nbcount = new Dictionary<string, int>();
            //Dictionary<string, int> catcount = new Dictionary<string, int>();

            //double nnb = 0;
            long popmax = 0;
            long pop3 = 3000;
            long totalpop = 0;
            int npopmax = 0;
            int npopnear = 0;
            int nppl = 0;
            int nbpop = -1;
            double popmindist = 9999.9;

            foreach (int nb in farlist)
            {
                if (!gndict.ContainsKey(nb))
                    continue;

                //if (catnormdict.ContainsKey(categorydict[gndict[nb].featurecode]))
                //{
                //    nnb += 1;

                //    if (!nbcount.ContainsKey(categorydict[gndict[nb].featurecode]))
                //        nbcount.Add(categorydict[gndict[nb].featurecode], 0);
                //    nbcount[categorydict[gndict[nb].featurecode]]++;
                //}


                if ((gndict[nb].featureclass == 'P') || (gndict[nb].featurecode == "STNB"))
                {
                    nppl++;
                    nbpop = nb;
                    totalpop += gndict[nb].population;

                    if (gndict[nb].population > popmax)
                    {
                        popmax = gndict[nb].population;
                        npopmax = nb;
                    }
                    if (gndict[nb].population > pop3)
                    {
                        double dist = get_distance(gnid, nb);
                        if (dist < popmindist)
                        {
                            popmindist = dist;
                            npopnear = nb;
                        }
                    }


                }



            }

            string[] p113 = { gndict[gnid].Name_ml };

            int nhalt = 0;
            int nearhigh = -1;
            double[] nhlatlong = get_nearhigh(gnid, farlist, nbradius, 1.0, out nearhigh, out nhalt);

            if (nearhigh > 0)
            {
                string[] p116 = { makegnidlink(nearhigh), fnum(gndict[nearhigh].elevation) };
                text += " " + mp(116, p116) + ", " + fnum(get_distance(gnid, nearhigh)) + " " + mp(308) + " " + mp(100 + get_direction(gnid, nearhigh)) + " " + gndict[gnid].Name_ml + "." + addnote(mp(137) + geonameref(gnid));
            }
            else if (gndict[gnid].elevation > nhalt)
            {
                text += " " + mp(165, p113) + ".";
            }
            else if (nhlatlong[0] + nhlatlong[1] < 720.0)
            {
                string[] p172 = { fnum(nhalt) };
                text += " " + mp(172, p172) + ", " + fnum(get_distance_latlong(gndict[gnid].latitude, gndict[gnid].longitude, nhlatlong[0], nhlatlong[1])) + " " + mp(308) + " " + mp(100 + get_direction_latlong(gndict[gnid].latitude, gndict[gnid].longitude, nhlatlong[0], nhlatlong[1])) + " " + gndict[gnid].Name_ml + "." + addnote(mp(140) + addref("vp", viewfinder_ref()) + " " + mp(200));
                Console.WriteLine("nhlatlong = " + nhlatlong[0].ToString() + "   " + nhlatlong[1].ToString());
            }


            //double popdensity = totalpop / (3.14 * nbradius * nbradius);
            //if (popdensity < 10.0)
            //{
            //    text += " " + mp(117) + ".";
            //}
            //else if (popdensity > 100.0)
            //{
            //    text += " " + mp(118) + ".";
            //}
            //else if (popdensity > 400.0)
            //{
            //    text += " " + mp(119) + ".";
            //}

            text += make_popdensity(gnid);

            if (nppl == 0)
            {
                if (!getghostneighbors(gndict[gnid].latitude, gndict[gnid].longitude, 0.5 * nbradius))
                {
                    text += " " + mp(169) + ".";
                }
            }
            else if (npopmax > 0)
            {
                int nbig = npopmax;
                if (npopnear > 0)
                {
                    if (gndict[nbig].population < 3 * gndict[npopnear].population)
                        nbig = npopnear;
                }
                string[] p114 = { makegnidlink(nbig) };
                text += " " + mp(114, p114) + ", " + fnum(get_distance(gnid, nbig)) + " " + mp(308) + " " + mp(100 + get_direction(gnid, nbig)) + " " + gndict[gnid].Name_ml + ".";
            }
            else if (nbpop > 0)
            {
                string[] p212 = { makegnidlink(nbpop) };
                text += " " + mp(212, p212) + ", " + fnum(get_distance(gnid, nbpop)) + " " + mp(308) + " " + mp(100 + get_direction(gnid, nbpop)) + " " + gndict[gnid].Name_ml + ".";
            }
            else
            {
                Console.WriteLine("Should never come here. make_point <ret>");
                Console.ReadLine();
            }

            text += make_landcover(gnid);

            int imp = imp_mountainpart(gndict[gnid].featurecode);
            if (imp > 0)
            {
                int mtgnid = attach_to_mountain(gnid);
                if (gndict.ContainsKey(mtgnid))
                {
                    text += " " + gndict[gnid].Name_ml + " " + mp(imp) + " " + makegnidlink(mtgnid) + "." + addnote(mp(140) + addref("vp", viewfinder_ref()) + " " + mp(200));
                }
            }

            if (categorydict[gndict[gnid].featurecode] == "passes")
            {
                int mtgnid1 = -1;
                int mtgnid2 = -1;
                if (between_mountains(gnid, out mtgnid1, out mtgnid2))
                {
                    string[] p199 = { makegnidlink(mtgnid1), makegnidlink(mtgnid2) };
                    text += " " + mp(199, p199) + "." + addnote(mp(140) + addref("vp", viewfinder_ref()) + " " + mp(200));
                }
            }

            text += get_overrep(gnid, 20.0);



            //public static double get_distance(int gnidfrom, int gnidto)
            //public static int get_direction(int gnidfrom, int gnidto)

            //public static Dictionary<string, string> categorydict = new Dictionary<string, string>(); //from featurecode to category
            //public static Dictionary<string, string> parentcategory = new Dictionary<string, string>(); //from category to parent category
            //public static Dictionary<string, string> categoryml = new Dictionary<string, string>(); //from category to category name in makelang

            return text;
        }

        public static string make_channel(int gnid) //make any pointlike place, from mountains to oases 
        {
            string text = "";

            //List<int> nearlist = getneighbors(gnid, 10.0);
            double nbradius = 20.0;
            List<int> farlist = getneighbors(gnid, nbradius);

            //double nnb = 0;
            long popmax = 0;
            long pop3 = 3000;
            long totalpop = 0;
            int npopmax = 0;
            int npopnear = 0;
            int nppl = 0;
            int nbpop = -1;
            double popmindist = 9999.9;

            foreach (int nb in farlist)
            {
                if (!gndict.ContainsKey(nb))
                    continue;

                if ((gndict[nb].featureclass == 'P') || (gndict[nb].featurecode == "STNB"))
                {
                    nppl++;
                    nbpop = nb;
                    totalpop += gndict[nb].population;

                    if (gndict[nb].population > popmax)
                    {
                        popmax = gndict[nb].population;
                        npopmax = nb;
                    }
                    if (gndict[nb].population > pop3)
                    {
                        double dist = get_distance(gnid, nb);
                        if (dist < popmindist)
                        {
                            popmindist = dist;
                            npopnear = nb;
                        }
                    }
                }
            }

            string[] p113 = { gndict[gnid].Name_ml };

            int nhalt = 0;
            int nearhigh = -1;
            double[] nhlatlong = get_nearhigh(gnid, farlist, nbradius, 1.0, out nearhigh, out nhalt);

            if (nearhigh > 0)
            {
                string[] p116 = { makegnidlink(nearhigh), fnum(gndict[nearhigh].elevation) };
                text += " " + mp(116, p116) + ", " + fnum(get_distance(gnid, nearhigh)) + " " + mp(308) + " " + mp(100 + get_direction(gnid, nearhigh)) + " " + gndict[gnid].Name_ml + "." + addnote(mp(137) + geonameref(gnid));
            }
            else if (gndict[gnid].elevation > nhalt)
            {
                text += " " + mp(165, p113) + ".";
            }
            else if (nhlatlong[0] + nhlatlong[1] < 720.0)
            {
                string[] p172 = { fnum(nhalt) };
                text += " " + mp(172, p172) + ", " + fnum(get_distance_latlong(gndict[gnid].latitude, gndict[gnid].longitude, nhlatlong[0], nhlatlong[1])) + " " + mp(308) + " " + mp(100 + get_direction_latlong(gndict[gnid].latitude, gndict[gnid].longitude, nhlatlong[0], nhlatlong[1])) + " " + gndict[gnid].Name_ml + "." + addnote(mp(140) + addref("vp", viewfinder_ref()) + " " + mp(200));
                Console.WriteLine("nhlatlong = " + nhlatlong[0].ToString() + "   " + nhlatlong[1].ToString());
            }

            text += make_popdensity(gnid);

            if (nppl == 0)
            {
                if (!getghostneighbors(gndict[gnid].latitude, gndict[gnid].longitude, 0.5 * nbradius))
                {
                    text += " " + mp(169) + ".";
                }
            }
            else if (npopmax > 0)
            {
                int nbig = npopmax;
                if (npopnear > 0)
                {
                    if (gndict[nbig].population < 3 * gndict[npopnear].population)
                        nbig = npopnear;
                }
                string[] p114 = { makegnidlink(nbig) };
                text += " " + mp(114, p114) + ", " + fnum(get_distance(gnid, nbig)) + " " + mp(308) + " " + mp(100 + get_direction(gnid, nbig)) + " " + gndict[gnid].Name_ml + ".";
            }
            else if (nbpop > 0)
            {
                string[] p212 = { makegnidlink(nbpop) };
                text += " " + mp(212, p212) + ", " + fnum(get_distance(gnid, nbpop)) + " " + mp(308) + " " + mp(100 + get_direction(gnid, nbpop)) + " " + gndict[gnid].Name_ml + ".";
            }
            else
            {
                Console.WriteLine("Should never come here. make_channel <ret>");
                Console.ReadLine();
            }

            text += make_landcover(gnid);

            text += get_overrep(gnid, 20.0);

            return text;
        }

        public static string make_adm(int gnid)
        {
            string text = "";
            Dictionary<char, int> fc = new Dictionary<char, int>();
            fc.Add('H', 0);
            fc.Add('L', 0);
            fc.Add('A', 0);
            fc.Add('P', 0);
            fc.Add('S', 0);
            fc.Add('T', 0);
            fc.Add('U', 0);
            fc.Add('V', 0);

            string[] p79 = new string[1] { gndict[gnid].Name_ml };

            //bordering:

            if (wdid > 0)
            {
                List<int> neighbors = get_wd_prop_idlist(propdict["borders"], currentxml);

                if (neighbors.Count > 1)
                {
                    text += " " + mp(96, p79);
                    int i = 0;
                    foreach (int wdnb in neighbors)
                    {
                        i++;
                        if (i == neighbors.Count)
                            text += mp(97);
                        else if (i > 1)
                            text += ",";
                        text += " " + get_name_from_wdid(wdnb);
                    }
                    text += ". ";
                }
                else if (neighbors.Count == 1)
                {
                    text += " " + mp(96, p79);
                    text += " " + get_name_from_wdid(neighbors[0]);
                    text += ".";
                }
            }

            //terrain type

            if (gndict[gnid].area > 0)
            {
                double nbradius = Math.Sqrt(gndict[gnid].area);

                string terrain_type = get_terrain_type3(gnid, nbradius);

                //string[] p136 = { nbradius.ToString("F0") };
                text += "\n\n" + terrain_text(terrain_type, gnid);
                //if ( makelang == "sv" )
                text += addnote(mp(140) + addref("vp", viewfinder_ref()) + " " + mp(200));

            }



            //administrative subdivisions:

            if (gndict[gnid].children.Count > 1)
            {
                Console.WriteLine("Subdivisions from gnid-kids");
                text += "\n\n" + mp(79, p79) + "\n";
                foreach (int kid in gndict[gnid].children)
                    text += "* " + makegnidlink(kid) + "\n";
            }
            else if (wdid > 0)
            {

                List<int> kidlist = get_wd_kids(currentxml);
                Console.WriteLine("Subdivisions from wikidata-kids, " + kidlist.Count.ToString());
                if (kidlist.Count > 0)
                {
                    text += "\n\n" + mp(79, p79) + "\n";
                    foreach (int kid in kidlist)
                    {
                        text += "* " + get_name_from_wdid(kid) + "\n";
                    }
                }
            }



            //feature lists:
            if (gndict[gnid].features.Count > 0)
            {
                foreach (int ff in gndict[gnid].features)
                {
                    fc[gndict[ff].featureclass]++;
                }
                // H: vatten
                // L: diverse människoskapade områden 
                // P: samhällen
                // S: Byggnadsverk
                // T: diverse naturfenomen; berg, dalar, öar...
                // U: undervattensfenomen
                // V: växtlighet; skogar, hedar...

                SortedDictionary<long, int> flist = new SortedDictionary<long, int>();

                if (fc['P'] > 0)
                {
                    text += "\n\n" + mp(80, p79) + "\n";
                    foreach (int ff in gndict[gnid].features)
                        if (gndict[ff].featureclass == 'P')
                        {
                            long pop = gndict[ff].population;
                            while (flist.ContainsKey(pop))
                                pop++;
                            flist.Add(pop, ff);
                        }
                    string sorted = "";
                    foreach (int fpop in flist.Keys)
                    {
                        //sorted = "\n* " + makegnidlink(flist[fpop]) + " (" + fpop.ToString("N0",nfi) + " " + mp(81) + ")" + sorted; //With pop
                        sorted = "\n* " + makegnidlink(flist[fpop]) + sorted; //Without pop
                    }
                    text += sorted;
                }

                SortedDictionary<string, int> fl2 = new SortedDictionary<string, int>();

                if (fc['H'] + fc['T'] + fc['U'] + fc['V'] > 10)
                {
                    //public static Dictionary<string, string> categorydict = new Dictionary<string, string>(); //from featurecode to category
                    //public static Dictionary<string, string> parentcategory = new Dictionary<string, string>(); //from category to parent category
                    //public static Dictionary<string, string> categoryml = new Dictionary<string, string>(); //from category to category name in makelang

                    text += "\n\n" + mp(91, p79) + "\n";

                    foreach (string cat in categoryml.Keys)
                    {
                        fl2.Clear();
                        foreach (int ff in gndict[gnid].features)
                            if ((gndict[ff].featureclass == 'H') || (gndict[ff].featureclass == 'T') || (gndict[ff].featureclass == 'U') || (gndict[ff].featureclass == 'V'))
                            {
                                if (categorydict[gndict[ff].featurecode] == cat)
                                {
                                    string ffname = gndict[ff].Name_ml;
                                    while (fl2.ContainsKey(ffname))
                                        ffname += " ";
                                    fl2.Add(ffname, ff);
                                }
                            }

                        if (fl2.Count > 0)
                        {
                            text += "\n\n* " + initialcap(categoryml[cat]) + ":\n";
                            string sorted = "";
                            foreach (string fname in fl2.Keys)
                            {
                                sorted += "\n:* " + makegnidlink(fl2[fname]) + " (" + linkfeature(gndict[fl2[fname]].featurecode, fl2[fname]) + ")";
                            }
                            text += sorted;
                        }
                    }

                }
                else if (fc['H'] + fc['T'] + fc['U'] + fc['V'] > 0)
                {
                    fl2.Clear();

                    text += "\n\n" + mp(91, p79) + "\n";
                    foreach (int ff in gndict[gnid].features)
                        if ((gndict[ff].featureclass == 'H') || (gndict[ff].featureclass == 'T') || (gndict[ff].featureclass == 'U') || (gndict[ff].featureclass == 'V'))
                        {
                            string ffname = gndict[ff].Name_ml;
                            while (fl2.ContainsKey(ffname))
                                ffname += " ";
                            fl2.Add(ffname, ff);
                        }
                    string sorted = "";
                    foreach (string fname in fl2.Keys)
                    {
                        sorted += "\n* " + makegnidlink(fl2[fname]) + " (" + linkfeature(gndict[fl2[fname]].featurecode, fl2[fname]) + ")";
                    }
                    text += sorted;
                }



            }

            return text;

        }


        public static string make_island(int gnid, Page p)
        {
            string text = "";
            Dictionary<char, int> fc = new Dictionary<char, int>();
            fc.Add('H', 0);
            fc.Add('L', 0);
            fc.Add('A', 0);
            fc.Add('P', 0);
            fc.Add('S', 0);
            fc.Add('T', 0);
            fc.Add('U', 0);
            fc.Add('V', 0);

            string[] p79 = new string[1] { gndict[gnid].Name_ml };


            //terrain type

            if (gndict[gnid].area > 0)
            {
                //double nbradius = Math.Sqrt(gndict[gnid].area);

                string terrain_type = get_terrain_type_island(gnid);

                //string[] p136 = { nbradius.ToString("F0") };
                string tt = terrain_text(terrain_type, gnid);
                if (!String.IsNullOrEmpty(tt))
                {
                    text += "\n\n" + tt;
                    //if (makelang == "sv")
                    text += addnote(mp(140) + addref("vp", viewfinder_ref()) + " " + mp(200));
                }

                string heightmarker = "|elevation_max";
                if (terrain_type.Contains(heightmarker))
                {
                    string elmax = terrain_type.Substring(terrain_type.IndexOf(heightmarker) + heightmarker.Length + 1);
                    int maxheight = tryconvert(elmax);
                    if (maxheight > gndict[gnid].elevation)
                    {
                        string[] p163 = { fnum(maxheight) };
                        text += " " + mp(163, p163);
                        p.SetTemplateParameter("geobox", "highest_elevation", elmax, true);
                    }

                }

                if ((islanddict.ContainsKey(gnid)) && ((islanddict[gnid].kmns + islanddict[gnid].kmew) > 1.0))
                {
                    string[] p164 = { islanddict[gnid].kmns.ToString("F1", culture), islanddict[gnid].kmew.ToString("F1", culture) };
                    text += " " + mp(164, p164);
                    //if (makelang == "sv")
                    text += addnote(mp(140) + addref("vp", viewfinder_ref()) + " " + mp(200));

                    if (islanddict[gnid].kmew > 2 * islanddict[gnid].kmns)
                    {
                        p.SetTemplateParameter("geobox", "length", islanddict[gnid].kmew.ToString(), true);
                        p.SetTemplateParameter("geobox", "width", islanddict[gnid].kmns.ToString(), true);
                        p.SetTemplateParameter("geobox", "length_orientation", "EW", true);
                        p.SetTemplateParameter("geobox", "width_orientation", "NS", true);
                    }
                    else if (islanddict[gnid].kmns > 2 * islanddict[gnid].kmew)
                    {
                        p.SetTemplateParameter("geobox", "length", islanddict[gnid].kmns.ToString(), true);
                        p.SetTemplateParameter("geobox", "width", islanddict[gnid].kmew.ToString(), true);
                        p.SetTemplateParameter("geobox", "length_orientation", "NS", true);
                        p.SetTemplateParameter("geobox", "width_orientation", "EW", true);
                    }
                }

                if (gndict[gnid].area < 300)
                    text += " " + make_landcover(gnid);
            }




            //feature lists:
            if (islanddict.ContainsKey(gnid))
                if (islanddict[gnid].onisland.Count > 0)
                {
                    foreach (int ff in islanddict[gnid].onisland)
                    {
                        if (!categorydict.ContainsKey(gndict[ff].featurecode))
                        {
                            Console.WriteLine(gndict[ff].featurecode + " missing in categorydict");
                            Console.ReadLine();
                            continue;
                        }
                        if (categorydict[gndict[ff].featurecode] != "islands") //Not island on island
                            fc[gndict[ff].featureclass]++;
                    }
                    // H: vatten
                    // L: diverse människoskapade områden 
                    // P: samhällen
                    // S: Byggnadsverk
                    // T: diverse naturfenomen; berg, dalar, öar...
                    // U: undervattensfenomen
                    // V: växtlighet; skogar, hedar...

                    SortedDictionary<long, int> flist = new SortedDictionary<long, int>();

                    if (fc['P'] > 0)
                    {
                        text += "\n\n" + mp(159, p79) + "\n";
                        foreach (int ff in islanddict[gnid].onisland)
                            if (gndict[ff].featureclass == 'P')
                            {
                                long pop = gndict[ff].population;
                                while (flist.ContainsKey(pop))
                                    pop++;
                                flist.Add(pop, ff);
                            }
                        string sorted = "";
                        foreach (int fpop in flist.Keys)
                        {
                            //sorted = "\n* " + makegnidlink(flist[fpop]) + " (" + fpop.ToString("N0",nfi) + " " + mp(81) + ")" + sorted;
                            sorted = "\n* " + makegnidlink(flist[fpop]) + sorted;
                        }
                        text += sorted;
                    }

                    SortedDictionary<string, int> fl2 = new SortedDictionary<string, int>();

                    if (fc['H'] + fc['T'] + fc['U'] + fc['V'] > 10)
                    {
                        //public static Dictionary<string, string> categorydict = new Dictionary<string, string>(); //from featurecode to category
                        //public static Dictionary<string, string> parentcategory = new Dictionary<string, string>(); //from category to parent category
                        //public static Dictionary<string, string> categoryml = new Dictionary<string, string>(); //from category to category name in makelang

                        text += "\n\n" + mp(160, p79) + "\n";

                        foreach (string cat in categoryml.Keys)
                        {
                            if (cat == "islands") //not island on island
                                continue;

                            fl2.Clear();
                            foreach (int ff in islanddict[gnid].onisland)
                                if ((gndict[ff].featureclass == 'H') || (gndict[ff].featureclass == 'T') || (gndict[ff].featureclass == 'U') || (gndict[ff].featureclass == 'V'))
                                {
                                    if (categorydict[gndict[ff].featurecode] == cat)
                                    {
                                        string ffname = gndict[ff].Name_ml;
                                        while (fl2.ContainsKey(ffname))
                                            ffname += " ";
                                        fl2.Add(ffname, ff);
                                    }
                                }

                            if (fl2.Count > 0)
                            {
                                text += "\n* " + initialcap(categoryml[cat]) + ":\n";
                                string sorted = "";
                                foreach (string fname in fl2.Keys)
                                {
                                    sorted += "\n** " + makegnidlink(fl2[fname]) + " (" + linkfeature(gndict[fl2[fname]].featurecode, fl2[fname]) + ")";
                                }
                                text += sorted;
                            }
                        }

                    }
                    else if (fc['H'] + fc['T'] + fc['U'] + fc['V'] > 0)
                    {
                        fl2.Clear();

                        foreach (int ff in islanddict[gnid].onisland)
                            if ((gndict[ff].featureclass == 'H') || (gndict[ff].featureclass == 'T') || (gndict[ff].featureclass == 'U') || (gndict[ff].featureclass == 'V'))
                            {
                                if (categorydict[gndict[ff].featurecode] != "islands") //Not island on island
                                    continue;
                                string ffname = gndict[ff].Name_ml;
                                while (fl2.ContainsKey(ffname))
                                    ffname += " ";
                                fl2.Add(ffname, ff);
                            }

                        if (fl2.Count > 0)
                        {
                            text += "\n\n" + mp(160, p79) + "\n";
                            string sorted = "";
                            foreach (string fname in fl2.Keys)
                            {
                                sorted += "\n* " + makegnidlink(fl2[fname]) + " (" + linkfeature(gndict[fl2[fname]].featurecode, fl2[fname]) + ")";
                            }
                            text += sorted;
                        }
                    }



                }

            return text;

        }


        public static string make_range(int gnid, Page p)
        {
            string text = "";
            Dictionary<char, int> fc = new Dictionary<char, int>();
            fc.Add('H', 0);
            fc.Add('L', 0);
            fc.Add('A', 0);
            fc.Add('P', 0);
            fc.Add('S', 0);
            fc.Add('T', 0);
            fc.Add('U', 0);
            fc.Add('V', 0);

            if (rangedict.ContainsKey(gnid))
            {
                string[] p207 = new string[3] { gndict[gnid].Name_ml, fnum(rangedict[gnid].length), get_nsew(rangedict[gnid].angle) };

                text += "\n\n" + mp(207, p207) + addnote(mp(140) + addref("vp", viewfinder_ref()) + " " + mp(200));
                if (rangedict[gnid].maxheight > 0)
                {
                    string[] p208 = new string[1] { fnum(rangedict[gnid].maxheight) };
                    text += " " + mp(208, p208);
                }
                else if (gndict.ContainsKey(-rangedict[gnid].maxheight))
                {
                    int hgnid = -rangedict[gnid].maxheight;
                    string[] p209 = new string[2] { makegnidlink(hgnid), fnum(gndict[hgnid].elevation) };
                    text += " " + mp(209, p209);
                }

                string[] p79 = new string[1] { gndict[gnid].Name_ml };

                //feature lists:
                if (rangedict[gnid].inrange.Count > 0)
                {
                    foreach (int ff in rangedict[gnid].inrange)
                    {
                        fc[gndict[ff].featureclass]++;
                    }
                    // H: vatten
                    // L: diverse människoskapade områden 
                    // P: samhällen
                    // S: Byggnadsverk
                    // T: diverse naturfenomen; berg, dalar, öar...
                    // U: undervattensfenomen
                    // V: växtlighet; skogar, hedar...

                    SortedDictionary<string, int> fl2 = new SortedDictionary<string, int>();

                    if (fc['H'] + fc['T'] + fc['U'] + fc['V'] > 0)
                    {

                        text += "\n\n" + mp(206, p79) + "\n";
                        foreach (int ff in rangedict[gnid].inrange)
                            if ((gndict[ff].featureclass == 'H') || (gndict[ff].featureclass == 'T') || (gndict[ff].featureclass == 'U') || (gndict[ff].featureclass == 'V'))
                            {
                                string ffname = gndict[ff].Name_ml;
                                while (fl2.ContainsKey(ffname))
                                    ffname += " ";
                                fl2.Add(ffname, ff);
                            }
                        string sorted = "";
                        foreach (string fname in fl2.Keys)
                        {
                            sorted += "\n* " + makegnidlink(fl2[fname]);
                        }
                        text += sorted;
                    }



                }
            }
            return text;

        }

        public static string make_lake(int gnid, Page p)
        {
            string text = "";
            Dictionary<char, int> fc = new Dictionary<char, int>();
            fc.Add('H', 0);
            fc.Add('L', 0);
            fc.Add('A', 0);
            fc.Add('P', 0);
            fc.Add('S', 0);
            fc.Add('T', 0);
            fc.Add('U', 0);
            fc.Add('V', 0);

            if (lakedict.ContainsKey(gnid))
            {
                double lakesize = lakedict[gnid].kmns + lakedict[gnid].kmew;
                double nbradius = 30.0;
                if ((lakesize > 0) && (lakesize < 0.5 * nbradius))
                {
                    int nhalt = 0;
                    int nearhigh = -1;
                    List<int> farlist = getneighbors(gnid, nbradius);

                    double[] nhlatlong = get_nearhigh(gnid, farlist, nbradius, lakesize, out nearhigh, out nhalt);

                    if (nearhigh > 0)
                    {
                        string[] p116 = { makegnidlink(nearhigh), fnum(gndict[nearhigh].elevation) };
                        text += " " + mp(116, p116) + ", " + fnum(get_distance(gnid, nearhigh)) + " " + mp(308) + " " + mp(100 + get_direction(gnid, nearhigh)) + " " + gndict[gnid].Name_ml + "." + addnote(mp(137) + geonameref(gnid));
                    }
                    else if (nhlatlong[0] + nhlatlong[1] < 720.0)
                    {
                        string[] p172 = { fnum(nhalt) };
                        text += " " + mp(172, p172) + ", " + fnum(get_distance_latlong(gndict[gnid].latitude, gndict[gnid].longitude, nhlatlong[0], nhlatlong[1])) + " " + mp(308) + " " + mp(100 + get_direction_latlong(gndict[gnid].latitude, gndict[gnid].longitude, nhlatlong[0], nhlatlong[1])) + " " + gndict[gnid].Name_ml + "." + addnote(mp(140) + addref("vp", viewfinder_ref()) + " " + mp(200));

                    }
                }

                text += make_landcover(gnid);

                string[] p79 = new string[1] { gndict[gnid].Name_ml };

                if (lakedict[gnid].kmns + lakedict[gnid].kmew > 1.0)
                {
                    string[] p164 = { lakedict[gnid].kmns.ToString("F1", culture), lakedict[gnid].kmew.ToString("F1", culture) };
                    text += " " + mp(164, p164);

                    if (lakedict[gnid].kmew > 2 * lakedict[gnid].kmns)
                    {
                        p.SetTemplateParameter("geobox", "length", lakedict[gnid].kmew.ToString(), true);
                        p.SetTemplateParameter("geobox", "width", lakedict[gnid].kmns.ToString(), true);
                        p.SetTemplateParameter("geobox", "length_orientation", "EW", true);
                        p.SetTemplateParameter("geobox", "width_orientation", "NS", true);
                    }
                    else if (lakedict[gnid].kmns > 2 * lakedict[gnid].kmew)
                    {
                        p.SetTemplateParameter("geobox", "length", lakedict[gnid].kmns.ToString(), true);
                        p.SetTemplateParameter("geobox", "width", lakedict[gnid].kmew.ToString(), true);
                        p.SetTemplateParameter("geobox", "length_orientation", "NS", true);
                        p.SetTemplateParameter("geobox", "width_orientation", "EW", true);
                    }
                }

                //feature lists:
                if (lakedict[gnid].inlake.Count > 0)
                {
                    text += "\n\n" + mp(91, p79) + "\n";
                    foreach (int il in lakedict[gnid].inlake)
                        text += "\n* " + makegnidlink(il) + " (" + linkfeature(gndict[il].featurecode, il) + ")";
                    text += "\n\n";

                }
                if (lakedict[gnid].atlake.Count > 0)
                {
                    foreach (int ff in lakedict[gnid].atlake)
                    {
                        fc[gndict[ff].featureclass]++;
                    }
                    // H: vatten
                    // L: diverse människoskapade områden 
                    // P: samhällen
                    // S: Byggnadsverk
                    // T: diverse naturfenomen; berg, dalar, öar...
                    // U: undervattensfenomen
                    // V: växtlighet; skogar, hedar...

                    SortedDictionary<long, int> flist = new SortedDictionary<long, int>();

                    if (fc['P'] > 0)
                    {
                        text += "\n\n" + mp(161, p79) + "\n";
                        foreach (int ff in lakedict[gnid].atlake)
                            if (gndict[ff].featureclass == 'P')
                            {
                                long pop = gndict[ff].population;
                                while (flist.ContainsKey(pop))
                                    pop++;
                                flist.Add(pop, ff);
                            }
                        string sorted = "";
                        foreach (int fpop in flist.Keys)
                        {
                            sorted = "\n* " + makegnidlink(flist[fpop]) + " (" + fpop.ToString("N0", nfi) + " " + mp(81) + ")" + sorted;
                        }
                        text += sorted;
                    }

                    SortedDictionary<string, int> fl2 = new SortedDictionary<string, int>();

                    if (fc['H'] + fc['T'] + fc['U'] + fc['V'] > 10)
                    {
                        //public static Dictionary<string, string> categorydict = new Dictionary<string, string>(); //from featurecode to category
                        //public static Dictionary<string, string> parentcategory = new Dictionary<string, string>(); //from category to parent category
                        //public static Dictionary<string, string> categoryml = new Dictionary<string, string>(); //from category to category name in makelang

                        text += "\n\n" + mp(162, p79) + "\n";

                        foreach (string cat in categoryml.Keys)
                        {
                            fl2.Clear();
                            foreach (int ff in lakedict[gnid].atlake)
                                if ((gndict[ff].featureclass == 'H') || (gndict[ff].featureclass == 'T') || (gndict[ff].featureclass == 'U') || (gndict[ff].featureclass == 'V'))
                                {
                                    if (categorydict[gndict[ff].featurecode] == cat)
                                    {
                                        string ffname = gndict[ff].Name_ml;
                                        while (fl2.ContainsKey(ffname))
                                            ffname += " ";
                                        fl2.Add(ffname, ff);
                                    }
                                }

                            if (fl2.Count > 0)
                            {
                                text += "\n* " + initialcap(categoryml[cat]) + ":\n";
                                string sorted = "";
                                foreach (string fname in fl2.Keys)
                                {
                                    sorted += "\n** " + makegnidlink(fl2[fname]) + " (" + linkfeature(gndict[fl2[fname]].featurecode, fl2[fname]) + ")";
                                }
                                text += sorted;
                            }
                        }

                    }
                    else if (fc['H'] + fc['T'] + fc['U'] + fc['V'] > 0)
                    {
                        fl2.Clear();

                        text += "\n\n" + mp(168, p79) + "\n";
                        foreach (int ff in lakedict[gnid].atlake)
                            if ((gndict[ff].featureclass == 'H') || (gndict[ff].featureclass == 'T') || (gndict[ff].featureclass == 'U') || (gndict[ff].featureclass == 'V'))
                            {
                                string ffname = gndict[ff].Name_ml;
                                while (fl2.ContainsKey(ffname))
                                    ffname += " ";
                                fl2.Add(ffname, ff);
                            }
                        string sorted = "";
                        foreach (string fname in fl2.Keys)
                        {
                            sorted += "\n* " + makegnidlink(fl2[fname]) + " (" + linkfeature(gndict[fl2[fname]].featurecode, fl2[fname]) + ")";
                        }
                        text += sorted;
                    }



                }
            }

            return text;

        }


        public static string geonameref(int gnid)
        {

            //string gn = "[http://www.geonames.org/gnidgnid/asciiascii.html " + gndict[gnid].Name + "] at [http://www.geonames.org/about.html Geonames.Org (cc-by)]; updated "+gndict[gnid].moddate;
            string gn = "[{{" + mp(173) + "|gnid=" + gnid.ToString() + "|name=" + gndict[gnid].asciiname.ToLower().Replace(" ", "%20").Replace("\"", "%22") + "}} " + gndict[gnid].Name + "] " + mp(293) + " [{{Geonamesabout}} Geonames.org (cc-by)]; post " + mp(179) + " " + gndict[gnid].moddate + "; " + mp(180) + " " + dumpdate;
            //gn = gn.Replace("gnidgnid", gnid.ToString());
            //gn = gn.Replace("asciiascii", gndict[gnid].asciiname.ToLower().Replace(" ", "%20"));
            return addref("gn" + gnid.ToString(), gn);

        }

        public static string chinapopref()
        {

            string gn = "";
            string url = "http://pan.baidu.com/share/link?uk=2922733136&shareid=2553664090&third=0&adapt=pc&fr=ftw#path=%252F%25E3%2580%258A%25E4%25B8%25AD%25E5%259B%25BD2010%25E5%25B9%25B4%25E4%25BA%25BA%25E5%258F%25A3%25E6%2599%25AE%25E6%259F%25A5%25E5%2588%2586%25E4%25B9%25A1%25E3%2580%2581%25E9%2595%2587%25E3%2580%2581%25E8%25A1%2597%25E9%2581%2593%25E8%25B5%2584%25E6%2596%2599%25E3%2580%258Bxls";
            if (makelang == "sv")
                gn = "{{webbref |url= " + url + "|titel= 中国2010年人口普查分乡、镇、街道资料》xls (Folkräkning 2010 Kina)|hämtdatum=23 april 2016 |efternamn= |förnamn= |datum= |verk= |utgivare= Baidu.com}}";
            else
                gn = "{{Cite web |url= " + url + "|title= 中国2010年人口普查分乡、镇、街道资料》xls (China 2010 census)|access-date = 23 Abril 2016 |publisher= Baidu.com}}";
            //{{Webbref |url= |titel= |hämtdatum= |författare= |efternamn= |förnamn= |författarlänk= |efternamn2= |förnamn2= |datum= |år= |månad= |format= |verk= |utgivare= |sid= |språk= |doi= |arkivurl= |arkivdatum= |citat= |ref= }}
            return addref("chinapop", gn);
        }


        public static string nasaref()
        {

            string gn = "";
            if (makelang == "sv")
                gn = "{{webbref |url= http://neo.sci.gsfc.nasa.gov/dataset_index.php|titel= NASA Earth Observations Data Set Index|hämtdatum=30 januari 2016 |efternamn= |förnamn= |datum= |verk= |utgivare= NASA}}";
            else
                gn = "{{Cite web |url= http://neo.sci.gsfc.nasa.gov/dataset_index.php|title= NASA Earth Observations Data Set Index|access-date = 30 Enero 2016 |publisher= NASA}}";
            //{{Webbref |url= |titel= |hämtdatum= |författare= |efternamn= |förnamn= |författarlänk= |efternamn2= |förnamn2= |datum= |år= |månad= |format= |verk= |utgivare= |sid= |språk= |doi= |arkivurl= |arkivdatum= |citat= |ref= }}
            return addref("nasa", gn);
        }

        public static string nasapopref()
        {

            string gn = "";
            if (makelang == "sv")
                gn = "{{webbref |url= http://neo.sci.gsfc.nasa.gov/view.php?datasetId=SEDAC_POP|titel= NASA Earth Observations: Population Density|hämtdatum=30 januari 2016 |efternamn= |förnamn= |datum= |verk= |utgivare= NASA/SEDAC}}";
            else
                gn = "{{Cite web |url= http://neo.sci.gsfc.nasa.gov/view.php?datasetId=SEDAC_POP|title= NASA Earth Observations: Population Density|access-date = 30 Enero 2016 |publisher= NASA/SEDAC}}";
            //{{Webbref |url= |titel= |hämtdatum= |författare= |efternamn= |förnamn= |författarlänk= |efternamn2= |förnamn2= |datum= |år= |månad= |format= |verk= |utgivare= |sid= |språk= |doi= |arkivurl= |arkivdatum= |citat= |ref= }}
            return addref("nasapop", gn);
        }

        public static string nasarainref()
        {

            string gn = "";
            if (makelang == "sv")
                gn = "{{webbref |url= http://neo.sci.gsfc.nasa.gov/view.php?datasetId=TRMM_3B43M|titel= NASA Earth Observations: Rainfall (1 month - TRMM)|hämtdatum=30 januari 2016 |efternamn= |förnamn= |datum= |verk= |utgivare= NASA/Tropical Rainfall Monitoring Mission}}";
            else
                gn = "{{Cite web |url= http://neo.sci.gsfc.nasa.gov/view.php?datasetId=TRMM_3B43M|title= NASA Earth Observations: Rainfall (1 month - TRMM)|access-date = 30 Enero 2016 |publisher= NASA/Tropical Rainfall Monitoring Mission}}";
            //{{Webbref |url= |titel= |hämtdatum= |författare= |efternamn= |förnamn= |författarlänk= |efternamn2= |förnamn2= |datum= |år= |månad= |format= |verk= |utgivare= |sid= |språk= |doi= |arkivurl= |arkivdatum= |citat= |ref= }}
            return addref("nasarain", gn);
        }

        public static string nasalandcoverref()
        {

            string gn = "";
            if (makelang == "sv")
                gn = "{{webbref |url= http://neo.sci.gsfc.nasa.gov/view.php?datasetId=MCD12C1_T1|titel= NASA Earth Observations: Land Cover Classification|hämtdatum=30 januari 2016 |efternamn= |förnamn= |datum= |verk= |utgivare= NASA/MODIS}}";
            else
                gn = "{{Cite web |url= http://neo.sci.gsfc.nasa.gov/view.php?datasetId=MCD12C1_T1|title= NASA Earth Observations: Land Cover Classification|access-date = 30 Enero 2016 |publisher= NASA/MODIS}}";
            //{{Webbref |url= |titel= |hämtdatum= |författare= |efternamn= |förnamn= |författarlänk= |efternamn2= |förnamn2= |datum= |år= |månad= |format= |verk= |utgivare= |sid= |språk= |doi= |arkivurl= |arkivdatum= |citat= |ref= }}
            return addref("nasalandcover", gn);
        }

        public static string koppenref()
        {
            string gn = "";
            if (makelang == "sv")
                gn = "{{Tidskriftsref | författare = | redaktör = | rubrik = Updated world map of the Köppen-Geiger climate classification| url = http://www.hydrol-earth-syst-sci.net/11/1633/2007/hess-11-1633-2007.html| år = 2007| tidskrift = Hydrology and Earth System Sciences| volym = 11| utgivningsort = | utgivare = | nummer = | sid = 1633-1644| hämtdatum = 2016-01-30| id = | doi = 10.5194/hess-11-1633-2007| issn = | citat = | språk = | förnamn =M C| förnamn2 = B L| förnamn3 = T A| efternamn = Peel| efternamn2 = Finlayson| efternamn3 =McMahon| ref = }}";
            else
                gn = "{{cite journal |last= Peel|first= M C|last2= Finlayson|first2= B L|date= |title= Updated world map of the Köppen-Geiger climate classification| url = http://www.hydrol-earth-syst-sci.net/11/1633/2007/hess-11-1633-2007.html |journal= Hydrology and Earth System Sciences|publisher= |volume= 11|issue= |pages= 1633-1644|doi= 10.5194/hess-11-1633-2007|access-date=30 Enero 2016}}";
            return addref("koppen", gn);
        }

        public static string wikiref(string iwlang)
        {
            Console.WriteLine("wikiref " + iwlang);

            if ((iwlang == "Wikidata") || (iwlang == "wd"))
                return addref(iwlang, "(" + mp(130) + " " + iwlang + DateTime.Now.ToString("yyyy-MM-dd") + ")");
            else
                return addref(iwlang + "wiki", "(" + mp(130) + " " + iwlang + "wiki " + wdtime.ToString("yyyy-MM-dd") + ")");

        }

        public static string makegnidlink(int gnid)
        {
            string link = "[[";

            if (!gndict.ContainsKey(gnid))
            {
                Console.WriteLine("Bad gnid in makegnidlink " + gnid.ToString());
                return "";
            }

            string aname = gndict[gnid].articlename;
            if (aname == "XXX")
                return "[[" + gndict[gnid].Name_ml + "]]"; //no article name - return Name_ml linked!

            if (aname.Contains("*"))
                aname = aname.Replace("*", "");

            if (gndict[gnid].Name_ml == aname)
                link += aname;
            else
                link += aname + "|" + gndict[gnid].Name_ml;
            link += "]]";
            return link;
        }

        public static string make_catname(string catcode, string adm, bool countrylevel)
        {
            string catname = "";
            if (countrylevel)
            {
                catname = mp(1) + initialcap(categoryml[catcode]) + " " + mp(75) + " " + adm;
                if ((makelang == "sv") && ((catcode == "geography") || (catcode == "islands")))
                {
                    catname = mp(1) + adm + "s " + categoryml[catcode];
                    if (catname.Contains("ss " + categoryml[catcode]))
                        catname = catname.Replace("ss " + categoryml[catcode], "s " + categoryml[catcode]);
                    if (catname.Contains("zs " + categoryml[catcode]))
                        catname = catname.Replace("zs " + categoryml[catcode], "z " + categoryml[catcode]);

                }
            }
            else
            {
                catname = mp(1) + initialcap(categoryml[catcode]) + " " + mp(75) + " " + adm;
            }
            return catname;
        }

        public static void make_x_in_country(string catcode, string countrynameml)
        {
            string catname = make_catname(catcode, countrynameml, true);

            if (donecats.Contains(catname))
                return;
            else
            {
                stats.Adddonecat();
                donecats.Add(catname);
            }
            Page pc = new Page(makesite, catname);
            tryload(pc, 1);
            if (pc.Exists())
                return;

            string[] p95 = new string[1] { categoryml[catcode] };

            if (parentcategory[catcode] == "top")
            {
                pc.text = mp(120);
                pc.AddToCategory(initialcap(mp(95, p95)) + "|" + countrynameml);
                pc.AddToCategory(countrynameml);
                trysave(pc, 2, makesite.defaultEditComment+" "+mp(1));

                Page pc1 = new Page(makesite, mp(1) + mp(95, p95));
                tryload(pc1, 1);
                if (!pc1.Exists())
                {
                    pc1.text = mp(120);
                    pc1.AddToCategory(initialcap(categoryml[catcode]));
                    trysave(pc1, 2, makesite.defaultEditComment + " " + mp(1));
                }


            }
            else
            {
                pc.text = mp(120);
                pc.AddToCategory(make_catname(parentcategory[catcode], countrynameml, true));
                pc.AddToCategory(initialcap(mp(95, p95)) + "|" + countrynameml);
                trysave(pc, 2, makesite.defaultEditComment + " " + mp(1));
                make_x_in_country(parentcategory[catcode], countrynameml);

            }

        }

        public static void make_x_in_adm1(string catcode, int admgnid, string countrynameml)
        {
            string admname = getartname(admgnid);
            string catname = make_catname(catcode, admname, false);
            Console.WriteLine(catname);

            if (donecats.Contains(catname))
                return;
            else
            {
                stats.Adddonecat();
                donecats.Add(catname);
            }

            Page pc = new Page(makesite, catname);
            if (!tryload(pc, 1))
                return;
            if (pc.Exists())
                return;

            if (parentcategory[catcode] == "top")
            {
                pc.text = mp(120);
                string[] p93 = new string[3] { categoryml[catcode], countrynameml, getadmlabel(makecountry, 1, admgnid) };
                pc.AddToCategory(initialcap(mp(93, p93)));
                //pc.AddToCategory(getgnidname(admgnid));
                trysave(pc, 2, makesite.defaultEditComment + " " + mp(1));

                Page pc1 = new Page(makesite, mp(1) + mp(93, p93));
                tryload(pc1, 1);
                if (!pc1.Exists())
                {
                    pc1.text = mp(120);
                    pc1.AddToCategory(make_catname(catcode, countrynameml, true));
                    trysave(pc1, 2, makesite.defaultEditComment + " " + mp(1));
                    Page pc2 = new Page(makesite, make_catname(catcode, countrynameml, true));
                    tryload(pc2, 1);
                    if (!pc2.Exists())
                    {
                        pc2.text = mp(120);
                        pc2.AddToCategory(countrynameml);
                        trysave(pc2, 2, makesite.defaultEditComment + " " + mp(1));
                    }
                }


            }
            else
            {
                string parentcat = make_catname(parentcategory[catcode], admname, false);
                Console.WriteLine(parentcat);
                pc.text = mp(120);
                pc.AddToCategory(parentcat);
                pc.AddToCategory(make_catname(catcode, countrynameml, true));
                trysave(pc, 2, makesite.defaultEditComment + " " + mp(1));
                make_x_in_adm1(parentcategory[catcode], admgnid, countrynameml);
                make_x_in_country(catcode, countrynameml);
            }
        }

        public static void merge_refs(Page p, string refstring)
        {
            //Console.WriteLine(refstring);
            if (!p.text.Contains("</references>"))
            {
                Console.WriteLine("no </references>");
                return;
            }

            p.text = p.text.Replace("</references>", refstring.Replace("<references>", "") + "\n</references>");

            //Reference list:

            //if (hasnotes)
            //    p.text += mp(175).Replace("XX", "\n\n");

            //reflist += "\n</references>\n\n";
            //p.text += "\n\n== " + mp(51) + " ==\n\n" + reflist;


        }

        public static void retrofit_nasa(int gnid)
        {
            Console.WriteLine("============");

            hasnotes = false;

            if (!gndict.ContainsKey(gnid))
            {
                Console.WriteLine("Bad gnid in retrofit_nasa " + gnid.ToString());
                return;
            }

            Console.WriteLine(gndict[gnid].featureclass.ToString() + "." + gndict[gnid].featurecode);

            if (createclass != ' ')
                if (gndict[gnid].featureclass != createclass)
                {
                    Console.WriteLine("Wrong class in retrofit_nasa");
                    return;
                }
            if (createfeature != "")
                if (gndict[gnid].featurecode != createfeature)
                {
                    Console.WriteLine("Wrong feature in retrofit_nasa");
                    return;
                }

            if (createexceptfeature != "")
                if (gndict[gnid].featurecode == createexceptfeature)
                {
                    Console.WriteLine("Wrong feature in retrofit_nasa");
                    return;
                }

            if (createunit > 0)
            {
                bool admfound = false;
                for (int i = 0; i < 5; i++)
                    if (gndict[gnid].adm[i] == createunit)
                        admfound = true;
                if (!admfound)
                {
                    Console.WriteLine("Wrong adm-unit in retrofit_nasa");
                    return;
                }
            }

            if (createexceptunit > 0)
            {
                bool admfound = false;
                for (int i = 0; i < 5; i++)
                    if (gndict[gnid].adm[i] == createexceptunit)
                        admfound = true;
                if (admfound)
                {
                    Console.WriteLine("Wrong adm-unit in retrofit_nasa");
                    return;
                }
            }


            string prefix = testprefix;
            //string maintitle = "";

            if (gndict[gnid].articlename == "XXX")
            {
                Console.WriteLine("No articlename");
                gndict[gnid].articlename = gndict[gnid].Name_ml;
                //return;
            }

            if (String.IsNullOrEmpty(gndict[gnid].articlename))
            {
                Console.WriteLine("Null articlename");
                gndict[gnid].articlename = gndict[gnid].Name_ml;
                return;
            }

            Console.WriteLine("gnid = " + gnid);


            string countryname = "";
            int icountry = -1;
            if (countrydict.ContainsKey(gndict[gnid].adm[0]))
            {
                icountry = gndict[gnid].adm[0];
                countryname = countrydict[gndict[gnid].adm[0]].Name;
                //Console.WriteLine("country name = " + countryname);
                //Console.WriteLine("Native wiki = "+countrydict[icountry].nativewiki);
            }
            else
                Console.WriteLine("Invalid country " + gndict[gnid].adm[0].ToString());

            string countrynameml = countryname;
            if (countryml.ContainsKey(countryname))
                countrynameml = countryml[countryname];

            string cleanname = remove_disambig(gndict[gnid].articlename);
            string oldname = gndict[gnid].articlename.Replace("*", "");
            if (!is_latin(cleanname))
            {
                string latinname = gndict[gnid].asciiname;
                if ((get_alphabet(cleanname) == "cyrillic") && (makelang == "sv"))
                {
                    latinname = cyrillic.Transliterate(cleanname, countrydict[icountry].nativewiki);
                }
                gndict[gnid].articlename = gndict[gnid].articlename.Replace(cleanname, latinname);
                if (!is_latin(gndict[gnid].Name_ml))
                    gndict[gnid].Name_ml = latinname;
            }

            Page p = new Page(makesite, prefix + getartname(gnid));

            tryload(p, 3);

            string origtext = "";

            if (p.Exists())
            {
                if (!p.text.Contains(mp(195)))
                {
                    Console.WriteLine("Not botmade");
                    return;
                }

                if (!p.text.Contains(gnid.ToString()))
                {
                    Console.WriteLine("Wrong gnid in old article");
                    return;
                }

                if (p.text.Contains("NASA Earth"))
                {
                    Console.WriteLine("Already done");
                    return;
                }


                origtext = p.text;

                string mp117 = mp(117);
                string mp118 = mp(118);
                string mp119 = mp(119);

                string mpop = make_popdensity(gnid).Trim();
                bool popdone = false;
                if (!String.IsNullOrEmpty(mpop))
                {
                    if (p.text.Contains(mp117))
                    {
                        p.text = p.text.Replace(mp117, mpop);
                        popdone = true;
                    }
                    else if (p.text.Contains(mp118))
                    {
                        p.text = p.text.Replace(mp118, mpop);
                        popdone = true;
                    }
                    else if (p.text.Contains(mp119))
                    {
                        p.text = p.text.Replace(mp119, mpop);
                        popdone = true;
                    }

                }

                string climate = make_climate(gnid);
                string lc = make_landcover(gnid);

                string total = lc;
                if (!popdone && !String.IsNullOrEmpty(mpop))
                {
                    if (!String.IsNullOrEmpty(total))
                        total += " ";
                    total += mpop;
                }
                if (!String.IsNullOrEmpty(climate))
                {
                    if (!String.IsNullOrEmpty(total))
                        total += " ";
                    total += climate;
                }

                total = total.Trim();

                string mp175 = mp(175).Replace("XX", "").Substring(0, 11);

                string replacekey = "XXXX";
                if (p.text.Contains(mp(51)))
                    replacekey = "== " + mp(51);
                if (p.text.Contains(mp175))
                    replacekey = mp175;

                Console.WriteLine("replacekey = " + replacekey);

                p.text = p.text.Replace(replacekey, total + "\n\n" + replacekey);

                merge_refs(p, reflist);

                p.text = p.text.Replace("Mer om algoritmen finns här: [[Användare:Lsjbot/Algoritmer]].", "{{Lsjbot-algoritmnot}}").Replace("\n\n\n", "\n\n");
                p.text = p.text.Replace("at [{{Geonames", mp(293) + " [{{Geonames");

                //Reference list:

                //if (hasnotes)
                //    p.text += mp(175).Replace("XX", "\n\n");

                //reflist += "\n</references>\n\n";
                //p.text += "\n\n== " + mp(51) + " ==\n\n" + reflist;

                if (p.text != origtext)
                    trysave(p, 3);
            }
        }


        public static void retrofit_nasa()
        {
            makesite.defaultEditComment = mp(219) + " " + countryml[makecountryname];

            int iremain = gndict.Count;
            int iremain0 = iremain;

            foreach (int gnid in gndict.Keys)
            {
                iremain--;
                if ((resume_at > 0) && (resume_at != gnid))
                {
                    stats.Addskip();
                    continue;
                }
                else
                    resume_at = -1;

                if (stop_at == gnid)
                    break;

                reflist = "<references>";
                refnamelist.Clear();

                retrofit_nasa(gnid);

                Console.WriteLine(iremain.ToString() + " remaining.");

                if (firstround && (iremain0 - iremain < 5))
                {
                    Console.WriteLine("<cr>");
                    Console.ReadLine();
                }
            }

        }

        public static void test_nasa()
        {
            foreach (int gnid in gndict.Keys)
                make_climate(gnid);
            foreach (string s in climatemismatchdict.Keys)
                Console.WriteLine(s + ": " + climatemismatchdict[s]);

            while (true)
            {
                Console.WriteLine("Latitude:");
                double lat = tryconvertdouble(Console.ReadLine());
                Console.WriteLine("Longitude:");
                double lon = tryconvertdouble(Console.ReadLine());

                Console.WriteLine("Climate: " + make_climate(lat, lon));
                Console.WriteLine("Landcover: " + make_landcover(lat, lon));
                Console.WriteLine("Pop density: " + make_popdensity(lat, lon));
            }
        }

        public static string make_climate_chart(nasaclass nc, string name)
        {
            string s = mp(220);
            if (String.IsNullOrEmpty(s))
                return s;

            s = "{| border=\"1\"\n" + s + "\n|  " + name + "\n";
            int rainmax = 0;
            bool validrain = true;
            bool zerosuppress = (makelang == "sv");

            for (int i = 1; i < 13; i++)
            {
                if ((nc.month_temp_day[i] < -100) || (nc.month_temp_night[i] < -100))
                {
                    Console.WriteLine("Invalid temperature data in make_climate_chart");
                    return "";
                }
                else if (nc.month_rain[i] < 0)
                    validrain = false;
            }
            for (int i = 1; i < 13; i++)
            {
                s += "| " + nc.month_temp_night[i].ToString(culture_en) + "| " + nc.month_temp_day[i].ToString(culture_en);
                if ((validrain) || (zerosuppress && (nc.month_rain[i] >= 0)))
                    s += "| " + nc.month_rain[i].ToString() + "\n";
                else if (zerosuppress)
                    s += "|\n";
                else
                    s += "| 0\n";
                if (nc.month_rain[i] > rainmax)
                    rainmax = nc.month_rain[i];
            }
            if (rainmax > 750)
                s += "|maxprecip = " + rainmax.ToString() + "\n";

            s += "|float=left\n|clear=left\n|source = " + nasaref() + "\n}}\n|}";


            //|maxprecip = <!--- supply largest monthly precipitation, in case it's > 750 mm (30 in.) --->
            //|float     = <!--- left, right, or none --->
            //|clear     = <!--- left, right, both, or none --->
            //|units     = <!--- set to "imperial" if the values are in °F and inches --->
            //|source    = <!--- source of the data --->
            //}}
            return s;

        }

        public static void read_nasa()
        {
            string fname = geonamesfolder + @"nasa.txt";
            Console.WriteLine("read_nasa " + fname);
            if (!File.Exists(fname))
                return;

            //if (stats)
            //{
            //    pophist.SetBins(0.0, 10000.0, 100);
            //    rainhist.SetBins(0.0, 10000.0, 100);
            //    rainminmaxhist.SetBins(0.0, 2000.0, 20);
            //    daynighthist.SetBins(-20.0, 40.0, 30);
            //}

            using (StreamReader sr = new StreamReader(fname))
            {
                int n = 0;

                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();

                    //if (n > 250)
                    //    Console.WriteLine(line);

                    string[] words = line.Split(tabchar);

                    //foreach (string s in words)
                    //    Console.WriteLine(s);

                    //Console.WriteLine(words[0] + "|" + words[1]);

                    //    public static Dictionary<string,Dictionary<string,int>> adm1dict = new Dictionary<string,Dictionary<string,int>>();

                    int code = tryconvert(words[0]);
                    //Console.WriteLine("code = " + code);
                    if (code < 0)
                        continue;

                    n++;

                    nasaclass nc = new nasaclass();
                    //public class nasaclass
                    //{
                    //    public int landcover = -1; //Landcover code 1-17 http://eospso.nasa.gov/sites/default/files/atbd/atbd_mod12.pdf
                    //    public int popdensity = -1; //people per square km
                    //    public int temp_average = -999; //average across months and day-night
                    //    public int temp_max = -999; //temp of hottest month
                    //    public int month_max = -999; //hottest month (1-12)
                    //    public int temp_min = -999; //temp of coldest month
                    //    public int month_min = -999; //coldest month
                    //    public int temp_daynight = -999; //average difference between day and night
                    //    public int rainfall = -999; //mm per year
                    //}

                    nc.landcover = tryconvert(words[1]);
                    nc.popdensity = tryconvert(words[2]);
                    nc.temp_average = tryconvert(words[3]);
                    nc.temp_max = tryconvert(words[4]);
                    nc.month_max = tryconvert(words[5]);
                    nc.temp_min = tryconvert(words[6]);
                    nc.month_min = tryconvert(words[7]);
                    nc.temp_daynight = tryconvert(words[8]);
                    nc.rainfall = tryconvert(words[9]);
                    nc.rain_max = tryconvert(words[10]);
                    nc.rain_month_max = tryconvert(words[11]);
                    nc.rain_min = tryconvert(words[12]);
                    nc.rain_month_min = tryconvert(words[13]);
                    nc.koppen = tryconvert(words[14]);
                    if (words.Length > 15)
                    {
                        for (int i = 0; i < 13; i++)
                            nc.month_temp_day[i] = tryconvert(words[15 + i]);
                        for (int i = 0; i < 13; i++)
                            nc.month_temp_night[i] = tryconvert(words[28 + i]);
                        for (int i = 0; i < 13; i++)
                            nc.month_rain[i] = tryconvert(words[41 + i]);
                    }

                    int ndaynight = 0;
                    double daynightdiff = 0;
                    for (int i = 1; i < 13; i++) //remove zeroes that might be invalid
                    {
                        //if ( Math.Abs(nc.month_temp_day[i]) < 0.01 )
                        //    nc.month_temp_day[i] = -999;
                        //if (Math.Abs(nc.month_temp_night[i]) < 0.01)
                        //    nc.month_temp_night[i] = -999;
                        if ((nc.month_temp_day[i] > -100) && (nc.month_temp_night[i] > -100) && (Math.Abs(nc.month_temp_day[i]) > 0.01) && (Math.Abs(nc.month_temp_night[i]) > 0.01))
                        {
                            daynightdiff += nc.month_temp_day[i] - nc.month_temp_night[i];
                            ndaynight++;
                        }
                    }
                    if (ndaynight > 0)
                    {
                        nc.temp_daynight = Convert.ToInt32(daynightdiff / (1.0 * ndaynight));

                        if (ndaynight < 12)
                        {
                            for (int i = 1; i < 13; i++) //remove zeroes that might be invalid, if expected temp outside +-2
                            {
                                if (Math.Abs(nc.month_temp_day[i]) < 0.01)
                                {
                                    if (nc.month_temp_night[i] > -100)
                                    {
                                        double expected = nc.month_temp_night[i] + nc.temp_daynight;
                                        if (Math.Abs(expected) > 2)
                                            nc.month_temp_day[i] = -999;
                                    }
                                    else
                                        nc.month_temp_day[i] = -999;
                                }
                                if (Math.Abs(nc.month_temp_night[i]) < 0.01)
                                {
                                    if (nc.month_temp_day[i] > -100)
                                    {
                                        double expected = nc.month_temp_day[i] - nc.temp_daynight;
                                        if (Math.Abs(expected) > 2)
                                            nc.month_temp_night[i] = -999;
                                    }
                                    else
                                        nc.month_temp_night[i] = -999;
                                }
                            }

                        }
                    }

                    int ntemp = 0;
                    double tempsum = 0;

                    nc.temp_max = -999;
                    nc.month_max = -1;
                    nc.temp_min = 999;
                    nc.month_min = -1;

                    for (int i = 1; i < 13; i++) //estimate missing values
                    {
                        if ((nc.month_temp_day[i] > -100) && (nc.month_temp_night[i] > -100))
                        {
                            tempsum += nc.month_temp_day[i];
                            tempsum += nc.month_temp_night[i];
                            ntemp += 2;
                        }
                        else if (nc.month_temp_day[i] > -100)
                        {
                            tempsum += nc.month_temp_day[i];
                            tempsum += nc.month_temp_day[i] - nc.temp_daynight;
                            ntemp += 2;
                        }
                        else if (nc.month_temp_night[i] > -100)
                        {
                            tempsum += nc.month_temp_night[i];
                            tempsum += nc.month_temp_night[i] + nc.temp_daynight;
                            ntemp += 2;
                        }
                    }

                    if (ntemp > 12)
                        nc.temp_average = Convert.ToInt32(tempsum / (1.0 * ntemp));

                    if (ntemp > 20)
                        for (int i = 1; i < 13; i++) //estimate missing values
                        {
                            double tmean = 0.5 * (nc.month_temp_day[i] + nc.month_temp_night[i]);
                            if (tmean > -100)
                            {
                                if (tmean > nc.temp_max)
                                {
                                    nc.temp_max = Convert.ToInt32(tmean);
                                    nc.month_max = i;
                                }
                                if (tmean < nc.temp_min)
                                {
                                    nc.temp_min = Convert.ToInt32(tmean);
                                    nc.month_min = i;
                                }
                            }
                        }

                    nasadict.Add(code, nc);

                    //if (stats)
                    //{
                    //    pophist.Add(Convert.ToDouble(nc.popdensity));
                    //    rainhist.Add(Convert.ToDouble(nc.rainfall));
                    //    rainminmaxhist.Add(Convert.ToDouble(nc.rain_max - nc.rain_min));
                    //    daynighthist.Add(Convert.ToDouble(nc.temp_daynight));
                    //}
                }
                Console.WriteLine("readnasa " + n);

                //if (stats)
                //{
                //    Console.WriteLine("Pophist:");
                //    pophist.PrintDHist();
                //    Console.ReadLine();
                //    Console.WriteLine("Rainhist:");
                //    rainhist.PrintDHist();
                //    Console.ReadLine();
                //    Console.WriteLine("Rainminmaxhist:");
                //    rainminmaxhist.PrintDHist();
                //    Console.ReadLine();
                //    Console.WriteLine("Daynighthist:");
                //    daynighthist.PrintDHist();
                //    Console.ReadLine();
                //}
            }
        }


        public static string make_popdensity(int gnid)
        {
            if (gndict.ContainsKey(gnid))
                return " " + make_popdensity(gndict[gnid].latitude, gndict[gnid].longitude).Replace("XXX", gndict[gnid].Name_ml);
            else
                return "";
        }

        public static string make_popdensity(double lat, double lon)
        {
            int row = Convert.ToInt32(10 * (90 - lat));
            int col = Convert.ToInt32(10 * (180 + lon));
            int code = 10000 * row + col;

            if (!nasadict.ContainsKey(code))
                return "";

            if (nasadict[code].popdensity >= 0)
            {
                if (nasadict[code].popdensity < 2)
                    return " " + mp(239) + nasapopref();
                else
                {
                    int[] poplevels = { 7, 20, 50, 250, 1000, 99999 };
                    int i = 0;
                    while (poplevels[i] < nasadict[code].popdensity)
                        i++;
                    string[] p238 = new string[] { mp(240 + i), fnum(nasadict[code].popdensity) };
                    return mp(238, p238) + nasapopref();
                }
            }

            return "";
        }

        public static string make_landcover(int gnid)
        {
            if (gndict.ContainsKey(gnid))
                return " " + make_landcover(gndict[gnid].latitude, gndict[gnid].longitude).Replace("XXX", gndict[gnid].Name_ml);
            else
                return "";
        }

        public static string make_landcover(double lat, double lon)
        {
            int row = Convert.ToInt32(10 * (90 - lat));
            int col = Convert.ToInt32(10 * (180 + lon));
            int code = 10000 * row + col;

            if (!nasadict.ContainsKey(code))
                return "";

            if ((nasadict[code].landcover > 0) && (nasadict[code].landcover <= 17))
            {
                return mp(220 + nasadict[code].landcover) + nasalandcoverref();
            }

            return "";
        }

        public static string make_climate(int gnid)
        {
            if (gndict.ContainsKey(gnid))
                return " " + make_climate(gndict[gnid].latitude, gndict[gnid].longitude, gndict[gnid].elevation, gndict[gnid].Name_ml).Replace("XXX", gndict[gnid].Name_ml);
            else
                return "";
        }

        public static string make_climate(double lat, double lon)
        {
            return make_climate(lat, lon, 0, "");
        }

        public static string make_climate(double lat, double lon, int altitude, string name)
        {
            Dictionary<int, string> koppendict = new Dictionary<int, string>();
            koppendict.Add(1, "rainforest");
            koppendict.Add(2, "monsoon");
            koppendict.Add(3, "savanna");
            koppendict.Add(4, "desert hot");
            koppendict.Add(5, "desert cold");
            koppendict.Add(6, "steppe hot");
            koppendict.Add(7, "steppe cold");
            koppendict.Add(8, "mediterranean");
            koppendict.Add(9, "mediterranean");
            koppendict.Add(10, "subalpine");
            koppendict.Add(11, "humid subtropical");
            koppendict.Add(12, "Cwb");
            koppendict.Add(13, "Cwc");
            koppendict.Add(14, "humid subtropical");
            koppendict.Add(15, "oceanic");
            koppendict.Add(16, "Cfc");
            koppendict.Add(17, "continental");
            koppendict.Add(18, "hemiboreal");
            koppendict.Add(19, "boreal");
            koppendict.Add(20, "continental subarctic");
            koppendict.Add(21, "continental");
            koppendict.Add(22, "hemiboreal");
            koppendict.Add(23, "boreal");
            koppendict.Add(24, "continental subarctic");
            koppendict.Add(25, "continental");
            koppendict.Add(26, "hemiboreal");
            koppendict.Add(27, "boreal");
            koppendict.Add(28, "continental subarctic");
            koppendict.Add(29, "tundra");
            koppendict.Add(30, "arctic");

            string s = "";
            //string dummyname = "XXX";

            int row = Convert.ToInt32(10 * (90 - lat));
            int col = Convert.ToInt32(10 * (180 + lon));
            int code = 10000 * row + col;

            Console.WriteLine("lat,lon = " + lat + ", " + lon);
            Console.WriteLine("row,col,code = " + row + ", " + col + ", " + code);

            if (!nasadict.ContainsKey(code))
                return "";

            string climate = "unknown";
            string koppenclimate = "";
            Console.WriteLine("koppen = " + nasadict[code].koppen);
            if (nasadict[code].koppen > 0)
                koppenclimate = koppendict[nasadict[code].koppen];

            if (koppenclimate.Contains("Cw"))
            {
                if (Math.Abs(lat) < 30)
                    koppenclimate = "tropical highland";
                else
                    koppenclimate = "oceanic";
            }

            if (koppenclimate == "Cfc")
            {
                if (Math.Abs(lat) > 60)
                    koppenclimate = "subarctic oceanic";
                else
                    koppenclimate = "oceanic";
            }

            if (koppenclimate == "tundra")
                if (nasadict[code].temp_max > 13)
                    koppenclimate = "unknown";


            if (koppenclimate == "subalpine")
                if (altitude < 500)
                    koppenclimate = "unknown";

            //{{cite journal | author=Peel, M. C. and Finlayson, B. L. and McMahon, T. A. | year=2007 | title= Updated world map of the Köppen-Geiger climate classification | journal=Hydrol. Earth Syst. Sci. | volume=11 | pages=1633-1644 | url=http://www.hydrol-earth-syst-sci.net/11/1633/2007/hess-11-1633-2007.html | issn = 1027-5606}}

            if (nasadict[code].rainfall >= 0)
            {
                if (nasadict[code].temp_average > -100)
                {
                    if (nasadict[code].temp_min >= 18) //Tropical
                    {
                        if (nasadict[code].rain_min > 60)
                            climate = "rainforest";
                        else if (nasadict[code].rain_min > 100 - nasadict[code].rainfall / 25)
                            climate = "monsoon";
                        else
                        {
                            int potevapo = 20 * nasadict[code].temp_average;
                            if (Math.Abs(nasadict[code].rain_month_max - nasadict[code].month_max) <= 5)
                                potevapo += 280 - 56 * Math.Abs(nasadict[code].rain_month_max - nasadict[code].month_max);
                            if (nasadict[code].rainfall < 0.5 * potevapo)
                                climate = "desert";
                            else if (nasadict[code].rainfall < potevapo)
                                climate = "steppe";
                            else
                            {
                                climate = "savanna";
                            }
                        }

                    }
                    else if (nasadict[code].temp_max < 0) //Arctic
                        climate = "arctic";
                    else if (nasadict[code].temp_max < 10) //Tundra
                        climate = "tundra";
                    else
                    {
                        int potevapo = 20 * nasadict[code].temp_average;
                        if (Math.Abs(nasadict[code].rain_month_max - nasadict[code].month_max) <= 5)
                            potevapo += 280 - 56 * Math.Abs(nasadict[code].rain_month_max - nasadict[code].month_max);
                        if (nasadict[code].rainfall < 0.5 * potevapo)
                            climate = "desert";
                        else if (nasadict[code].rainfall < potevapo)
                            climate = "steppe";
                        else
                        {
                            if (nasadict[code].temp_min > -3)
                                climate = "temperate";
                            else
                                climate = "continental";
                        }
                    }
                }
                else if (String.IsNullOrEmpty(koppenclimate)) //temperature and Köppen unknown
                {
                    int[] rainlevels = { 300, 1000, 99999 };
                    int i = 0;
                    while (rainlevels[i] < nasadict[code].rainfall)
                        i++;
                    //string[] p238 = new string[] { mp(240 + i), fnum(nasadict[code].popdensity) };
                    //s += " " + mp(238, p238);
                    if (i == 0)
                        climate = "dry";
                    else if (i >= 2)
                        climate = "wet";

                }
            }
            else if (nasadict[code].temp_average > -100)
            {
                if (nasadict[code].temp_min >= 18) //Tropical
                    climate = "tropical";
                else if (nasadict[code].temp_max < 0) //Arctic
                    climate = "arctic";
                else if (nasadict[code].temp_max < 10) //Tundra
                    climate = "tundra";
                else
                {
                    if (nasadict[code].temp_min > -3)
                        climate = "temperate";
                    else
                        climate = "continental";
                }

            }

            Console.WriteLine("koppen, climate 1 = " + koppenclimate + ", " + climate);

            if (!String.IsNullOrEmpty(koppenclimate)) //check consistency
            {
                if (koppenclimate.Contains(climate))
                {
                    climate = koppenclimate;
                }
                else if (climate == "unknown")
                {
                    climate = koppenclimate;
                }
                else
                {
                    if (climate == "temperate")
                    {
                        if (((nasadict[code].koppen == 5) || (nasadict[code].koppen >= 7)) && (nasadict[code].koppen <= 28))
                            climate = koppenclimate;
                        else
                        {
                            Console.WriteLine("Koppen = " + koppenclimate + ", climate = " + climate);
                            string kc = koppenclimate + " - " + climate;
                            if (!climatemismatchdict.ContainsKey(kc))
                                climatemismatchdict.Add(kc, 0);
                            climatemismatchdict[kc]++;

                            climate = "unknown";
                        }
                    }
                    else if (climate == "continental")
                    {
                        if ((nasadict[code].koppen == 5) || ((nasadict[code].koppen >= 7) && (nasadict[code].koppen <= 10)) || ((nasadict[code].koppen >= 15) && (nasadict[code].koppen <= 29)))
                            climate = koppenclimate;
                        else
                        {
                            Console.WriteLine("Koppen = " + koppenclimate + ", climate = " + climate);
                            string kc = koppenclimate + " - " + climate;
                            if (!climatemismatchdict.ContainsKey(kc))
                                climatemismatchdict.Add(kc, 0);
                            climatemismatchdict[kc]++;
                            climate = "unknown";
                        }
                    }
                    else if ((climate == "desert") || (climate == "steppe"))
                    {
                        if (koppenclimate.Contains("desert") || koppenclimate.Contains("steppe"))
                            climate = koppenclimate;
                        else if ((koppenclimate.Contains("cold")) && ((climate == "tundra") || (climate == "continental")))
                            climate = koppenclimate;
                        else if (koppenclimate == "mediterranean")
                            climate = koppenclimate;
                        else
                        {
                            Console.WriteLine("Koppen = " + koppenclimate + ", climate = " + climate);
                            string kc = koppenclimate + " - " + climate;
                            if (!climatemismatchdict.ContainsKey(kc))
                                climatemismatchdict.Add(kc, 0);
                            climatemismatchdict[kc]++;
                            climate = "unknown";
                        }
                    }
                    else if ((climate == "tropical") || (climate == "rainforest") || (climate == "monsoon"))
                    {
                        if (koppenclimate.Contains("tropical") || koppenclimate.Contains("rainforest") || koppenclimate.Contains("monsoon") || koppenclimate.Contains("savanna"))
                            climate = koppenclimate;
                        else
                        {
                            Console.WriteLine("Koppen = " + koppenclimate + ", climate = " + climate);
                            string kc = koppenclimate + " - " + climate;
                            if (!climatemismatchdict.ContainsKey(kc))
                                climatemismatchdict.Add(kc, 0);
                            climatemismatchdict[kc]++;
                            climate = "unknown";
                        }
                    }
                    else if (climate == "savanna")
                    {
                        if ((koppenclimate == "steppe hot") || (koppenclimate == "mediterranean") || (koppenclimate == "monsoon") || (koppenclimate == "humid subtropical"))
                            climate = koppenclimate;
                        else
                        {
                            Console.WriteLine("Koppen = " + koppenclimate + ", climate = " + climate);
                            string kc = koppenclimate + " - " + climate;
                            if (!climatemismatchdict.ContainsKey(kc))
                                climatemismatchdict.Add(kc, 0);
                            climatemismatchdict[kc]++;
                            climate = "unknown";
                        }
                    }
                    else if (climate == "tundra")
                    {
                        if (koppenclimate.Contains("boreal") || koppenclimate.Contains("suba"))
                            climate = koppenclimate;
                        else
                        {
                            Console.WriteLine("Koppen = " + koppenclimate + ", climate = " + climate);
                            string kc = koppenclimate + " - " + climate;
                            if (!climatemismatchdict.ContainsKey(kc))
                                climatemismatchdict.Add(kc, 0);
                            climatemismatchdict[kc]++;
                            climate = "unknown";
                        }
                    }
                    else if ((climate == "dry") || (climate == "wet"))
                        climate = koppenclimate;
                    else
                    {
                        Console.WriteLine("Koppen = " + koppenclimate + ", climate = " + climate);
                        string kc = koppenclimate + " - " + climate;
                        if (!climatemismatchdict.ContainsKey(kc))
                            climatemismatchdict.Add(kc, 0);
                        climatemismatchdict[kc]++;
                        climate = "unknown";
                    }
                }
            }

            Console.WriteLine("koppen, climate 2 = " + koppenclimate + ", " + climate);


            if (climate != "unknown")
            {

                switch (climate)
                {
                    case "rainforest":
                        s = mp(250);
                        break;
                    case "monsoon":
                        s = mp(251);
                        break;
                    case "savanna":
                        s = mp(252);
                        break;
                    case "desert":
                        s = mp(253);
                        break;
                    case "desert hot":
                        s = mp(254);
                        break;
                    case "desert cold":
                        s = mp(255);
                        break;
                    case "steppe":
                        s = mp(256);
                        break;
                    case "steppe hot":
                        s = mp(257);
                        break;
                    case "steppe cold":
                        s = mp(258);
                        break;
                    case "mediterranean":
                        s = mp(259);
                        break;
                    case "subalpine":
                        s = mp(260);
                        break;
                    case "humid subtropical":
                        s = mp(261);
                        break;
                    case "oceanic":
                        s = mp(262);
                        break;
                    case "subarctic oceanic":
                        s = mp(263);
                        break;
                    case "tropical highland":
                        s = mp(264);
                        break;
                    case "continental":
                        s = mp(265);
                        break;
                    case "hemiboreal":
                        s = mp(266);
                        break;
                    case "boreal":
                        s = mp(267);
                        break;
                    case "continental subarctic":
                        s = mp(268);
                        break;
                    case "tundra":
                        s = mp(269);
                        break;
                    case "arctic":
                        s = mp(270);
                        break;
                    case "tropical":
                        s = mp(271);
                        break;
                    case "dry":
                        s = mp(272);
                        break;
                    case "wet":
                        s = mp(273);
                        break;
                    case "temperate":
                        s = mp(274);
                        break;
                    default:
                        break;
                }
                if (climate == koppenclimate)
                    s += koppenref();

            }


            if (nasadict[code].temp_average > -100)
            {
                string[] p248 = new string[] { fnum(nasadict[code].temp_average) };
                s += " " + mp(248, p248);
                if (nasadict[code].month_max > 0)
                {
                    string[] p249 = new string[] { mp(280 + nasadict[code].month_max), fnum(nasadict[code].temp_max), mp(280 + nasadict[code].month_min), fnum(nasadict[code].temp_min) };
                    s += " " + mp(249, p249);
                }
                s += nasaref();

            }

            if (nasadict[code].rainfall >= 0)
            {
                string[] p246 = new string[] { fnum(nasadict[code].rainfall) };
                s += " " + mp(246, p246);
                if (nasadict[code].rain_month_max > 0)
                {
                    string[] p247 = new string[] { mp(280 + nasadict[code].rain_month_max), fnum(nasadict[code].rain_max), mp(280 + nasadict[code].rain_month_min), fnum(nasadict[code].rain_min) };
                    s += " " + mp(247, p247);
                }
                s += nasarainref();
            }


            if (nasadict[code].temp_average > -100)
            {
                s += "\n\n" + make_climate_chart(nasadict[code], name);
            }


            return s.Trim();
        }

        public static double city_radius(long population)
        {
            double radius = 1.5 + 0.004 * Math.Sqrt(population);

            return radius;
        }

        public static string from_capital(int gnid)
        {
            string fromcapital = ""; ;
            int capitalgnid = countrydict[gndict[gnid].adm[0]].capital_gnid;

            if (capitalgnid == gnid) //capital itself is not far from capital :)
                return fromcapital;

            if (nocapital.Contains(makecountry))
                return fromcapital;

            if (gndict.ContainsKey(capitalgnid))
            {
                double dist = get_distance(gnid, capitalgnid);
                double mindistcapital = city_radius(gndict[capitalgnid].population);

                if (dist > mindistcapital)
                {
                    int intdist = Convert.ToInt32(dist);
                    if (intdist > 300)
                    {
                        intdist = 100 * Convert.ToInt32(0.01 * dist);
                    }
                    else if (intdist > 30)
                    {
                        intdist = 10 * Convert.ToInt32(0.1 * dist);
                    }

                    fromcapital = ", " + fnum(intdist) + " " + mp(308) + " " + mp(100 + get_direction(capitalgnid, gnid)) + " " + mp(132) + " " + makegnidlink(capitalgnid);
                }
                else //coinciding with capital location
                {
                    if (gndict[gnid].featureclass == 'A')
                    {
                        fromcapital = ". " + initialcap(mp(132)) + " " + makegnidlink(capitalgnid) + " " + mp(77) + " " + gndict[gnid].Name_ml;
                    }
                    else if (featurepointdict[gndict[gnid].featurecode])
                    {
                        fromcapital = ", " + mp(198) + " " + mp(132) + " " + makegnidlink(capitalgnid);
                    }

                }
            }
            return fromcapital;

        }

        public static string getmonthstring()
        {
            DateTime thismonth = DateTime.Now;
            string monthstring = thismonth.Month.ToString();
            while (monthstring.Length < 2)
                monthstring = "0" + monthstring;
            return thismonth.Year.ToString() + "-" + monthstring;
        }

        public static void fill_wdid_buffer()
        {

            read_rdf_tree();

            foreach (int gnid in gndict.Keys)
            {
                if ((resume_at_wdid > 0) && (resume_at_wdid != gnid))
                {
                    continue;
                }
                else
                    resume_at_wdid = -1;

                if (!valid_article_type(gnid))
                    continue;

                int wdid = gndict[gnid].wdid;

                Console.WriteLine("wdid = " + wdid);

                if (wdid <= 0)
                {
                    wdid = get_wd_item_direct(gnid);
                    Console.WriteLine("THREAD: gnid,wdid: " + gnid.ToString() + ", " + wdid.ToString());
                    if (!check_wd_instance(gnid, wdid))
                    {
                        Console.WriteLine("THREAD: bad instance");
                        wdid = -1;
                    }
                }

                if (gndict[gnid].wdid > 0)
                {
                    if (!check_wd_instance(gnid, gndict[gnid].wdid))
                    {
                        Console.WriteLine("THREAD: bad instance");
                        wdid = -2;
                    }

                }
                if (!wdid_buffer.ContainsKey(gnid))
                    wdid_buffer.Add(gnid, wdid);

            }

            Console.WriteLine("End of fill_wdid_buffer");
        }

        public static bool valid_article_type(int gnid)
        {

            if (createclass != ' ')
                if (gndict[gnid].featureclass != createclass)
                {
                    Console.WriteLine("Wrong class in valid_article_type");
                    return false;
                }

            if (createexceptclass != ' ')
                if (gndict[gnid].featureclass == createclass)
                {
                    Console.WriteLine("Wrong class in valid_article_type");
                    return false;
                }


            if (createfeature != "")
                foreach (string cf in createfeature.Split(','))
                {
                    bool found = false;
                    if (gndict[gnid].featurecode == cf)
                    {
                        found = true;
                    }
                    if (!found)
                    {
                        Console.WriteLine("Wrong feature in valid_article_type");
                        return false;
                    }
                }

            if (createexceptfeature != "")
                foreach (string cf in createexceptfeature.Split(','))
                    if (gndict[gnid].featurecode == cf)
                    {
                        Console.WriteLine("Wrong feature in valid_article_type");
                        return false;
                    }

            if (createcategory != "")
                foreach (string cf in createcategory.Split(','))
                {
                    bool found = false;
                    if (categorydict[gndict[gnid].featurecode] == cf)
                    {
                        found = true;
                    }
                    if (!found)
                    {
                        Console.WriteLine("Wrong category in valid_article_type");
                        return false;
                    }
                }

            if (createexceptcategory != "")
                foreach (string cf in createexceptcategory.Split(','))
                    if (categorydict[gndict[gnid].featurecode] == cf)
                    {
                        Console.WriteLine("Wrong category in valid_article_type");
                        return false;
                    }



            if ((gndict[gnid].featureclass == 'A') && (gndict[gnid].featurecode.Length > 3) && (gndict[gnid].featurecode.Contains("ADM"))) //check so ADMx supported
            {
                int iadm = tryconvert(gndict[gnid].featurecode.Substring(3, 1));
                if (String.IsNullOrEmpty(getadmlabel(makecountry, iadm, gnid)))
                //if (iadm > admdict[makecountry].maxadm)
                {
                    Console.WriteLine("iadm = " + iadm.ToString());
                    Console.WriteLine("Unsupported ADM-label in valid_article_type");
                    //Console.ReadLine();
                    return false;
                }
            }

            if (createunit > 0)
            {
                bool admfound = false;
                for (int i = 0; i < 5; i++)
                    if (gndict[gnid].adm[i] == createunit)
                        admfound = true;
                if (!admfound)
                {
                    Console.WriteLine("Wrong adm-unit in valid_article_type");
                    return false;
                }
            }

            if (createexceptunit > 0)
            {
                bool admfound = false;
                for (int i = 0; i < 5; i++)
                    if (gndict[gnid].adm[i] == createexceptunit)
                        admfound = true;
                if (admfound)
                {
                    Console.WriteLine("Wrong adm-unit in valid_article_type");
                    return false;
                }
            }


            return true;
        }

        public static void make_article(int gnid)
        {

            Console.WriteLine("============");

            hasnotes = false;

            if (!gndict.ContainsKey(gnid))
            {
                Console.WriteLine("Bad gnid in make_article " + gnid.ToString());
                return;
            }

            if (blacklist.Contains(gnid))
            {
                Console.WriteLine("Blacklisted gnid in make_article " + gnid.ToString());
                return;
            }

            Console.WriteLine(gndict[gnid].featureclass.ToString() + "." + gndict[gnid].featurecode);

            if (!valid_article_type(gnid))
                return;

            if (resurrection > 0)
            {
                if (!resurrected.Contains(gnid))
                    return;
            }
            else if (resurrection < 0)
            {
                if (resurrected.Contains(gnid))
                {
                    Console.WriteLine("Skip resurrected");
                    return;
                }
            }

            if (gndict[gnid].roundminute && minutesensitivedict[gndict[gnid].featurecode])
            {
                Console.WriteLine("Rounded coordinates for sensitive article type");
                return;
            }


            string prefix = testprefix;
            string maintitle = "";

            if (gndict[gnid].articlename.Contains("*"))
            {
                Console.WriteLine("Exists already");
                if (makedoubles)
                {
                    prefix = doubleprefix;
                    maintitle = getartname(gnid);
                    Page pmain = new Page(makesite, maintitle);
                    if (tryload(pmain, 1))
                        if (!pmain.Exists())
                        {
                            prefix = testprefix;
                            maintitle = "";
                        }
                        else if (pmain.text.Contains(mp(195)) && !human_touched(pmain, makesite))
                        {
                            prefix = testprefix;
                            maintitle = "";
                        }

                }
                else
                    return;
            }

            if (gndict[gnid].articlename == "XXX")
            {
                Console.WriteLine("No articlename");
                gndict[gnid].articlename = gndict[gnid].Name_ml;
                //return;
            }

            if (String.IsNullOrEmpty(gndict[gnid].articlename))
            {
                Console.WriteLine("Null articlename");
                gndict[gnid].articlename = gndict[gnid].Name_ml;
                return;
            }


            string countryname = "";
            int icountry = -1;
            if (countrydict.ContainsKey(gndict[gnid].adm[0]))
            {
                icountry = gndict[gnid].adm[0];
                countryname = countrydict[gndict[gnid].adm[0]].Name;
                //Console.WriteLine("country name = " + countryname);
                //Console.WriteLine("Native wiki = "+countrydict[icountry].nativewiki);
            }
            else
                Console.WriteLine("Invalid country " + gndict[gnid].adm[0].ToString());

            Console.WriteLine("gnid = " + gnid);

            string countrynameml = countryname;
            if (countryml.ContainsKey(countryname))
                countrynameml = countryml[countryname];

            string cleanname = remove_disambig(gndict[gnid].articlename);
            string oldname = gndict[gnid].articlename.Replace("*", "");
            if (!is_latin(cleanname))
            {
                string latinname = gndict[gnid].asciiname;
                if ((get_alphabet(cleanname) == "cyrillic") && (makelang == "sv"))
                {
                    latinname = cyrillic.Transliterate(cleanname, countrydict[icountry].nativewiki);
                }
                gndict[gnid].articlename = gndict[gnid].articlename.Replace(cleanname, latinname);
                if (!is_latin(gndict[gnid].Name_ml))
                    gndict[gnid].Name_ml = latinname;
            }

            if (gndict[gnid].articlename != oldname)
            {
                int ilang = -1;
                if (langtoint.ContainsKey(countrydict[icountry].nativewiki))
                    ilang = langtoint[countrydict[icountry].nativewiki];
                make_redirect(oldname, getartname(gnid), "", ilang);
            }

            if ((makecountry == "CN") && ((gndict[gnid].featurecode == "ADM4") || (gndict[gnid].featurecode == "PPLA4")))
            {
                if (is_zhen(gnid))
                {
                    gndict[gnid].oldarticlename = gndict[gnid].articlename;
                    gndict[gnid].articlename = gndict[gnid].articlename.Replace("(" + mp(298), "(" + mp(297));
                }
            }



            //TEMPORARY!
            //if (getartname(gnid).Contains(","))
            //{
            //    string namenocomma = getartname(gnid).Replace(",", "");
            //    Page pagenc = new Page(makesite, namenocomma);
            //    tryload(pagenc, 1);
            //    if ( pagenc.Exists())
            //        if (pagenc.text.Contains("obotskapad") && (pagenc.text.Contains(gnid.ToString())))
            //        {
            //            make_redirect_override(pagenc, getartname(gnid),"",-1);
            //            //Console.ReadLine();
            //        }
            //}
            //TEMPORARY!

            Page p = new Page(makesite, prefix + getartname(gnid));

            tryload(p, 3);

            bool ok_to_overwrite = false;
            string origtext = "";

            if (p.Exists())
            {
                Console.WriteLine("Exists already 1: " + p.title);
                if ((overwrite && (p.text.Contains(mp(195)) && !p.text.Contains(mp(69))) || p.IsRedirect()) && !human_touched(p, makesite) && p.text.Contains(gnid.ToString()))
                {
                    //p.text = "";
                    ok_to_overwrite = true;
                }
                else if (makedoubles && !p.text.Contains(mp(195)))
                {
                    prefix = doubleprefix;
                    maintitle = p.title;
                    p.title = doubleprefix + p.title;
                    Console.WriteLine("Prefix 1: " + p.title);
                }
                else
                    return;
            }

            if ((!String.IsNullOrEmpty(gndict[gnid].oldarticlename)) && (gndict[gnid].oldarticlename.Replace("*", "") != gndict[gnid].articlename.Replace("*", "")))
            {
                Page pold = new Page(makesite, gndict[gnid].oldarticlename.Replace("*", ""));
                tryload(pold, 1);
                if (pold.Exists())
                {
                    if (human_touched(pold, makesite)) //old article exists and is edited; don't make new, redirect and return instead
                    {
                        make_redirect(getartname(gnid), pold.title, "", -1);
                        return;
                    }
                    else if (!is_fork(pold))
                        make_redirect_override(pold, getartname(gnid), "", -1); //redirect from old to new
                }
            }

            //if ( gndict[gnid].wdid <= 0 )
            //    gndict[gnid].wdid = get_wd_item_direct(gnid);

            if (gndict[gnid].wdid <= 0)
            {
                if (makespecificarticles || remakearticleset)
                {
                    gndict[gnid].wdid = get_wdid_by_name(makelang, p.title, gnid);
                    if (gndict[gnid].wdid < 0)
                        gndict[gnid].wdid = get_wd_item_direct(gnid);
                }
                else
                {
                    int nwait = 0;
                    while (!wdid_buffer.ContainsKey(gnid) && (nwait < 10))
                    {
                        Console.WriteLine("Waiting for wdid thread.");
                        Thread.Sleep(5000);//milliseconds
                        nwait++;
                    }
                    if (wdid_buffer.ContainsKey(gnid))
                    {
                        gndict[gnid].wdid = wdid_buffer[gnid];
                        Console.WriteLine("Getting from wdid thread.");
                    }
                }
            }
            else
            {
                if (wdid_buffer.ContainsKey(gnid))
                {
                    if (wdid_buffer[gnid] == -2)
                    {
                        Console.WriteLine("VETO from wdid thread. Bad instance");
                        gndict[gnid].wdid = -2;
                    }
                }
            }

            wdid = gndict[gnid].wdid;

            if (wdid > 0)
            {
                currentxml = get_wd_xml(wdid);
                if (currentxml == null)
                    wdid = -1;

                if (wdid > 0)
                {
                    double areawd = -1.0;
                    long popwd = -1;
                    string iwss = "";

                    bool preferurban = (gndict[gnid].featureclass == 'P');
                    get_wd_area_pop(wdid, currentxml, out areawd, out popwd, out iwss, preferurban);
                    if (popwd > 0)
                    {
                        Console.WriteLine("popwd = " + popwd.ToString());
                        gndict[gnid].population_wd = popwd;
                        if ((gndict[gnid].population < minimum_population) || (!prefergeonamespop))
                            gndict[gnid].population = popwd;
                        gndict[gnid].population_wd_iw = iwss;
                        //npop++;
                    }

                    if (areawd > 0)
                    {
                        gndict[gnid].area = areawd;
                        //narea++;
                    }

                }
            }
            else
                currentxml = null;

            //Console.WriteLine(gndict[gnid].Name + ": " + gndict[gnid].population_wd.ToString() + gndict[gnid].population_wd_iw);


            Console.WriteLine("wdid = " + wdid.ToString());

            string commonscat = "";

            if (wdid > 0)
            {
                Console.WriteLine("get_wd_sitelinks");
                Dictionary<string, string> sitelinks = get_wd_sitelinks(currentxml);
                foreach (string lang in sitelinks.Keys)
                {
                    if (lang == makelang + "wiki")
                    {
                        Console.WriteLine("Already iw to makelang (1)");
                        if (String.IsNullOrEmpty(prefix))
                        {
                            make_redirect(getartname(gnid), sitelinks[lang], "", -1);

                            if (makedoubles)
                            {
                                Page psl = new Page(makesite, sitelinks[lang]);
                                tryload(psl, 2);
                                if (psl.Exists() && !psl.IsRedirect())
                                    if ((p.title != sitelinks[lang]) || !ok_to_overwrite)
                                    {
                                        Console.WriteLine("Setting double");
                                        prefix = doubleprefix;
                                        if (!p.title.Contains(doubleprefix))
                                            p.title = doubleprefix + p.title;
                                        maintitle = sitelinks[lang];
                                        Console.WriteLine("Prefix 2: " + p.title);

                                    }
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    if (lang == "commonswiki")
                        commonscat = sitelinks[lang];
                }
                Console.WriteLine("get_wd_commonscat");
                if (String.IsNullOrEmpty(commonscat))
                    commonscat = get_wd_prop(propdict["commonscat"], currentxml);
            }


            string[] p10 = new string[3] { botname, countrynameml, getmonthstring() };

            origtext = p.text;
            p.text = mp(10, p10) + "\n";

            if (is_disambig(p.title)) //top link to disambig page
            {
                string forktitle = "";
                Page pfork = new Page(makesite, remove_disambig(p.title));
                if (tryload(pfork, 1))
                {
                    if (is_fork(pfork))
                        forktitle = pfork.title;
                    else
                    {
                        Page pfork2 = new Page(makesite, pfork.title + " (" + mp(67) + ")");
                        if (tryload(pfork2, 1))
                        {
                            if (pfork2.Exists())
                            {
                                forktitle = pfork2.title;
                            }
                        }

                    }
                }

                if (!String.IsNullOrEmpty(forktitle))
                {
                    String[] p181 = { forktitle };
                    p.text += mp(181, p181) + "\n";
                    //pauseaftersave = true;
                }
            }

            if (p.title.Contains("&#"))
                saveanomaly(p.title, "Contains broken html?");

            if ((gndict[gnid].featureclass != 'P') && (gndict[gnid].featureclass != 'A'))
            {
                gndict[gnid].population = 0;
                gndict[gnid].population_wd = 0;
            }

            if (gndict[gnid].featureclass == 'P')
                gndict[gnid].area = 0;


            p.text += fill_geobox(gnid) + "\n\n";

            //Native names:
            Dictionary<string, int> nativenames = new Dictionary<string, int>();

            if ((icountry > 0) && (altdict.ContainsKey(gnid)))
            {
                foreach (altnameclass ac in altdict[gnid])
                {
                    if (ac.altname != gndict[gnid].Name_ml)
                    {
                        if (countrydict[icountry].languages.Contains(ac.ilang))
                        {
                            if (!nativenames.ContainsKey(ac.altname))
                                nativenames.Add(ac.altname, ac.ilang);
                        }
                    }
                }
            }

            string nativestring = "";
            if (nativenames.Count > 0)
            {
                int nname = 0;
                bool commaneeded = false;
                nativestring = "(";

                int prevlang = -1;

                foreach (string nn in nativenames.Keys)
                {
                    int ilang = nativenames[nn];
                    if (langdict.ContainsKey(ilang))
                    {
                        if (langdict[ilang].name.ContainsKey(makelang))
                        {
                            if (commaneeded)
                                nativestring += ", ";
                            if (ilang != prevlang)
                                nativestring += "[[" + langdict[ilang].name[makelang] + "]]: ";
                            nativestring += "'''" + nn + "'''";
                            nname++;
                            commaneeded = true;
                            prevlang = ilang;
                        }
                    }

                }
                if (nname > 0)
                    nativestring += ") ";
                else
                    nativestring = "";
            }

            bool namestart = false; //true if previous sentence started with article name; used to avoid repetitive language.
            string sent = "";
            string pronoun = gndict[gnid].Name_ml;
            string flabel = getfeaturelabelindet(makecountry, gndict[gnid].featurecode, gnid);
            if (makelang == "sv")
            {
                if (flabel.StartsWith("en "))
                    pronoun = "den";
                else if (flabel.StartsWith("ett "))
                    pronoun = "det";
                else
                    pronoun = "de";
            }
            else if (makelang == "ceb")
            {
                if (featuredict[gndict[gnid].featurecode].StartsWith("mga "))
                    pronoun = "sila";
                else
                    pronoun = "siya";
            }


            /// X är en Y i landet Z
            sent += "'''" + gndict[gnid].Name_ml + "''' " + nativestring + mp(4) + " " + linkfeature(gndict[gnid].featurecode, gnid);
            if (countrydict.ContainsKey(gndict[gnid].adm[0]))
            {
                sent += " " + mp(75) + " " + countrytitle(gndict[gnid].adm[0]) + linkcountry(gndict[gnid].adm[0]);
            }
            if (gndict[gnid].altcountry.Count > 0)
            {
                string countrylist = "";
                int noo = 0;
                foreach (int oo in gndict[gnid].altcountry)
                {
                    if (countrydict.ContainsKey(oo))
                    {
                        if (motherdict.ContainsKey(makecountry))
                            if (oo == countryid[motherdict[makecountry]])
                                continue;
                        noo++;
                        if (noo > 1)
                        {
                            if (noo == gndict[gnid].altcountry.Count)
                                countrylist += mp(97);
                            else
                                countrylist += ",";
                        }
                        countrylist += " " + countrydict[oo].Name_ml;
                    }
                }
                if (noo > 0)
                {
                    sent += ", " + mp(134) + countrylist;
                }

            }
            p.text += sent + "." + geonameref(gnid);
            namestart = true;



            // X ligger i (kommun), (provins) och (region)

            sent = "";
            int maxadm = 0;
            int minadm = 999;

            for (int i = 4; i > 0; i--)
            {
                if (!String.IsNullOrEmpty(getgnidname(gndict[gnid].adm[i])) && (gndict[gnid].adm[i] != gnid))
                {
                    if (i > maxadm)
                        maxadm = i;
                    if (i < minadm)
                        minadm = i;
                }
            }

            //int capitalgnid = -1;
            string fromcapital = "";
            if (countrydict.ContainsKey(gndict[gnid].adm[0]))
            {
                fromcapital = from_capital(gnid);
            }


            string countrypart = mp(getcountrypart(gnid));
            if ((makelang == "sv") && (motherdict.ContainsKey(makecountry)))
                countrypart = countrypart.Replace("delen av landet", "delen av " + countrynameml);

            if (maxadm > 0)
            {
                sent += " " + mp(135) + " " + gndict[gnid].Name_ml + " " + mp(77) + " ";
                for (int i = 4; i > 0; i--)
                {
                    if (!String.IsNullOrEmpty(getgnidname(gndict[gnid].adm[i])) && (gndict[gnid].adm[i] != gnid))
                    {
                        sent += getadmdet(makecountry, i, gndict[gnid].adm[i]) + " " + comment("ADM" + i.ToString()) + makegnidlink(gndict[gnid].adm[i]);
                        if (i > minadm)
                        {
                            if (i == minadm + 1)
                                sent += " " + mp(3) + " ";
                            else
                                sent += ", ";
                        }
                    }
                }
                sent += ", " + countrypart + fromcapital + ".";

            }
            else
            {
                sent += " " + gndict[gnid].Name_ml + " " + mp(92) + " " + countrypart + fromcapital + ".";
            }

            if (!String.IsNullOrEmpty(sent))
                if (sent.Trim().StartsWith(gndict[gnid].Name_ml))
                {
                    if (namestart)
                    {
                        sent = ReplaceOne(sent, gndict[gnid].Name_ml, initialcap(pronoun), 0);
                        namestart = false;
                    }
                    else
                        namestart = true;
                }
                else
                    namestart = false;

            p.text += sent;



            //population & elevation & area



            string[] p99 = new string[2] { gndict[gnid].Name_ml, fnum(gndict[gnid].elevation) };
            string[] p100 = new string[1] { comment("pop") + fnum(gndict[gnid].population) };
            string[] p100wd = new string[1] { comment("pop") + fnum(gndict[gnid].population_wd) };


            sent = "";

            Console.WriteLine("elevation/population");
            if ((gndict[gnid].elevation > 0) && ((categorydict[gndict[gnid].featurecode] != "peninsulas")) && (featurepointdict[gndict[gnid].featurecode] || (categorydict[gndict[gnid].featurecode] == "lakes")))
            {
                string heightref = geonameref(gnid);
                if ((gndict[gnid].elevation > 0) && (gndict[gnid].elevation == gndict[gnid].elevation_vp))
                    heightref = addnote(mp(140) + addref("vp", viewfinder_ref()) + " " + mp(200));

                if ((makecountry == "CN") && (chinese_pop_dict2.ContainsKey(gnid)))
                {
                    p100[0] = comment("pop") + fnum(chinese_pop_dict2[gnid].pop);
                    sent += " " + mp(99, p99) + heightref + mp(97) + " " + mp(100, p100) + "." + chinapopref();
                    //public class chinese_pop_class
                    //{
                    //    public int adm1 = -1;
                    //    public long pop = -1;
                    //    public long malepop = -1;
                    //    public long femalepop = -1;
                    //    public long households = -1;
                    //    public long pop014 = -1;
                    //    public long pop1564 = -1;
                    //    public long pop65 = -1;
                    //}
                    string[] p300 = new string[2] { fnum(chinese_pop_dict2[gnid].femalepop), fnum(chinese_pop_dict2[gnid].malepop) };
                    sent += " " + mp(300, p300);
                    double c014 = (100 * chinese_pop_dict2[gnid].pop014) / chinese_pop_dict2[gnid].pop;
                    double c1564 = (100 * chinese_pop_dict2[gnid].pop1564) / chinese_pop_dict2[gnid].pop;
                    double c65 = (100 * chinese_pop_dict2[gnid].pop65) / chinese_pop_dict2[gnid].pop;
                    string[] p301 = new string[3] { fnum(c014), fnum(c1564), fnum(c65) };
                    sent += " " + mp(301, p301) + chinapopref();
                }
                else if (gndict[gnid].population > minimum_population)
                {
                    if (gndict[gnid].population_wd == gndict[gnid].population)
                        sent += " " + mp(99, p99) + heightref + mp(97) + " " + mp(100, p100wd) + "." + wikiref(gndict[gnid].population_wd_iw);
                    else
                        sent += " " + mp(99, p99) + heightref + mp(97) + " " + mp(100, p100) + "." + geonameref(gnid);
                }
                else
                {
                    if (gndict[gnid].population_wd > minimum_population)
                    {
                        p100[0] = gndict[gnid].population_wd.ToString();
                        sent += " " + mp(99, p99) + heightref + mp(97) + " " + mp(100, p100wd) + "." + wikiref(gndict[gnid].population_wd_iw);
                    }
                    else
                    {
                        int imp = 99;
                        bool peak = is_height(gndict[gnid].featurecode);
                        if (peak)
                            imp = 178;
                        //switch (categorydict[gndict[gnid].featurecode])
                        //{
                        //    case "mountains":
                        //    case "hills":
                        //    case "volcanoes":
                        //        imp = 178;
                        //        break;
                        //    default:
                        //        imp = 99;
                        //        break;

                        //}
                        sent += " " + mp(imp, p99);
                        if ((peak) && (gndict[gnid].prominence > minimum_prominence))
                        {
                            sent += "," + heightref;
                            string[] p190 = new string[1] { fnum(gndict[gnid].prominence) };
                            sent += " " + mp(190, p190);
                            sent += addnote(mp(191) + addref("vp", viewfinder_ref()) + " " + mp(200));
                            string[] p192 = new string[1] { fnum(gndict[gnid].width) };
                            sent += ". " + mp(192, p192) + "." + addnote(mp(193));
                        }
                        else
                            sent += "." + heightref;
                    }
                }
            }
            else if ((makecountry == "CN") && (chinese_pop_dict2.ContainsKey(gnid)))
            {
                p100[0] = comment("pop") + fnum(chinese_pop_dict2[gnid].pop);
                sent += " " + initialcap(mp(100, p100)) + "." + chinapopref();
                //public class chinese_pop_class
                //{
                //    public int adm1 = -1;
                //    public long pop = -1;
                //    public long malepop = -1;
                //    public long femalepop = -1;
                //    public long households = -1;
                //    public long pop014 = -1;
                //    public long pop1564 = -1;
                //    public long pop65 = -1;
                //}
                string[] p300 = new string[2] { fnum(chinese_pop_dict2[gnid].femalepop), fnum(chinese_pop_dict2[gnid].malepop) };
                sent += " " + mp(300, p300);
                double c014 = (100 * chinese_pop_dict2[gnid].pop014) / chinese_pop_dict2[gnid].pop;
                double c1564 = (100 * chinese_pop_dict2[gnid].pop1564) / chinese_pop_dict2[gnid].pop;
                double c65 = (100 * chinese_pop_dict2[gnid].pop65) / chinese_pop_dict2[gnid].pop;
                string[] p301 = new string[3] { fnum(c014), fnum(c1564), fnum(c65) };
                sent += " " + mp(301, p301) + chinapopref();
            }
            else if (gndict[gnid].population > minimum_population)
            {
                sent += " " + initialcap(mp(100, p100)) + ".";
                if (gndict[gnid].population_wd == gndict[gnid].population)
                    sent += wikiref(gndict[gnid].population_wd_iw);
                else
                    sent += geonameref(gnid);
            }
            else if (gndict[gnid].population_wd > minimum_population)
            {
                //p100[0] = comment("pop") + fnum(gndict[gnid].population_wd);
                sent += " " + initialcap(mp(100, p100wd)) + "." + wikiref(gndict[gnid].population_wd_iw);
            }

            Console.WriteLine("area");
            if (gndict[gnid].area > minimum_area)
            {
                string[] p129 = new string[2] { gndict[gnid].Name_ml, fnum(gndict[gnid].area) };
                sent += " " + mp(129, p129);
            }

            if (!String.IsNullOrEmpty(sent))
                if (sent.Trim().StartsWith(gndict[gnid].Name_ml))
                {
                    if (namestart)
                    {
                        sent = ReplaceOne(sent, gndict[gnid].Name_ml, initialcap(pronoun), 0);
                        namestart = false;
                    }
                    else
                        namestart = true;

                }
                else
                    namestart = false;

            p.text += sent;
            sent = "";

            if ((gndict[gnid].island > 0) && (categorydict[gndict[gnid].featurecode] != "islands")) //On island; not island-on-island
            {
                if (gndict[gndict[gnid].island].area > gndict[gnid].area) //Only if island is bigger than gnid
                {
                    string[] p139 = new string[2] { gndict[gnid].Name_ml, makegnidlink(gndict[gnid].island) };
                    sent += " " + mp(139, p139);
                    //if (makelang == "sv")
                    sent += addnote(mp(140) + addref("vp", viewfinder_ref()) + " " + mp(200));
                }
            }

            if (!String.IsNullOrEmpty(sent))
                if (sent.Trim().StartsWith(gndict[gnid].Name_ml))
                {
                    if (namestart)
                    {
                        sent = ReplaceOne(sent, gndict[gnid].Name_ml, initialcap(pronoun), 0);
                        namestart = false;
                    }
                    else
                        namestart = true;
                }
                else
                    namestart = false;

            p.text += sent;
            sent = "";

            if ((gndict[gnid].inlake > 0) && (categorydict[gndict[gnid].featurecode] != "lakes"))//In a lake (mainly islands); not lake-in-lake
            {
                if (gndict[gndict[gnid].inlake].area > gndict[gnid].area) //Only if lake is bigger than gnid
                {
                    string[] p155 = new string[2] { gndict[gnid].Name_ml, makegnidlink(gndict[gnid].inlake) };
                    sent += " " + mp(155, p155);
                    if (makelang == "sv")
                        sent += addnote(mp(140) + addref("vp", viewfinder_ref()) + " " + mp(200));
                }
            }


            if (!String.IsNullOrEmpty(sent))
                if (sent.Trim().StartsWith(gndict[gnid].Name_ml))
                {
                    if (namestart)
                    {
                        sent = ReplaceOne(sent, gndict[gnid].Name_ml, initialcap(pronoun), 0);
                        namestart = false;
                    }
                    else
                        namestart = true;
                }
                else
                    namestart = false;

            p.text += sent;
            sent = "";


            if (gndict[gnid].atlakes.Count > 0) //Near one or more lakes
            {
                if (gndict[gnid].atlakes.Count == 1)
                {
                    if (gndict[gndict[gnid].atlakes[0]].area > gndict[gnid].area) //Only if lake is bigger than gnid
                    {
                        string[] p156 = new string[2] { gndict[gnid].Name_ml, makegnidlink(gndict[gnid].atlakes[0]) };
                        sent += " " + mp(156, p156);
                        if (makelang == "sv")
                            sent += addnote(mp(140) + addref("vp", viewfinder_ref()) + " " + mp(200));
                    }
                }
                else
                {
                    string lakes = "";
                    int ilakes = 0;
                    foreach (int lg in gndict[gnid].atlakes)
                    {
                        ilakes++;
                        if (ilakes == gndict[gnid].atlakes.Count)
                            lakes += mp(97);
                        lakes += " " + makegnidlink(lg);
                    }
                    string[] p157 = new string[2] { gndict[gnid].Name_ml, lakes };
                    sent += " " + mp(157, p157);
                    //if (makelang == "sv")
                    sent += addnote(mp(140) + addref("vp", viewfinder_ref()) + " " + mp(200));
                }
            }

            if (!String.IsNullOrEmpty(sent))
                if (sent.Trim().StartsWith(gndict[gnid].Name_ml))
                {
                    if (namestart)
                    {
                        sent = ReplaceOne(sent, gndict[gnid].Name_ml, initialcap(pronoun), 0);
                        namestart = false;
                    }
                    else
                        namestart = true;
                }
                else
                    namestart = false;

            p.text += sent;
            sent = "";

            string rangecat = "";
            if ((gndict[gnid].inrange > 0) && (gndict.ContainsKey(gndict[gnid].inrange)))//Part of a mountain range
            {
                sent = " " + gndict[gnid].Name_ml + " " + mp(204) + " " + makegnidlink(gndict[gnid].inrange) + ".";
                sent += addnote(mp(140) + addref("vp", viewfinder_ref()) + " " + mp(200));
                rangecat = getartname(gndict[gnid].inrange);
            }

            if (!String.IsNullOrEmpty(sent))
                if (sent.Trim().StartsWith(gndict[gnid].Name_ml))
                {
                    if (namestart)
                    {
                        sent = ReplaceOne(sent, gndict[gnid].Name_ml, initialcap(pronoun), 0);
                        namestart = false;
                    }
                    else
                        namestart = true;
                }
                else
                    namestart = false;

            p.text += sent;
            sent = "";

            //p.text += "\n\n";

            //separate for different types of features:
            Console.WriteLine("Feature-specific");
            if ((gndict[gnid].featureclass == 'A') && (gndict[gnid].featurecode.Contains("ADM")) && (!gndict[gnid].featurecode.Contains("ADMD")))
            {
                p.text += make_adm(gnid);
            }
            else if (gndict[gnid].featureclass == 'P')
            {
                if ((gndict[gnid].population > minimum_population) || (chinese_pop_dict2.ContainsKey(gnid)))
                    p.text += make_town(gnid);
                else
                {
                    Console.WriteLine("Below minimum population.");
                    return;
                }
            }
            else if (categorydict[gndict[gnid].featurecode] == "islands")
            {
                p.text += make_island(gnid, p);
            }
            else if (categorydict[gndict[gnid].featurecode] == "lakes")
            {
                p.text += make_lake(gnid, p);
            }
            else if ((gndict[gnid].featurecode == "MTS") || (gndict[gnid].featurecode == "HLLS"))
            {
                p.text += make_range(gnid, p);
            }
            else if (gndict[gnid].featurecode == "CHN")
            {
                p.text += make_channel(gnid);
            }
            else if (featurepointdict[gndict[gnid].featurecode])
            {
                p.text += make_point(gnid);
            }
            else //Nothing type-specific to add
            {
            }

            if (!noclimatelist.Contains(gndict[gnid].featurecode))
            {
                p.text += "\n\n" + make_climate(gnid);
            }

            //locator map:
            if (!locatoringeobox)
            {
                string[] p73 = new string[2] { countrynameml, gndict[gnid].Name_ml };
                p.text += "\n\n" + mp(72) + "|" + locatordict[countryname].get_locator(gndict[gnid].latitude, gndict[gnid].longitude) + " |float = right  |width=300 |";
                if (makelang != "sv")
                    p.text += " caption = " + mp(73, p73) + " | ";
                p.text += mp(76) + " = " + gndict[gnid].Name_ml + "|position=right|background=white|lat=" + gndict[gnid].latitude.ToString(culture_en) + "|long=" + gndict[gnid].longitude.ToString(culture_en);
                p.text += "}}\n";
            }

            //Reference list:

            if (hasnotes)
                p.text += mp(175).Replace("XX", "\n\n");

            reflist += "\n</references>\n\n";
            p.text += "\n\n== " + mp(51) + " ==\n\n" + reflist;

            //External links:
            if (!String.IsNullOrEmpty(commonscat))
            {
                if (commonscat.Contains("Category:"))
                    commonscat = commonscat.Replace("Category:", "");
                p.text += "\n\n== " + mp(52) + "==\n\n{{commonscat|" + commonscat + "|" + gndict[gnid].Name_ml + "}}\n";
            }

            //If testrun only, inactivate categories and iw:

            if (!String.IsNullOrEmpty(prefix))
            {
                p.text += "<nowiki>\n";
            }

            //Categories:

            string catcode = categorydict[gndict[gnid].featurecode];

            string catname = "";
            if (!String.IsNullOrEmpty(getartname(gndict[gnid].adm[1])))
            {
                //catname = categoryml[catcode] + " " + mp(75) + " " + getgnidname(gndict[gnid].adm[1]);
                catname = make_catname(catcode, getartname(gndict[gnid].adm[1]), false);
                if (String.IsNullOrEmpty(prefix))
                    make_x_in_adm1(catcode, gndict[gnid].adm[1], countrynameml);
            }
            else
            {
                catname = make_catname(catcode, countrynameml, true);
                if (String.IsNullOrEmpty(prefix))
                    make_x_in_country(catcode, countrynameml);
            }
            if (!String.IsNullOrEmpty(rangecat))
            {
                p.AddToCategory(initialcap(rangecat));
                Page rcp = new Page(makesite, mp(1) + rangecat);
                tryload(rcp, 1);
                if (!rcp.Exists())
                {
                    rcp.text = mp(120) + "\n";
                    rcp.AddToCategory(initialcap(catname));
                    trysave(rcp, 2, makesite.defaultEditComment + " " + mp(1));
                }
                else if (!rcp.text.Contains(catname))
                {
                    rcp.AddToCategory(initialcap(catname));
                    trysave(rcp, 2, makesite.defaultEditComment + " " + mp(1));
                }

            }

            p.AddToCategory(initialcap(catname));

            switch (catcode)
            {
                case "lakes":
                case "islands":
                    cat_by_size(p, catcode, countrynameml, gndict[gnid].area);
                    break;
                case "mountains":
                case "hills":
                case "volcanoes":
                    cat_by_size(p, "mountains", countrynameml, gndict[gnid].elevation);
                    break;
                case "populated places":
                    double dpop = gndict[gnid].population;
                    cat_by_size(p, "populated places", countrynameml, dpop, false);
                    break;
                default:
                    break;
            }

            if ((makecountry == "CN") && (makelang == "sv"))
                p.AddToCategory("WP:Projekt Kina");

            p.text += "\n\n";

            //Interwiki:
            if (wdid > 0)
            {
                string iwl = iwlinks(currentxml);
                if (!iwl.Contains("Exists already"))
                    p.text += "\n" + iwl;
                //else
                //{
                //    string oldtit = iwl.Split(':')[1];
                //    if (!makedoubles)
                //    {
                //        make_redirect(prefix + gndict[gnid].articlename, oldtit, "", -1);
                //        return;
                //    }
                //    else
                //    {
                //        if ((p.title != oldtit) || !ok_to_overwrite)
                //        {
                //            if (!p.title.Contains(doubleprefix))
                //                p.title = doubleprefix + p.title;
                //            maintitle = oldtit;
                //        }
                //    }
                //}
            }
            else
            {
                if (!String.IsNullOrEmpty(gndict[gnid].artname2))
                {
                    string iwl = "\n[[";
                    if (makelang == "sv")
                        iwl += "ceb:";
                    else
                        iwl += "sv:";
                    iwl += gndict[gnid].artname2 + "]]\n";
                    p.text += iwl;
                }
            }

            if (!String.IsNullOrEmpty(prefix))
            {
                p.text += "</nowiki>\n";
            }

            if (makedoubles && !String.IsNullOrEmpty(maintitle))
                p.text = saveconflict(p.title, maintitle) + p.text;

            if (p.text.Contains(mp(213)))
                p.AddToCategory(mp(214));

            countryspecials(p, gnid, catcode);

            //Clean and save:

            p.text = p.text.Replace("{{geobox\n| 0 ", "{{geobox\n| 1 ");
            p.text = p.text.Replace("= <!--", "=\n<!--");

            p.text = cleanup_text(p.text);

            if (p.text != origtext)
            {
                if (ok_to_overwrite)
                    trysave(p, 4, mp(303) + " " + makesite.defaultEditComment);
                else
                    trysave(p, 4);
            }

            //Redirects:

            //if (!String.IsNullOrEmpty(testprefix))
            //{
            //make_redirect(testprefix + gndict[gnid].Name, gndict[gnid].articlename, "");

            if (resurrection <= 0)
            {
                if (gndict[gnid].Name != getartname(gnid))
                    make_redirect(testprefix + gndict[gnid].Name, getartname(gnid), "", -1);
                if (gndict[gnid].asciiname != getartname(gnid))
                    make_redirect(testprefix + gndict[gnid].asciiname, getartname(gnid), "", -1);
            }

            if (altdict.ContainsKey(gnid))
            {
                foreach (altnameclass ac in altdict[gnid])
                {
                    if ((!String.IsNullOrEmpty(ac.altname)) && (tryconvert(ac.altname) <= 0) && (ac.altname != remove_disambig(getartname(gnid))))
                        make_redirect(testprefix + ac.altname, getartname(gnid), "", ac.ilang);
                }
            }
            //}

            romanian_redirect(getartname(gnid));

            if (!String.IsNullOrEmpty(gndict[gnid].unfixedarticlename))
                make_redirect(gndict[gnid].unfixedarticlename.Replace("*", ""), getartname(gnid), "", -1);

            //Console.WriteLine("<ret>");
            //Console.ReadLine();

        }

        public static void countryspecials(Page p, int gnid, string catcode)
        {
            if (makecountry == "AQ") //specials for Antarctica:
            {
                p.SetTemplateParameter("geobox", "timezone", "", true);
                p.SetTemplateParameter("geobox", "timezone_label", "", true);
                p.SetTemplateParameter("geobox", "utc_offset", "", true);
                p.SetTemplateParameter("geobox", "timezone_DST", "", true);
                p.SetTemplateParameter("geobox", "utc_offset_DST", "", true);

                string sectortext = antarctic_sector(gndict[gnid].longitude);

                if (makelang == "sv")
                {
                    p.text = p.text.Replace("Trakten är glest befolkad. Det finns inga samhällen i närheten.", "Trakten är obefolkad. Det finns inga samhällen i närheten.");
                    p.text = p.text.Replace("delen av landet.", "delen av kontinenten. " + sectortext);
                    p.text = p.text.Replace(" Närmaste större samhälle ", " Närmaste befolkade plats ");
                    p.text = p.text.Replace("den östra delen av kontinenten", "[[Östantarktis]]");
                    p.text = p.text.Replace("den västra delen av kontinenten", "[[Västantarktis]]");
                    p.text = p.text.Replace("den norra delen av kontinenten", "[[Sydshetlandsöarna]]");
                    p.text = p.text.Replace("den södra delen av kontinenten", "[[Sydorkneyöarna]]");
                    if (p.text.Contains("[[Östantarktis]]"))
                        p.AddToCategory(make_catname(catcode, "Östantarktis", false));
                    if (p.text.Contains("[[Västantarktis]]"))
                        p.AddToCategory(make_catname(catcode, "Västantarktis", false));
                    if (p.text.Contains("[[Sydshetlandsöarna]]"))
                        p.AddToCategory("Sydshetlandsöarna");
                    if (p.text.Contains("[[Sydorkneyöarna]]"))
                        p.AddToCategory("Sydorkneyöarna");
                    if (p.text.Contains("[[Norge]]"))
                        p.AddToCategory("Norges anspråk i Antarktis");
                    if (p.text.Contains("[[Storbritannien]]"))
                        p.AddToCategory("Storbritanniens anspråk i Antarktis");
                    if (p.text.Contains("[[Chile]]"))
                        p.AddToCategory("Chiles anspråk i Antarktis");
                    if (p.text.Contains("[[Argentina]]"))
                        p.AddToCategory("Argentinas anspråk i Antarktis");
                    if (p.text.Contains("[[Frankrike]]"))
                        p.AddToCategory("Frankrikes anspråk i Antarktis");
                    if (p.text.Contains("[[Australien]]"))
                        p.AddToCategory("Australiens anspråk i Antarktis");
                    if (p.text.Contains("[[Nya Zeeland]]"))
                        p.AddToCategory("Nya Zeelands anspråk i Antarktis");

                    if (p.text.Contains("Kategori:Landformer på havets botten") || p.text.Contains("Kategori:Antarktis ö"))
                    {
                        p.text = p.text.Replace("Den ligger i ", "Den ligger i havet utanför ");
                        p.text = p.text.Replace("Det ligger i ", "Det ligger i havet utanför ");
                    }

                    p.SetTemplateParameter("geobox", "country_type", "Kontinent", true);
                }
                else if (makelang == "ceb")
                {
                    p.text = p.text.Replace("bahin sa nasod.", "bahin sa kontinente. " + sectortext);
                    p.SetTemplateParameter("geobox", "country_type", "Kontinente", true);

                }
            }


        }

        public static string enumeration(List<string> namelist)
        {
            int n = namelist.Count;
            if (n == 0)
                return "";
            else if (n == 1)
                return namelist[0];
            else
            {
                string rs = "";
                foreach (string name in namelist)
                {
                    if (n == 1)
                        rs += mp(97) + " ";
                    else if (n < namelist.Count)
                        rs += ", ";
                    rs += name;
                    n--;
                }
                return rs;
            }
        }

        public static string antarctic_sector(double lon)
        {
            List<string> claims = new List<string>();
            if ((lon <= -150) || (lon >= 160))
                claims.Add("NZ");
            if ((lon <= -25) && (lon >= -74))
                claims.Add("AR");
            if ((lon >= 142) && (lon <= 160))
                claims.Add("AU");
            if ((lon >= 45) && (lon <= 136))
                claims.Add("AU");
            if ((lon >= 136) && (lon <= 142))
                claims.Add("FR");
            if ((lon >= -20) && (lon <= 45))
                claims.Add("NO");
            if ((lon <= -28) && (lon >= -53))
                claims.Add("BR");
            if ((lon <= -53) && (lon >= -90))
                claims.Add("CL");
            if ((lon <= -20) && (lon >= -80))
                claims.Add("GB");

            List<string> claimnames = new List<string>();
            foreach (string cc in claims)
            {
                string countrynameml = countrydict[countryid[cc]].Name;
                if (countryml.ContainsKey(countrynameml))
                    countrynameml = countryml[countrynameml];
                claimnames.Add("[[" + countrynameml + "]]");
            }

            string[] p210 = new string[1] { mp(211) };
            if (claimnames.Count > 0)
                p210[0] = enumeration(claimnames);

            return mp(210, p210);


        }

        public static void fix_sizecats()
        {
            int icountry = countryid[makecountry];
            string countrynameml = countrydict[icountry].Name;
            if (countryml.ContainsKey(countrynameml))
                countrynameml = countryml[countrynameml];

            string towncat = make_catname("populated places", countrynameml, true);
            PageList pl = new PageList(makesite);
            PageList pl1 = new PageList(makesite);
            pl.FillFromCategoryTree(towncat);

            foreach (Page p in pl)
            {
                tryload(p, 2);
                string origtext = p.text;

                double areaout = -1;
                long popout = -1;
                int heightout = -1;

                get_page_area_pop_height(p, out areaout, out popout, out heightout);

                double dpop = popout;
                cat_by_size(p, "populated places", countrynameml, dpop, false);

                if (origtext != p.text)
                {
                    trysave(p, 2,mp(305,null));

                    //Console.WriteLine("<ret>");
                    //Console.ReadLine();
                }
            }
        }

        public static void fix_sizecats2()
        {
            int icountry = countryid[makecountry];
            string countrynameml = countrydict[icountry].Name;
            if (countryml.ContainsKey(countrynameml))
                countrynameml = countryml[countrynameml];

            string towncat = make_catname("populated places", countrynameml, true);
            string tcbad1 = "Orter i " + countrynameml + " större än 100 kvadratkilometer";
            string tcbad2 = "Orter i " + countrynameml + " större än 1000 kvadratkilometer";
            PageList pl = new PageList(makesite);
            PageList pl1 = new PageList(makesite);
            pl.FillFromCategoryTree(towncat);

            foreach (Page p in pl)
            {
                tryload(p, 2);
                string origtext = p.text;

                double areaout = -1;
                long popout = -1;
                int heightout = -1;

                get_page_area_pop_height(p, out areaout, out popout, out heightout);

                p.RemoveFromCategory(tcbad1);
                p.RemoveFromCategory(tcbad2);

                double dpop = popout;
                cat_by_size(p, "populated places", countrynameml, dpop, false);

                if (origtext != p.text)
                {
                    trysave(p, 2, mp(305));

                    //Console.WriteLine("<ret>");
                    //Console.ReadLine();
                }
            }


        }

        public static string oldsizecat(Page p, string catcode, string countrynameml, double size, bool is_area)
        {
            double[] heightsize = { 200.0, 500.0, 1000.0, 2000.0, 4000.0, 6000.0 };
            int sizecat = -1;
            double catsize = -1;
            int imax = heightsize.Length;

            for (int i = 0; i < imax; i++)
            {
                if (size >= heightsize[i])
                {
                    sizecat = i;
                    catsize = heightsize[i];
                }
            }
            if (sizecat < 0)
                return "";

            //Console.WriteLine("catcode = " + catcode);
            string[] p176 = { categoryml[catcode], countrynameml, catsize.ToString("F0", nfi) };
            string catname = "";
            int imp = 177;
            catname = initialcap(mp(imp, p176));
            return catname;
        }

        public static string tostringsize(double size)
        {
            if (size > 9000.0)
                return size.ToString("N0", nfi_space);
            else
                return size.ToString("F0", nfi);

        }

        public static void cat_by_size(Page p, string catcode, string countrynameml, double size, bool is_area)
        {
            double[] areasize = { 1.0, 2.0, 5.0, 10.0, 100.0, 1000.0 };
            //double[] heightsize = { 200.0, 500.0, 1000.0, 2000.0, 4000.0, 6000.0 };
            double[] heightsize = { 200.0, 500.0, 1000.0, 2000.0, 3000.0, 4000.0, 5000.0, 6000.0, 7000.0, 8000.0 };
            double[] popsize = { 3000.0, 10000.0, 30000.0, 100000.0, 300000.0, 1000000.0 };

            //Console.WriteLine("popsize[5].ToString() "+popsize[5].ToString());
            //Console.WriteLine("popsize[5].ToString(nfi) " + popsize[5].ToString(nfi));
            //Console.WriteLine("popsize[5].ToString(F0,nfi) " + popsize[5].ToString("F0",nfi));
            //Console.WriteLine("popsize[5].ToString(F0,nfi_en) " + popsize[5].ToString("F0", nfi_en));
            //Console.WriteLine("popsize[5].ToString(N0,nfi) " + popsize[5].ToString("N0", nfi));
            //Console.WriteLine("popsize[5].ToString(N0,nfi_space) " + popsize[5].ToString("N0", nfi_space));
            //Console.WriteLine("popsize[0].ToString(N0,nfi_space) " + popsize[0].ToString("N0", nfi_space));
            //Console.ReadLine();


            int sizecat = -1;
            double catsize = -1;
            int imax = popsize.Length;
            if (is_area)
                imax = areasize.Length;
            else if (catcode == "mountains")
                imax = heightsize.Length;

            for (int i = 0; i < imax; i++)
            {
                if (is_area)
                {
                    if (size > areasize[i])
                    {
                        sizecat = i;
                        catsize = areasize[i];
                    }
                }
                else if (catcode == "mountains")
                {
                    if (size >= heightsize[i])
                    {
                        sizecat = i;
                        catsize = heightsize[i];
                    }
                }
                else
                {
                    if (size >= popsize[i])
                    {
                        sizecat = i;
                        catsize = popsize[i];
                    }
                }
            }
            if (sizecat < 0)
                return;

            string[] p176 = { categoryml[catcode], countrynameml, tostringsize(catsize) };
            string catname = "";
            int imp = 217;
            if (is_area)
                imp = 217;
            else if (catcode == "mountains")
                imp = 218;
            else
                imp = 216;
            catname = initialcap(mp(imp, p176));
            p.AddToCategory(catname);
            catname = mp(1) + catname;
            while (sizecat >= 0)
            {
                Page pcat = new Page(makesite, catname);
                tryload(pcat, 1);
                if (pcat.Exists())
                    break;
                string incat = "";
                if (sizecat > 0)
                {
                    if (is_area)
                    {
                        p176[2] = tostringsize(areasize[sizecat - 1]);
                    }
                    else if (catcode == "mountains")
                    {
                        p176[2] = tostringsize(heightsize[sizecat - 1]);
                    }
                    else
                    {
                        p176[2] = tostringsize(popsize[sizecat - 1]);
                    }
                    incat = mp(imp, p176);
                }
                else
                    incat = make_catname(catcode, countrynameml, true) + "| ";
                incat = initialcap(incat);
                pcat.text = mp(120);
                pcat.AddToCategory(incat);
                string worldcat = catname.Replace(mp(75) + " " + countrynameml, "") + "|" + countrynameml;
                pcat.AddToCategory(worldcat);
                trysave(pcat, 2, makesite.defaultEditComment + " " + mp(1));
                catname = mp(1) + incat;
                sizecat--;
            }

        }

        public static void cat_by_size(Page p, string catcode, string countrynameml, double area)
        {
            cat_by_size(p, catcode, countrynameml, area, true);
        }

        public static void cat_by_size(Page p, string catcode, string countrynameml, int elevation)
        {
            double elev = elevation;
            cat_by_size(p, catcode, countrynameml, elev, false);
        }



        public static void make_articles()
        {
            makesite.defaultEditComment = mp(60) + " " + countryml[makecountryname];

            if (pstats == null)
            {
                pstats = new Page(makesite, mp(13) + botname + "/Statistik");
                pstats.Load();
            }
            pstats.text += "\n\n== [[" + countryml[makecountryname] + "]] ==\n\n";
            trysave(pstats, 1, mp(302) + " " + countryml[makecountryname]);

            string[] p295 = new string[] { countryml[makecountryname] };
            Page pcat = new Page(makesite, mp(1) + mp(295, p295));
            tryload(pcat, 1);
            if (!pcat.Exists())
            {
                pcat.text = "[[" + mp(1) + mp(296, p295) + "]]";
                trysave(pcat, 2);
            }


            if (makecountry == "AQ") //Antarctica
                minimum_population = 5;
            else
                minimum_population = 100;

            int iremain = gndict.Count;
            int iremain0 = iremain;

            foreach (int gnid in gndict.Keys)
            {
                iremain--;
                if ((resume_at > 0) && (resume_at != gnid))
                {
                    stats.Addskip();
                    continue;
                }
                else
                    resume_at = -1;

                if (stop_at == gnid)
                    break;

                reflist = "<references>";
                refnamelist.Clear();
                try
                {
                    make_article(gnid);
                }
                catch (OutOfMemoryException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    return;
                }

                Console.WriteLine(iremain.ToString() + " remaining.");

                if (firstround && (iremain0 - iremain < 5))
                {
                    Console.WriteLine("<cr>");
                    Console.ReadLine();
                }
            }

            Console.WriteLine(stats.GetStat());
            if (pstats == null)
            {
                pstats = new Page(makesite, mp(13) + botname + "/Statistik");
                pstats.Load();
            }
            //pstats.text += "\n\n== [[" + countryml[makecountryname] + "]] ==\n\n";
            pstats.text += stats.GetStat();
            trysave(pstats, 1, mp(302) + " " +countryml[makecountryname]);
            stats.ClearStat();

        }

        public static void make_specific_articles()
        {
            makesite.defaultEditComment = mp(60) + " " + countryml[makecountryname];

            while (true)
            {
                Console.Write("Gnid: ");
                string gnidstring = Console.ReadLine();
                reflist = "<references>";
                refnamelist.Clear();

                make_article(tryconvert(gnidstring));

                //Console.WriteLine("<cr>");
                //Console.ReadLine();
            }
        }

        public static void remake_article_set()
        {
            Console.WriteLine("In remake_article_set");
            PageList pl = new PageList(makesite);
            PageList pl1 = new PageList(makesite);

            //Find articles from a category
            //pl.FillAllFromCategoryTree("Geografi i Goiás");
            //pl1.FillAllFromCategoryTree("Eufriesea");
            //foreach (Page p in pl1)
            //    pl.Add(p);
            //pl1.FillAllFromCategoryTree("Euglossa");
            //foreach (Page p in pl1)
            //    pl.Add(p);
            //pl1.FillAllFromCategoryTree("Eulaema");
            //foreach (Page p in pl1)
            //    pl.Add(p);
            //pl1.FillAllFromCategoryTree("Exaerete");
            //foreach (Page p in pl1)
            //    pl.Add(p);
            //pl.FillFromCategory("Robotskapade Finlandförgreningar");

            //Find subcategories of a category
            //pl.FillSubsFromCategory("Svampars vetenskapliga namn");

            //Find articles from all the links to an article, mostly useful on very small wikis
            //pl.FillFromLinksToPage("Användare:Lsjbot/Algoritmer");

            //Find articles containing a specific string
            pl.FillFromSearchResults("insource:/och [A-Z][a-z]+språkiga Wikipedia./", 4999);
            //pl.FillFromSearchResults("insource:\"http://www.itis.gov;http://\"", 4999);

            //Set specific article:
            //Page pp = new Page(site, "Citrontrogon");pl.Add(pp);

            //Skip all namespaces except articles:
            pl.RemoveNamespaces(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 100, 101 });

            Console.WriteLine("In remake_article_set. Pl.Count = " + pl.Count());

            foreach (Page p in pl)
            {
                tryload(p, 2);
                if (!p.Exists())
                    continue;

                if (!p.text.Contains("obotskapad"))
                    continue;

                int gnid = get_gnid_from_article(p);

                if (!gndict.ContainsKey(gnid))
                    continue;

                if (human_touched(p, makesite))
                    continue;

                reflist = "<references>";
                refnamelist.Clear();

                make_article(gnid);
            }

        }

        public static int get_gnid_from_article(Page p)
        {
            foreach (string gs in p.GetTemplateParameter(mp(173), "gnid"))
                if (tryconvert(gs) > 0)
                    return tryconvert(gs);

            return -1;
        }

        public static void verify_geonames()
        {

            int n = 0;
            int n1 = 0;
            int ngnid = 0;
            using (StreamWriter sw = new StreamWriter("gnvswiki_pop.txt"))
            {

                foreach (int gnid in gndict.Keys)
                {
                    ngnid++;
                    if ((gndict[gnid].population > 0) && (gndict[gnid].population_wd > 0))
                    {
                        n1++;
                        sw.WriteLine(gndict[gnid].Name + tabstring + gndict[gnid].population_wd.ToString() + tabstring + gndict[gnid].population.ToString());
                        if ((n1 % 1000) == 0)
                            Console.WriteLine("n1 = " + n1.ToString());
                    }
                }
                if ((n % 100000) == 0)
                    Console.WriteLine("n = " + n.ToString());
            }
            Console.WriteLine("n (pop) = " + n1.ToString());
            n = 0;
            using (StreamWriter sw = new StreamWriter("gnvswiki_area.txt"))
            {

                foreach (double wdarea in areavsarea.Keys)
                {
                    n++;
                    sw.WriteLine(wdarea.ToString() + tabstring + areavsarea[wdarea].ToString());
                }
                if ((n % 10000) == 0)
                    Console.WriteLine("n = " + n.ToString());
            }
            Console.WriteLine("n(area) = " + n.ToString());
            n = 0;
            using (StreamWriter sw = new StreamWriter("gnvswiki_duplicates.txt"))
            {

                foreach (int wdid in wdgniddict.Keys)
                {
                    if (wdgniddict[wdid] < 0)
                    {
                        n++;
                        sw.WriteLine(wdid.ToString() + tabstring + (-wdgniddict[wdid]).ToString());
                    }
                }
                if ((n % 10000) == 0)
                    Console.WriteLine("n = " + n.ToString());
            }
            Console.WriteLine("n(duplicate) = " + n.ToString());
            Console.WriteLine("nwdtot = " + nwdtot.ToString());
            Console.WriteLine("ngnid = " + ngnid.ToString());

            //verify_wd();

        }

        public static int[,] get_hgt_array(string filename)
        {

            Console.WriteLine("get_hgt_array: " + filename);
            int pixvalue = 0;
            int oldpix = 0;
            int mapsize = 1201;
            int[,] map = new int[mapsize, mapsize];

            try
            {
                FileInfo finfo = new FileInfo(filename);
                Console.WriteLine("File size = "+finfo.Length);
                if (finfo.Length > 3000000)
                {
                    Console.WriteLine("Weird file size <cr>");
                    Console.ReadLine();
                }
                //Console.ReadLine();
                byte[] pixels = File.ReadAllBytes(filename);

                Console.WriteLine("pixels = " + pixels.Length);
                int x = 0;
                int y = 0;
                bool odd = true;
                //bool negative = false;
                foreach (byte b in pixels)
                {
                    if (odd)
                    {
                        //if (b < 128)
                        pixvalue = b;
                        //else
                        //{
                        //    negative = true;
                        //    pixvalue = b - 128;
                        //}
                        odd = !odd;
                    }
                    else
                    {
                        //if ( b < 128 )
                        pixvalue = pixvalue * 256 + b;
                        //else
                        //    pixvalue = -(pixvalue * 256 + b - 128);
                        //if (negative)
                        //    pixvalue = -pixvalue;
                        if (pixvalue > 32768)
                            pixvalue = pixvalue - 65536;
                        else if (pixvalue > 9000)
                            pixvalue = oldpix;
                        //Console.WriteLine(pixvalue);
                        map[x, y] = pixvalue;
                        oldpix = pixvalue;
                        x++;
                        if (x >= mapsize)
                        {
                            x = 0;
                            y++;
                        }
                        odd = !odd;
                        //negative = false;
                    }
                }


            }
            catch (FileNotFoundException e)
            {
                Console.Error.WriteLine(e.Message);
                //Console.WriteLine("Not found!");
                for (int x = 0; x < mapsize; x++)
                    for (int y = 0; y < mapsize; y++)
                        map[x, y] = 0;
            }
            catch (OutOfMemoryException e)
            {
                Console.Error.WriteLine(e.Message);
                //Console.WriteLine("Not found!");
                for (int x = 0; x < mapsize; x++)
                    for (int y = 0; y < mapsize; y++)
                        map[x, y] = 0;
            }
            return map;
        }

        public static string padint(int n, int len)
        {
            string s = n.ToString();
            while (s.Length < len)
                s = "0" + s;
            return s;
        }

        public static string nexthgt(string filenamepar, string dir)
        {
            //Console.WriteLine("nexthgt: filename before = " + filenamepar);
            string filename = filenamepar;

            switch (dir)
            {
                case "north":
                    if (filename.Contains("N"))
                    {
                        int lat = tryconvert(filename.Substring(1, 2));
                        lat++;
                        filename = filename.Replace(filename.Substring(0, 3), "N" + padint(lat, 2));
                    }
                    else //"S"
                    {
                        int lat = tryconvert(filename.Substring(1, 2));
                        if (lat > 1)
                        {
                            lat--;
                            filename = filename.Replace(filename.Substring(0, 3), "S" + padint(lat, 2));
                        }
                        else
                        {
                            filename = filename.Replace(filename.Substring(0, 3), "N00");
                        }
                    }
                    break;
                case "south":
                    if (filename.Contains("S"))
                    {
                        int lat = tryconvert(filename.Substring(1, 2));
                        lat++;
                        filename = filename.Replace(filename.Substring(0, 3), "S" + padint(lat, 2));
                    }
                    else //"N"
                    {
                        int lat = tryconvert(filename.Substring(1, 2));
                        if (lat > 0)
                        {
                            lat--;
                            filename = filename.Replace(filename.Substring(0, 3), "N" + padint(lat, 2));
                        }
                        else
                        {
                            filename = filename.Replace(filename.Substring(0, 3), "S01");
                        }
                    }
                    break;
                case "east":
                    if (filename.Contains("E"))
                    {
                        int lon = tryconvert(filename.Substring(4, 3));
                        lon++;
                        if (lon >= 180)
                            filename = filename.Replace(filename.Substring(3, 4), "W180");
                        else
                            filename = filename.Replace(filename.Substring(3, 4), "E" + padint(lon, 3));
                    }
                    else //"W"
                    {
                        int lon = tryconvert(filename.Substring(4, 3));
                        if (lon > 1)
                        {
                            lon--;
                            filename = filename.Replace(filename.Substring(3, 4), "W" + padint(lon, 3));
                        }
                        else
                        {
                            filename = filename.Replace(filename.Substring(3, 4), "E000");
                        }
                    }
                    break;
                case "west":
                    if (filename.Contains("W"))
                    {
                        int lon = tryconvert(filename.Substring(4, 3));
                        lon++;
                        if (lon > 180)
                            filename = filename.Replace(filename.Substring(3, 4), "E179");
                        else
                            filename = filename.Replace(filename.Substring(3, 4), "W" + padint(lon, 3));
                    }
                    else //"E"
                    {
                        int lon = tryconvert(filename.Substring(4, 3));
                        if (lon > 0)
                        {
                            lon--;
                            filename = filename.Replace(filename.Substring(3, 4), "E" + padint(lon, 3));
                        }
                        else
                        {
                            filename = filename.Replace(filename.Substring(3, 4), "W001");
                        }
                    }
                    break;
            }

            //Console.WriteLine("nexthgt: filename after = " + filename);
            return filename;
        }

        public static string make_hgt_filename(double lat, double lon)
        {
            int intlat = Convert.ToInt32(Math.Abs(Math.Floor(lat)));
            int intlon = Convert.ToInt32(Math.Abs(Math.Floor(lon)));

            string filename = "N00E999.hgt";

            if (lat < 0)
                filename = filename.Replace('N', 'S');

            if (lon < 0)
                filename = filename.Replace('E', 'W');

            filename = filename.Replace("00", padint(intlat, 2));
            filename = filename.Replace("999", padint(intlon, 3));

            Console.WriteLine(filename);
            return filename;

        }

        public static int[,] get_9x9map(double lat, double lon)
        {
            //int[,] centermap = get_3x3map(lat, lon);

            int map3x3size = 3603; //centermap.GetLength(0);

            int[,] map = new int[3 * map3x3size, 3 * map3x3size];
            for (int x = 0; x < 3 * map3x3size; x++)
                for (int y = 0; y < 3 * map3x3size; y++)
                    map[x, y] = 0;

            int xoff = map3x3size;
            int yoff = map3x3size;

            for (int u = -1; u <= 1; u++)
                for (int v = -1; v <= 1; v++)
                {
                    int[,] map3x3 = get_3x3map(lat - 3 * u, lon + 3 * v);


                    for (int x = 0; x < map3x3size; x++)
                        for (int y = 0; y < map3x3size; y++)
                            map[x + (u + 1) * xoff, y + (v + 1) * yoff] = map3x3[x, y];

                }

            return map;

        }

        public static int[,] get_3x3map(double lat, double lon)
        {
            int mapsize = 1201;

            string dir = extractdir;
            string filename = make_hgt_filename(lat, lon);

            int[,] map;

            if (filename == mapfilecache)
                map = mapcache;
            else
            {
                mapfilecache = filename;
                Console.WriteLine("Garbage collection:");
                GC.Collect();
                Console.WriteLine("Making map array..."+mapsize);
                map = new int[3 * mapsize, 3 * mapsize];
                Console.WriteLine("Map array done.");
                for (int x = 0; x < 3 * mapsize; x++)
                    for (int y = 0; y < 3 * mapsize; y++)
                        map[x, y] = 0;


                // ...
                // .x.
                // ...

                Console.WriteLine("Getting first map square...");
                int[,] map0 = get_hgt_array(dir + filename);

                int xoff = mapsize;
                int yoff = mapsize;

                for (int x = 0; x < mapsize; x++)
                    for (int y = 0; y < mapsize; y++)
                        map[x + xoff, y + yoff] = map0[x, y];

                // ...
                // x..
                // ...

                Console.WriteLine("Getting 2nd map square...");
                filename = nexthgt(filename, "west");
                map0 = get_hgt_array(dir + filename);

                xoff = 0;
                yoff = mapsize;

                for (int x = 0; x < mapsize; x++)
                    for (int y = 0; y < mapsize; y++)
                        map[x + xoff, y + yoff] = map0[x, y];

                // x..
                // ...
                // ...

                filename = nexthgt(filename, "north");
                map0 = get_hgt_array(dir + filename);

                xoff = 0;
                yoff = 0;

                for (int x = 0; x < mapsize; x++)
                    for (int y = 0; y < mapsize; y++)
                        map[x + xoff, y + yoff] = map0[x, y];

                // .x.
                // ...
                // ...

                filename = nexthgt(filename, "east");
                map0 = get_hgt_array(dir + filename);

                xoff = mapsize;
                yoff = 0;

                for (int x = 0; x < mapsize; x++)
                    for (int y = 0; y < mapsize; y++)
                        map[x + xoff, y + yoff] = map0[x, y];

                // ..x
                // ...
                // ...

                filename = nexthgt(filename, "east");
                map0 = get_hgt_array(dir + filename);

                xoff = 2 * mapsize;
                yoff = 0;

                for (int x = 0; x < mapsize; x++)
                    for (int y = 0; y < mapsize; y++)
                        map[x + xoff, y + yoff] = map0[x, y];

                // ...
                // ..x
                // ...


                filename = nexthgt(filename, "south");
                map0 = get_hgt_array(dir + filename);

                xoff = 2 * mapsize;
                yoff = mapsize;

                for (int x = 0; x < mapsize; x++)
                    for (int y = 0; y < mapsize; y++)
                        map[x + xoff, y + yoff] = map0[x, y];

                // ...
                // ...
                // ..x

                filename = nexthgt(filename, "south");
                map0 = get_hgt_array(dir + filename);

                xoff = 2 * mapsize;
                yoff = 2 * mapsize;

                for (int x = 0; x < mapsize; x++)
                    for (int y = 0; y < mapsize; y++)
                        map[x + xoff, y + yoff] = map0[x, y];

                // ...
                // ...
                // .x.

                filename = nexthgt(filename, "west");
                map0 = get_hgt_array(dir + filename);

                xoff = mapsize;
                yoff = 2 * mapsize;

                for (int x = 0; x < mapsize; x++)
                    for (int y = 0; y < mapsize; y++)
                        map[x + xoff, y + yoff] = map0[x, y];

                // ...
                // ...
                // x..

                filename = nexthgt(filename, "west");
                map0 = get_hgt_array(dir + filename);

                xoff = 0;
                yoff = 2 * mapsize;

                for (int x = 0; x < mapsize; x++)
                    for (int y = 0; y < mapsize; y++)
                        map[x + xoff, y + yoff] = map0[x, y];

                mapcache = map;
            }

            return map;
        }

        public static int get_x_pixel(double lon, double orilon)
        {
            return get_x_pixel(lon, orilon, 1201);
        }

        public static int get_x_pixel(double lon, double orilon, int mapsize) //mapsize should be one third of actual mapsize!
        {
            double fraction = lon - Math.Floor(lon);
            int pix = Convert.ToInt32((Math.Floor(lon) - Math.Floor(orilon) + 1) * mapsize + mapsize * fraction);
            return pix;
        }

        public static int get_y_pixel(double lat, double orilat)
        {
            return get_y_pixel(lat, orilat, 1201);
        }

        public static int get_y_pixel(double lat, double orilat, int mapsize) //mapsize should be one third of actual mapsize!
        {
            double fraction = lat - Math.Floor(lat);
            int pix = 3 * mapsize - Convert.ToInt32((Math.Floor(lat) - Math.Floor(orilat) + 1) * mapsize + mapsize * fraction);
            return pix;
        }


        public static long seed_center_dist(int gnid) //verify island by calculating distance between seed point and center of gravity
        {
            double lat = gndict[gnid].latitude;
            double lon = gndict[gnid].longitude;
            double scale = Math.Cos(lat * 3.1416 / 180);
            double pixkmx = scale * 40000 / (360 * 1200);
            double pixkmy = 40000.0 / (360.0 * 1200.0);

            Console.WriteLine("scale,pixkmx,pixkmy = " + scale.ToString() + "; " + pixkmx.ToString() + "; " + pixkmy.ToString());
            int[,] mainmap = get_3x3map(lat, lon);

            int mapsize = mainmap.GetLength(0);

            byte[,] fillmap = new byte[mapsize, mapsize];

            for (int x = 0; x < mapsize; x++)
                for (int y = 0; y < mapsize; y++)
                    fillmap[x, y] = 1;

            int x0 = get_x_pixel(lon, lon);
            int y0 = get_y_pixel(lat, lat);
            floodfill(ref fillmap, ref mainmap, x0, y0, 0, 0, false);

            if (fillmap[0, 0] == 3) //fill failure
                return 99999999;

            long xsum = 0;
            long ysum = 0;
            int nfill = 0;

            for (int x = 0; x < mapsize; x++)
                for (int y = 0; y < mapsize; y++)
                    if (fillmap[x, y] == 2)
                    {
                        nfill++;
                        xsum += x;
                        ysum += y;
                    }
            if (nfill == 0) //fill failure
                return 99999999;

            long xc = xsum / nfill;
            long yc = ysum / nfill;

            return (x0 - xc) * (x0 - xc) + (y0 - yc) * (y0 - yc);

        }


        public static bool inmap(int x, int y, int size, int margin)
        {
            if (x < margin)
                return false;
            if (y < margin)
                return false;
            if (x > size - margin - 1)
                return false;
            if (y > size - margin - 1)
                return false;

            return true;
        }

        public static bool filldirland(ref byte[,] fillmap, ref int[,] mainmap, int xx, int yy, int dirx, int diry, ref int nnew, int level)
        {
            bool atedge = false;
            if ((fillmap[xx + dirx, yy + diry] == 1) && (mainmap[xx + dirx, yy + diry] > level))
            {
                fillmap[xx + dirx, yy + diry] = 2;
                nnew++;
                int u = 2;
                while (inmap(xx + u * dirx, yy + u * diry, mainmap.GetLength(0), 2) && (mainmap[xx + u * dirx, yy + u * diry] > level))
                {
                    fillmap[xx + u * dirx, yy + u * diry] = 2;
                    nnew++;
                    u++;
                }
                if (!inmap(xx + u * dirx, yy + u * diry, mainmap.GetLength(0), 2))
                    atedge = true;
            }
            return atedge;
        }

        public static bool filldirlake(ref byte[,] fillmap, ref int[,] mainmap, int xx, int yy, int dirx, int diry, ref int nnew, int level)
        {
            bool atedge = false;
            if ((fillmap[xx + dirx, yy + diry] == 1) && (mainmap[xx + dirx, yy + diry] == level))
            {
                fillmap[xx + dirx, yy + diry] = 2;
                nnew++;
                int u = 2;
                while (inmap(xx + u * dirx, yy + u * diry, mainmap.GetLength(0), 2) && (mainmap[xx + u * dirx, yy + u * diry] == level))
                {
                    fillmap[xx + u * dirx, yy + u * diry] = 2;
                    nnew++;
                    u++;
                }
                if (!inmap(xx + u * dirx, yy + u * diry, mainmap.GetLength(0), 2))
                    atedge = true;
            }
            return atedge;
        }



        public static void floodfill(ref byte[,] fillmap, ref int[,] mainmap, int x, int y, int level, int depth, bool exactlevel)
        {
            Console.WriteLine("flood " + x.ToString() + " " + y.ToString() + " " + mainmap[x, y].ToString() + " " + depth);
            if ((x < 0) || (x >= mainmap.GetLength(0)))
            {
                fillmap[0, 0] = 3; //bad fill
                Console.WriteLine("Invalid x");
                return;
            }
            if ((y < 0) || (y >= mainmap.GetLength(1)))
            {
                fillmap[0, 0] = 3; //bad fill
                Console.WriteLine("Invalid y");
                return;
            }

            if (fillmap[x, y] != 1) //1 = unchecked
            {
                Console.WriteLine("Starting point checked.");
                return;
            }

            bool atedge = false;

            if (exactlevel) //find all at same level
            {
                if (mainmap[x, y] == level)
                {
                    fillmap[x, y] = 2; //2 = filled
                    int nnew = 0;
                    int nfill = 0;
                    do
                    {
                        nnew = 0;
                        nfill = 0;
                        for (int xx = 1; xx < mainmap.GetLength(0) - 1; xx++)
                            for (int yy = 1; yy < mainmap.GetLength(0) - 1; yy++)
                            {
                                if (fillmap[xx, yy] == 2)
                                {
                                    nfill++;
                                    if (!inmap(xx, yy, mainmap.GetLength(0), 2))
                                        atedge = true;

                                    if (!atedge)
                                    {
                                        atedge = atedge || filldirlake(ref fillmap, ref mainmap, xx, yy, 1, 0, ref nnew, level);
                                        atedge = atedge || filldirlake(ref fillmap, ref mainmap, xx, yy, -1, 0, ref nnew, level);
                                        atedge = atedge || filldirlake(ref fillmap, ref mainmap, xx, yy, 0, 1, ref nnew, level);
                                        atedge = atedge || filldirlake(ref fillmap, ref mainmap, xx, yy, 0, -1, ref nnew, level);

                                    }
                                }
                            }
                        Console.WriteLine("nnew = " + nnew.ToString() + ", nfill = " + nfill.ToString());
                    }
                    while ((nnew > 0) && (!atedge));

                }
                else
                    fillmap[x, y] = 0; //0 = checked but wrong level
            }
            else //find all ABOVE the given level
            {
                if (mainmap[x, y] > level)
                {
                    fillmap[x, y] = 2;
                    //floodfill(x - 1, y, level, depth + 1);
                    //floodfill(x + 1, y, level, depth + 1);
                    //floodfill(x, y - 1, level, depth + 1);
                    //floodfill(x, y + 1, level, depth + 1);
                    int nnew = 0;
                    int nfill = 0;
                    do
                    {
                        nnew = 0;
                        nfill = 0;
                        for (int xx = 1; xx < mainmap.GetLength(0) - 1; xx++)
                            for (int yy = 1; yy < mainmap.GetLength(0) - 1; yy++)
                            {
                                if (fillmap[xx, yy] == 2)
                                {
                                    nfill++;
                                    if (!inmap(xx, yy, mainmap.GetLength(0), 2))
                                        atedge = true;

                                    if (!atedge)
                                    {
                                        atedge = atedge || filldirland(ref fillmap, ref mainmap, xx, yy, 1, 0, ref nnew, level);
                                        atedge = atedge || filldirland(ref fillmap, ref mainmap, xx, yy, -1, 0, ref nnew, level);
                                        atedge = atedge || filldirland(ref fillmap, ref mainmap, xx, yy, 0, 1, ref nnew, level);
                                        atedge = atedge || filldirland(ref fillmap, ref mainmap, xx, yy, 0, -1, ref nnew, level);

                                    }
                                }
                            }
                        Console.WriteLine("nnew = " + nnew.ToString() + ", nfill = " + nfill.ToString());
                    }
                    while ((nnew > 0) && (!atedge));
                }
                else
                    fillmap[x, y] = 0;
            }

            if (atedge)
                fillmap[0, 0] = 3;
        }

        public static string kml_header(string name)
        {
            return "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n<kml xmlns=\"http://www.opengis.net/kml/2.2\">\n<Document><Folder><name>#NAME#</name>\n".Replace("#NAME#", name);
        }

        public static string kml_placemark(List<coordclass> points, string description, string source, string name)
        {
            return kml_placemark(points, description, source, name, "green");
        }

        public static string kml_placemark(List<coordclass> points, string description, string source, string name, string colorparam)
        {
            string color = colorparam;
            switch (colorparam)
            {
                case "red":
                    color = "ff0000ff";
                    break;
                case "green":
                    color = "5014f000";
                    break;
                case "blue":
                    color = "50f01414";
                    break;
                case "yellow":
                    color = "5014f0ff";
                    break;
                case "orange":
                    color = "501478ff";
                    break;
                case "purple":
                    color = "50780078";
                    break;
                case "turqoise":
                    color = "50F0FF14";
                    break;
                case "black":
                    color = "50000000";
                    break;
                    
            }
            string placeheader = ("<Placemark>\n	<description>#DESCRIPTION#</description>\n	<Style><LineStyle><color>" + color + "</color></LineStyle><PolyStyle><fill>0</fill></PolyStyle></Style>\n	<ExtendedData><SchemaData schemaUrl=\"#SCHEMA#\">\n		<SimpleData name=\"Source\">#SOURCE#</SimpleData>\n	</SchemaData></ExtendedData>").Replace("#DESCRIPTION#", description).Replace("#SCHEMA#", name).Replace("#SOURCE#", source);
            string polygonheader = "<Polygon><altitudeMode>relativeToGround</altitudeMode><outerBoundaryIs><LinearRing><altitudeMode>relativeToGround</altitudeMode><coordinates>";
            string polygonend = "</coordinates></LinearRing></outerBoundaryIs></Polygon>\n";
            string placeend = "</Placemark>";

            string coordstring = "";
            foreach (coordclass cc in points)
                coordstring += cc.lon.ToString(culture_en) + "," + cc.lat.ToString(culture_en) + " ";
            coordstring = coordstring.Trim();

            return placeheader + polygonheader + coordstring + polygonend + placeend;
        }

        public static string kml_end(string name, string category)
        {
            return "</Folder>\n<Schema name=\"#NAME#\" id=\"#NAME#\">\n\t<SimpleField name=\"Name\" type=\"string\"></SimpleField>\n	<SimpleField name=\"Description\" type=\"string\"></SimpleField>\n	<SimpleField name=\"Source\" type=\"string\"></SimpleField>\n</Schema>//[[Kategori:#KATEGORI#]]\n</Document></kml>".Replace("#NAME#", name).Replace("#KATEGORI#", category);
        }

        public static string make_kml_file(List<coordclass> points, string description, string source, string name, string category)
        {
            string kmlfilename = kmlprefix + countryml[makecountryname] + "/" + name;
            kmlfilename = kmlfilename.Replace(" ", "_");
            Page p = new Page(makesite, kmlfilename);
            p.text = kml_header(name) + kml_placemark(points, description, source, name) + kml_end(name, category);
            trysave(p, 2, mp(60,null)+" KML");
            return kmlfilename;
        }

        public static bool in_map(int x, int y, int mapsize)
        {
            if (x < 1)
                return false;
            if (x > mapsize - 2)
                return false;
            if (y < 1)
                return false;
            if (y > mapsize - 2)
                return false;

            return true;
        }

        public static void make_rivers() //create rivers-XX file for a country
        {
            int nriver = 0;

            using (StreamWriter sw = new StreamWriter("rivers-" + makecountry + ".txt"))
            {

                int ngnid = gndict.Count;

                double minlat = 999;
                double maxlat = -999;
                double minlon = 999;
                double maxlon = -999;


                //Page pkr = new Page(makesite, "Användare:Lsjbot/Kartrutor");
                //tryload(pkr, 2);
                //pkr.text += "\n== Vattendrag i " + countryml[makecountryname] + " ==\n";

                foreach (int gnid in gndict.Keys)
                {
                    if (categorydict[gndict[gnid].featurecode] == "streams")
                    {
                        if (gndict[gnid].latitude < minlat)
                            minlat = gndict[gnid].latitude;
                        else if (gndict[gnid].latitude > maxlat)
                            maxlat = gndict[gnid].latitude;
                        if (gndict[gnid].longitude < minlon)
                            minlon = gndict[gnid].longitude;
                        else if (gndict[gnid].longitude > maxlon)
                            maxlon = gndict[gnid].longitude;
                    }
                }

                int ndone = 0;
                List<int> donelist = new List<int>();
                List<coordclass> mapcoord = new List<coordclass>();

                double slon = minlon;
                double slat = maxlat; //northeast corner

                while (slon < maxlon)
                {
                    while (slat > minlat)
                    {
                        coordclass cc = new coordclass();
                        cc.lon = slon;
                        cc.lat = slat;
                        mapcoord.Add(cc);
                        slat -= 3.0;
                    }
                    slat = maxlat;
                    slon += 3.0;
                }

                Console.WriteLine("n mapcoord = " + mapcoord.Count);

                foreach (coordclass mc in mapcoord)
                {
                    int[,] mainmap = get_9x9map(mc.lat, mc.lon);
                    Console.WriteLine("Map loaded");
                    int mapsize = mainmap.GetLength(0);
                    int fiducialsize = mapsize / 3;
                    int[,] fillmap = new int[mapsize, mapsize];
                    int[,] rivermap = new int[mapsize, mapsize];
                    int nullvalue = -1;
                    for (int x = 0; x < mapsize; x++)
                    {
                        for (int y = 0; y < mapsize; y++)
                        {
                            fillmap[x, y] = nullvalue;
                            rivermap[x, y] = nullvalue;
                        }
                    }

                    Console.WriteLine("put_category_on_map");
                    put_category_on_map(ref fillmap, mc.lat, mc.lon, "streams", 0.4);
                    put_category_on_map(ref rivermap, mc.lat, mc.lon, "streams", 0.4);
                    int nmark = 0;
                    for (int x0 = fiducialsize; x0 < 2 * fiducialsize; x0++)
                        for (int y0 = fiducialsize; y0 < 2 * fiducialsize; y0++)
                        {
                            if (rivermap[x0, y0] > 0)
                                nmark++;
                        }
                    Console.WriteLine("nmark = " + nmark);
                    Console.WriteLine("list_category_in_map");
                    List<int> fiducial_rivers = list_category_in_map(fiducialsize / 3, mc.lat, mc.lon, "streams");
                    Console.WriteLine(fiducial_rivers.Count + " fiducial rivers");
                    List<int> found_rivers = new List<int>();
                    int fillvalue = -2;
                    int deadvalue = -3;
                    int endvalue = -1;
                    int edgevalue = -4;
                    int tolerance = 3; //maximum "uphill" step of river

                    for (int x0 = fiducialsize; x0 < 2 * fiducialsize; x0++)
                        for (int y0 = fiducialsize; y0 < 2 * fiducialsize; y0++)
                        {
                            if (rivermap[x0, y0] > 0)
                            {
                                fillmap[x0, y0] = rivermap[x0, y0];
                                //Console.WriteLine("river at starting point");
                                continue;
                            }
                            if (mainmap[x0, y0] <= 0) //skip ocean
                                continue;
                            if (fillmap[x0, y0] != nullvalue) //done already
                                continue;

                            int x = x0;
                            int y = y0;
                            int maxx = x;
                            int minx = x;
                            int maxy = y;
                            int miny = y;
                            endvalue = -1;

                            while (true) //breaks inside loop //((fillmap[x, y] == nullvalue) && (rivermap[x, y] == nullvalue))
                            {
                                int h = 99999;

                                fillmap[x, y] = fillvalue;
                                int lowx = -1;
                                int lowy = -1;
                                for (int u = -1; u <= 1; u++)
                                    for (int v = -1; v <= 1; v++)
                                    {
                                        if ((u != 0) || (v != 0))
                                            if ((mainmap[x + u, y + v] <= h) && (fillmap[x + u, y + v] != fillvalue))
                                            {
                                                h = mainmap[x + u, y + v];
                                                lowx = x + u;
                                                lowy = y + v;
                                            }
                                    }
                                //Console.WriteLine("x,y,h = " + x + " " + y + " " + h);
                                if ((h > mainmap[x, y] + tolerance)) //reached dead end
                                {
                                    endvalue = deadvalue;
                                    break;
                                }
                                else if (rivermap[lowx, lowy] > 0) //found river
                                {
                                    endvalue = rivermap[lowx, lowy];
                                    break;
                                }
                                else if (mainmap[lowx, lowy] <= 0) // reached ocean
                                {
                                    endvalue = 0;
                                    break;
                                }
                                else if (fillmap[lowx, lowy] != nullvalue) //reached previous filling
                                {
                                    endvalue = fillmap[lowx, lowy];
                                    break;
                                }
                                else if (!in_map(lowx, lowy, mapsize)) //reached edge of map
                                {
                                    endvalue = edgevalue;
                                    break;
                                }
                                else
                                {
                                    x = lowx;
                                    y = lowy;
                                    fillmap[x, y] = fillvalue;
                                    if (x > maxx)
                                        maxx = x;
                                    if (x < minx)
                                        minx = x;
                                    if (y > maxy)
                                        maxy = y;
                                    if (y < miny)
                                        miny = y;

                                }
                            }

                            if (endvalue > 0)
                            {
                                Console.WriteLine("x0,y0 = " + x0 + " " + y0 + " endvalue: " + endvalue);
                                if (!found_rivers.Contains(endvalue))
                                    found_rivers.Add(endvalue);
                            }

                            //Console.ReadLine();

                            for (int xx = minx; xx <= maxx; xx++)
                                for (int yy = miny; yy <= maxy; yy++)
                                {
                                    if (fillmap[xx, yy] == fillvalue)
                                        fillmap[xx, yy] = endvalue;
                                }
                        }
                    Console.WriteLine(fiducial_rivers.Count + " fiducial rivers");

                    Console.WriteLine(found_rivers.Count + " found rivers");

                }
            }
        }

#if (DBGEOFLAG)
        public static DbGeography polygon_from_coords(List<coordclass> points)
        {
            string s = "POLYGON ((";
            bool first = true;
            foreach (coordclass cc in points)
            {
                if (first)
                {
                    first = false;
                }
                else
                    s += ", ";

                s += cc.lon.ToString(culture_en) + " " + cc.lat.ToString(culture_en);
            }

            s += "))";

            return DbGeography.FromText(s);
        }
#endif


        public static void make_lakes() //create lakes-XX file for a country
        {
            int nlake = 0;
            //int npop = 0;
            //int narea = 0;

#if (DBGEOFLAG)
            //make_glwd_countries();
            read_lakeshapes();

            hbookclass areahist = new hbookclass();
            areahist.SetBins(-1, 1, 20);
#endif
            using (StreamWriter sw = new StreamWriter("lakes-" + makecountry + ".txt"))
            {

                int ngnid = gndict.Count;

                double minlat = 999;
                double maxlat = -999;
                double minlon = 999;
                double maxlon = -999;


                Page pkr = new Page(makesite, "Användare:Lsjbot/Kartrutor2");
                tryload(pkr, 2);
                pkr.text += "\n== Sjöar i " + countryml[makecountryname] + " ==\n";

                foreach (int gnid in gndict.Keys)
                {
                    if (categorydict[gndict[gnid].featurecode] == "lakes")
                    {
                        if (gndict[gnid].latitude < minlat)
                            minlat = gndict[gnid].latitude;
                        else if (gndict[gnid].latitude > maxlat)
                            maxlat = gndict[gnid].latitude;
                        if (gndict[gnid].longitude < minlon)
                            minlon = gndict[gnid].longitude;
                        else if (gndict[gnid].longitude > maxlon)
                            maxlon = gndict[gnid].longitude;
                    }
                }

                int ndone = 0;
                int nround = 0;
                List<int> donelist = new List<int>();
                do
                {
                    ndone = 0;
                    ngnid = gndict.Count;
                    nround++;

                    string current_map = "";
                    int[,] donemap = new int[3603, 3603];
                    for (int x = 0; x < 3603; x++)
                        for (int y = 0; y < 3603; y++)
                            donemap[x, y] = -1;

                    string placemarks = "";

                    foreach (int gnid in gndict.Keys)
                    {
                        Console.WriteLine("=====" + makecountry + "==== " + nround + " ==== " + ngnid.ToString() + " remaining. ===========");
                        ngnid--;
                        if ((ngnid % 100000) == 0)
                        {
                            Console.WriteLine("Garbage collection:");
                            GC.Collect();
                        }

                        if (resume_at > 0)
                            if (resume_at != gnid)
                                continue;
                            else
                            {
                                resume_at = -1;
                                //Console.WriteLine("<cr>");
                                //Console.ReadLine();
                            }


                        if (categorydict[gndict[gnid].featurecode] == "lakes")
                        {
                            if (donelist.Contains(gnid))
                                continue;

                            nlake++;

                            double area = -1.0;
                            double lat = gndict[gnid].latitude;
                            double lon = gndict[gnid].longitude;
                            double scale = Math.Cos(lat * 3.1416 / 180);
                            double pixkmx = scale * 40000 / (360 * 1200);
                            double pixkmy = 40000.0 / (360.0 * 1200.0);

                            string mapname = make_hgt_filename(lat, lon); //do only those using the same map; loop over maps
                            Console.WriteLine(mapname);
                            if (String.IsNullOrEmpty(current_map))
                            {
                                Console.WriteLine("Starting new map");
                                current_map = mapname;
                            }
                            else if (mapname != current_map)
                                continue;

                            ndone++;
                            donelist.Add(gnid);

                            string kmlf = kmlprefix + countryml[makecountryname] + "/" + gndict[gnid].articlename;
                            Page pkml = new Page(makesite, kmlf);
                            tryload(pkml, 1);
                            if (pkml.Exists() && !overwrite)
                            {
                                continue;
                            }

                            //=================================================
                            //Console.WriteLine("scale,pixkmx,pixkmy = " + scale.ToString() + "; " + pixkmx.ToString() + "; " + pixkmy.ToString());
                            int[,] mainmap = get_3x3map(lat, lon);

                            int mapsize = mainmap.GetLength(0);

                            byte[,] fillmap = new byte[mapsize, mapsize];

                            for (int x = 0; x < mapsize; x++)
                                for (int y = 0; y < mapsize; y++)
                                    fillmap[x, y] = 1;

                            int x0 = get_x_pixel(lon, lon);
                            int y0 = get_y_pixel(lat, lat);
                            floodfill(ref fillmap, ref mainmap, x0, y0, mainmap[x0, y0], 0, true);

                            if (fillmap[0, 0] == 3) //fill failure
                                continue;

                            int xmax = -1;
                            int ymax = -1;
                            int xmin = 99999;
                            int ymin = 99999;
                            double r2max = -1;
                            int overlaps_with = -1;

                            byte edgevalue = 3;
                            byte fillvalue = 2;


                            int nfill = 0;
                            for (int x = 0; x < mapsize; x++)
                                for (int y = 0; y < mapsize; y++)
                                    if (fillmap[x, y] == fillvalue)
                                    {
                                        nfill++;
                                        if (x > xmax)
                                            xmax = x;
                                        if (y > ymax)
                                            ymax = y;
                                        if (x < xmin)
                                            xmin = x;
                                        if (y < ymin)
                                            ymin = y;
                                        double r2 = scale * scale * (x - x0) * (x - x0) + (y - y0) * (y - y0);
                                        if (r2 > r2max)
                                            r2max = r2;
                                        if (donemap[x, y] > 0)
                                            overlaps_with = donemap[x, y];
                                        else
                                            donemap[x, y] = gnid;
                                    }

                            double kmew = (xmax - xmin + 1) * pixkmx;
                            double kmns = (ymax - ymin + 1) * pixkmy;

                            Console.WriteLine("nfill = " + nfill.ToString());

                            int minpixel = 5;
                            if (nfill < minpixel) //skip lakes with just a few pixels
                                continue;

                            //area per pixel:
                            double km2perpixel = pixkmx * pixkmy;
                            area = nfill * km2perpixel;


                            double rmax = Math.Sqrt(r2max) * pixkmy + 2; //r2max in pixels; rmax in km
                            Console.WriteLine("r2max, rmax = " + r2max.ToString() + "; " + rmax.ToString());

                            List<int> nblist = getneighbors(gnid, rmax);
                            List<int> inlake = new List<int>();
                            List<int> aroundlake = new List<int>();

                            foreach (int nb in nblist) //first find islands and stuff in lake
                            {
                                if ((categorydict[gndict[nb].featurecode] != "islands") && (categorydict[gndict[nb].featurecode] != "seabed"))
                                    continue;
                                int xnb = get_x_pixel(gndict[nb].longitude, lon);
                                if ((xnb < 0) || (xnb >= mapsize))
                                    continue;
                                int ynb = get_y_pixel(gndict[nb].latitude, lat);
                                if ((ynb < 0) || (ynb >= mapsize))
                                    continue;
                                if (fillmap[xnb, ynb] == fillvalue)
                                    inlake.Add(nb);
                                else
                                {
                                    bool atedge = false;
                                    int u = 0;
                                    while ((xnb + u < mapsize) && (fillmap[xnb + u, ynb] != fillvalue))
                                        u++;
                                    if (xnb + u >= mapsize)
                                        atedge = true;
                                    else
                                    {
                                        u = 0;
                                        while ((xnb - u >= 0) && (fillmap[xnb - u, ynb] != fillvalue))
                                            u--;
                                        if (xnb - u < 0)
                                            atedge = true;
                                        else
                                        {
                                            u = 0;
                                            while ((ynb + u < mapsize) && (fillmap[xnb, ynb + u] != fillvalue))
                                                u++;
                                            if (ynb + u >= mapsize)
                                                atedge = true;
                                            else
                                            {
                                                u = 0;
                                                while ((ynb - u >= 0) && (fillmap[xnb, ynb - u] != fillvalue))
                                                    u--;
                                                if (ynb - u < 0)
                                                    atedge = true;
                                            }
                                        }
                                    }
                                    if (!atedge)
                                        inlake.Add(nb);

                                }

                            }

                            int maxdist = 20; //count things up to maxdist pixels away from the lake as "around" the lake
                            if (r2max < 300)   //smaller zone "around" if lake is smaller
                                maxdist = 10;
                            if (r2max < 30)
                                maxdist = 5;

                            for (byte step = edgevalue; step < maxdist + edgevalue; step++) //start at 3, because base level at 2.
                            {
                                for (int x = 1; x < mapsize - 1; x++)
                                    for (int y = 1; y < mapsize - 1; y++)
                                        if (fillmap[x, y] == step - 1)
                                        {
                                            for (int uu = -1; uu <= 1; uu++)
                                                for (int vv = -1; vv <= 1; vv++)
                                                {
                                                    if (fillmap[x + uu, y + vv] < fillvalue)
                                                        fillmap[x + uu, y + vv] = step;
                                                }
                                        }
                            }


                            foreach (int nb in nblist) //now things around the lake
                            {
                                if ((categorydict[gndict[nb].featurecode] == "islands") || (categorydict[gndict[nb].featurecode] == "seabed"))
                                    continue;
                                int xnb = get_x_pixel(gndict[nb].longitude, lon);
                                if ((xnb < 0) || (xnb >= mapsize))
                                    continue;
                                int ynb = get_y_pixel(gndict[nb].latitude, lat);
                                if ((ynb < 0) || (ynb >= mapsize))
                                    continue;
                                if (fillmap[xnb, ynb] > fillvalue)
                                    aroundlake.Add(nb);

                            }

                            //Check so not too many pixels around lake are below lake level. Also find max/min of contour and prepare for KML contour
                            int lower = 0;
                            int higher = 0;
                            //int xmax3 = -9999;
                            //int xmin3 = 9999;
                            //int ymax3 = -9999;
                            //int ymin3 = 9999;
                            int n3 = 0;

                            for (int x = 1; x < mapsize - 1; x++)
                                for (int y = 1; y < mapsize - 1; y++)
                                    if (fillmap[x, y] == edgevalue)
                                    {
                                        n3++;
                                        if (mainmap[x, y] > mainmap[x0, y0])
                                            higher++;
                                        else if (mainmap[x, y] < mainmap[x0, y0])
                                            lower++;
                                    }


                            List<coordclass> kmllist2 = make_kmllist(ref fillmap, edgevalue, fillvalue, x0, y0, gnid);

                            string kmlcategory = "Robotskapade KML-filer " + countryml[makecountryname];

                            string kmlfilename = make_kml_file(kmllist2, "Edge of the lake " + gndict[gnid].Name_ml, "Lsjbot using altitude data from Viewfinder Panorama", gndict[gnid].articlename, kmlcategory);

                            string color = "green";
                            if (overlaps_with > 0)
                                color = "red";
                            else if (higher < 4 * lower)
                                color = "orange";
                            else if (higher < 15 * lower)
                                color = "yellow";
                            else if (area < 0.1 * kmns * kmew)
                                color = "purple";
                            else if (area < 0.25 * kmns * kmew)
                                color = "blue";
                            else if (gndict[gnid].roundminute)
                                color = "turqoise";

                            //if ((color == "green") || (color == "blue") || (color == "purple") || (color == "turqoise"))
                            //{
                                int cgnid = countryid[makecountry];
                                int glwd_found = -1;
                                double glwd_area = 0;
                                
                                if ( countrylakedict.ContainsKey(cgnid))
                                {
                                    //DbGeography dlc = DbGeography.FromText("POINT ("+lon.ToString(culture_en)+" "+lat.ToString(culture_en)+")");
                                    DbGeography dlc = polygon_from_coords(kmllist2);

                                    foreach (int glwd_id in countrylakedict[cgnid])
                                    {
                                        foreach (DbGeography dg in lakeshapedict[glwd_id].shapes)
                                            if (dlc.Intersects(dg))
                                            {
                                                Console.WriteLine("Match found");
                                                glwd_found = glwd_id;
                                            }

                                    }
                                }
                                if (glwd_found > 0)
                                {
                                    glwd_area = (double)lakeshapedict[glwd_found].shapes[0].Area/1000000;
                                    Console.WriteLine("My area = "+area.ToString()+", glwd-area = "+glwd_area.ToString());
                                    double mismatch = (glwd_area - area) / (glwd_area + area);
                                    areahist.Add(mismatch);
                                    if (Math.Abs(mismatch) < 0.2)
                                        color = "purple";
                                    
                                }
                                else
                                {
                                    Console.WriteLine("No match");
                                    if ((color == "green") || (color == "blue") || (color == "purple") || (color == "turqoise"))
                                        color = "black";
                                }
                            //}
                            //Console.ReadLine();


                            Console.WriteLine("area,kmns,kmew " + area.ToString("N3") + " " + kmew.ToString("N3") + " " + kmns.ToString("N3") + " " + color);
                            //Console.ReadLine();

                            placemarks += kml_placemark(kmllist2, "Edge of the lake " + gndict[gnid].Name_ml, "Lsjbot using altitude data from Viewfinder Panorama", gndict[gnid].articlename, color);

                            if ((color == "green") || (color == "blue") || (color == "purple") || (color == "turqoise") || (color == "black"))
                            {
                                Page p = new Page(makesite, gndict[gnid].articlename);
                                tryload(p, 1);
                                if (p.Exists())
                                {
                                    if (p.text.Contains(mp(195)))
                                    {
                                        if (!p.text.Contains("{{KML"))
                                        {
                                            p.text += "\n{{KML|sida=" + kmlfilename + "}}";
                                            trysave(p, 2, mp(306));
                                        }
                                        else if (p.text.Contains("Wikipedia:KML/Robotskapade_KML-filer/"))
                                        {
                                            p.text = p.text.Replace("Wikipedia:KML/Robotskapade_KML-filer/", kmlprefix);
                                            trysave(p, 2, mp(306));
                                        }
                                    }
                                }
                            }
                            //fillmap.Dispose();

                            //Console.WriteLine("<ret>");
                            //Console.ReadLine();
                            Console.WriteLine(gndict[gnid].Name + "; " + area.ToString() + "; " + kmew.ToString() + "; " + kmns.ToString() + "; " + inlake.Count.ToString() + "; " + aroundlake.Count.ToString());
                            sw.Write(gnid.ToString() + tabstring + area.ToString() + tabstring + kmew.ToString() + tabstring + kmns.ToString() + tabstring + higher.ToString() + tabstring + lower.ToString() + tabstring + overlaps_with + tabstring + glwd_found.ToString() + tabstring + glwd_area.ToString() + tabstring + "in");
                            foreach (int il in inlake)
                            {
                                sw.Write(tabstring + il.ToString());
                                //Console.WriteLine(gndict[il].Name + " in lake");
                            }
                            sw.Write(tabstring + "around");
                            foreach (int al in aroundlake)
                            {
                                sw.Write(tabstring + al.ToString());
                                //Console.WriteLine(gndict[al].Name + " around lake");
                            }
                            sw.WriteLine();
                            //Console.ReadLine();
                        }
                    }

                    if (!String.IsNullOrEmpty(placemarks))
                    {
                        string pmapname = kmlprefix + "Kartrutor/" + makecountry + "-sjö-" + current_map.Replace(".hgt", "");
                        Page pmap = new Page(makesite, pmapname);
                        pmap.text = kml_header(current_map) + placemarks + kml_end(current_map, "Robotskapade kartrutor");
                        trysave(pmap, 2,mp(60,null)+" KML");
                        pkr.text += "\n" + current_map.Replace(".hgt", "") + " {{KML-nowd|sida=" + pmapname + "}}\n";
                        trysave(pkr, 1,mp(60,null)+" KML");
                    }
                    else
                    {
                        Console.WriteLine("No placemarks");
                        //Console.ReadLine();
                    }
                }
                while (ndone > 0);
            }

            areahist.PrintDHist();
        }

        public static void right_turn(ref int up, ref int vp)
        {
            if (up == 0)
            {
                up = vp;
                vp = 0;
            }
            else //vp = 0
            {
                vp = -up;
                up = 0;
            }
        }

        public static void left_turn(ref int up, ref int vp)
        {
            if (up == 0)
            {
                up = -vp;
                vp = 0;
            }
            else //vp = 0
            {
                vp = up;
                up = 0;
            }
        }

        public static coordclass edgepoint(int x, int y, int x0, int y0, int gnid, int ua, int va)
        {
            coordclass cc = new coordclass();

            int uc = 0;
            int vc = 0;

            if (ua == 0)
            {
                if (va == 1)
                {
                    uc = 1;
                    vc = -1;
                }
                else
                {
                    uc = 0;
                    vc = 0;
                }
            }
            else if (ua == 1)
            {
                uc = 0;
                vc = -1;
            }
            else
            {
                uc = 1;
                vc = 0;
            }



            double one1200 = 1.0 / 1200.0;
            double dlon = (x - x0 + uc) * one1200;
            double dlat = -(y - y0 + vc) * one1200; //reverse sign because higher pixel number is lower latitude
            cc.lat = gndict[gnid].latitude + dlat;
            cc.lon = gndict[gnid].longitude + dlon;

            return cc;
        }

        public static List<coordclass> make_kmllist(ref byte[,] fillmap, int edgevalue, int fillvalue, int x0, int y0, int gnid)
        {
            int xmax3 = -9999;
            int xmin3 = 9999;
            int ymax3 = -9999;
            int ymin3 = 9999;
            int n3 = 0;

            int mapsize = fillmap.GetLength(0);

            for (int x = 1; x < mapsize - 1; x++)
                for (int y = 1; y < mapsize - 1; y++)
                    if (fillmap[x, y] == edgevalue)
                    {
                        n3++;

                        if (x > xmax3)
                            xmax3 = x;
                        if (x < xmin3)
                            xmin3 = x;
                        if (y > ymax3)
                            ymax3 = y;
                        if (y < ymin3)
                            ymin3 = y;
                    }

            int x0kml = -1;
            int y0kml = -1;
            Console.WriteLine("n3 = " + n3);
            Console.WriteLine("xmin3 = " + xmin3);
            Console.WriteLine("xmax3 = " + xmax3);
            Console.WriteLine("ymin3 = " + ymin3);
            Console.WriteLine("ymax3 = " + ymax3);
            byte donevalue = 60;
            List<coordclass> kmllist1 = new List<coordclass>();
            List<coordclass> kmllist2 = new List<coordclass>();
            //double one1200 = 1.0 / 1200.0;
            //double dlon;
            //double dlat;
            bool foundedge = false;

            foundedge = false;
            for (int y = ymin3; y <= ymax3; y++) //find a starting point for kml
                for (int x = xmin3; x <= xmax3; x++) //find a starting point for kml
                {
                    if (fillmap[x, y] == edgevalue)
                    {
                        x0kml = x;
                        y0kml = y;
                        foundedge = true;
                        break;
                    }
                }
            if (!foundedge)
                return kmllist2;

            coordclass ccend = new coordclass();
            int xkml = x0kml;
            int ykml = y0kml;

            int up = -999; //perpendicular to shore
            int vp = -999;
            int ua = -999; //along shore
            int va = -999;

            int nround = 0;
            do
            {
                nround++;
                Console.WriteLine("nround,x,y = " + nround + " " + xkml + " " + ykml);

                if (up < -1) //first round, get starting direction
                {
                    bool founduv = false;
                    for (int uu = -1; uu <= 1; uu++)
                        for (int vv = -1; vv <= 1; vv++)
                            if (uu * vv == 0)
                                if (fillmap[xkml + uu, ykml + vv] == fillvalue)
                                {
                                    up = uu;
                                    vp = vv;
                                    founduv = true;
                                }
                    if (!founduv) //move to another edge square and try again
                    {
                        int newx = -999;
                        int newy = -999;
                        bool foundnewxy = false;
                        for (int uu = -1; uu <= 1; uu++)
                            for (int vv = -1; vv <= 1; vv++)
                                if (fillmap[xkml + uu, ykml + vv] == edgevalue)
                                {
                                    newx = xkml + uu;
                                    newy = ykml + vv;
                                    foundnewxy = true;
                                }
                        if (!foundnewxy)
                            break;
                        else
                        {
                            xkml = newx;
                            ykml = newy;
                            continue;
                        }
                    }
                    else //normal case, lake square next to edge square in up-vp-direction
                    {
                        //get direction along shore
                        if (vp == 0)
                        {
                            va = up;
                            ua = 0;
                        }
                        else //up == 0
                        {
                            ua = -vp;
                            va = 0;
                        }

                        x0kml = xkml;
                        y0kml = ykml;

                        kmllist1.Add(edgepoint(xkml, ykml, x0, y0, gnid, ua, va));
                        ccend = edgepoint(xkml, ykml, x0, y0, gnid, ua, va); //save copy for ending point
                    }
                }

                //normal iteration now:

                if (fillmap[xkml + up + ua, ykml + vp + va] == edgevalue) //right turn
                {
                    xkml = xkml + up + ua;
                    ykml = ykml + vp + va;

                    right_turn(ref up, ref vp);
                    right_turn(ref ua, ref va);

                    kmllist1.Add(edgepoint(xkml, ykml, x0, y0, gnid, ua, va));
                }
                else if ((fillmap[xkml + ua, ykml + va] == edgevalue) && (fillmap[xkml + up + ua, ykml + vp + va] == fillvalue)) //straight ahead
                {
                    xkml = xkml + ua;
                    ykml = ykml + va;
                }
                else if (fillmap[xkml + ua, ykml + va] == fillvalue) //left turn
                {

                    left_turn(ref up, ref vp);
                    left_turn(ref ua, ref va);

                    kmllist1.Add(edgepoint(xkml, ykml, x0, y0, gnid, ua, va));
                }
                else
                {
                    Console.WriteLine("Something wrong");
                    Console.ReadLine();
                }

            }
            while (!((xkml == x0kml) && (ykml == y0kml)));

            //Add final point equal to first point, in order to get closed loop:
            kmllist1.Add(ccend);


            coordclass ccfirst = new coordclass();
            coordclass ccprev = new coordclass();
            coordclass ccfirstmid = new coordclass();

            bool first = true;
            bool firstmid = true;

            foreach (coordclass cc3 in kmllist1)
            {
                if (first)
                {
                    ccfirst.lat = cc3.lat;
                    ccfirst.lon = cc3.lon;
                    first = false;
                }
                else
                {
                    coordclass ccmid = new coordclass();
                    ccmid.lat = 0.5 * (ccprev.lat + cc3.lat);
                    ccmid.lon = 0.5 * (ccprev.lon + cc3.lon);
                    kmllist2.Add(ccmid);
                    if (firstmid)
                    {
                        ccfirstmid.lat = ccmid.lat;
                        ccfirstmid.lon = ccmid.lon;
                        firstmid = false;
                    }
                }
                ccprev.lat = cc3.lat;
                ccprev.lon = cc3.lon;
            }

            kmllist2.Add(ccfirstmid); //add first point again, to close loop

            return kmllist2;
        }

        public static List<coordclass> make_kmllist2(ref byte[,] fillmap, int edgevalue, int x0, int y0, int gnid) //try to follow middle of edge-pixels
        {
            int xmax3 = -9999;
            int xmin3 = 9999;
            int ymax3 = -9999;
            int ymin3 = 9999;
            int n3 = 0;

            int mapsize = fillmap.GetLength(0);

            for (int x = 1; x < mapsize - 1; x++)
                for (int y = 1; y < mapsize - 1; y++)
                    if (fillmap[x, y] == edgevalue)
                    {
                        n3++;

                        if (x > xmax3)
                            xmax3 = x;
                        if (x < xmin3)
                            xmin3 = x;
                        if (y > ymax3)
                            ymax3 = y;
                        if (y < ymin3)
                            ymin3 = y;
                    }

            int x0kml = -1;
            int y0kml = -1;
            Console.WriteLine("n3 = " + n3);
            Console.WriteLine("xmin3 = " + xmin3);
            Console.WriteLine("xmax3 = " + xmax3);
            Console.WriteLine("ymin3 = " + ymin3);
            Console.WriteLine("ymax3 = " + ymax3);
            byte donevalue = 60;
            List<coordclass> kmllist1 = new List<coordclass>();
            List<coordclass> kmllist2 = new List<coordclass>();
            double one1200 = 1.0 / 1200.0;
            double dlon;
            double dlat;
            bool foundedge = false;

            do
            {
                foundedge = false;
                for (int y = ymin3; y <= ymax3; y++) //find a starting point for kml
                    for (int x = xmin3; x <= xmax3; x++) //find a starting point for kml
                    {
                        if (fillmap[x, y] == edgevalue)
                        {
                            x0kml = x;
                            y0kml = y;
                            foundedge = true;
                            break;
                        }
                    }
                if (!foundedge)
                    break;

                int xkml = x0kml;
                int ykml = y0kml;

                bool found = false;
                bool backtrack = false;

                do
                {
                    found = false;
                    //Console.WriteLine("xkml = " + xkml);
                    //Console.WriteLine("ykml = " + ykml);

                    fillmap[xkml, ykml] = donevalue;
                    dlon = (xkml - x0 + 0.5) * one1200;
                    dlat = -(ykml - y0 + 0.5) * one1200; //reverse sign because higher pixel number is lower latitude
                    coordclass cc = new coordclass();
                    cc.lat = gndict[gnid].latitude + dlat;
                    cc.lon = gndict[gnid].longitude + dlon;
                    kmllist1.Add(cc);
                    int newx = -1;
                    int newy = -1;
                    for (int uu = -1; uu <= 1; uu++)
                        for (int vv = -1; vv <= 1; vv++)
                        {
                            if ((uu * vv == 0) || backtrack)
                            {
                                if (fillmap[xkml + uu, ykml + vv] == edgevalue)
                                {
                                    found = true;
                                    backtrack = false;
                                    newx = xkml + uu;
                                    newy = ykml + vv;
                                }
                            }
                        }
                    if (found)
                    {
                        xkml = newx;
                        ykml = newy;
                    }
                    else
                    {
                        bool foundorigin = false;
                        for (int uu = -1; uu <= 1; uu++)
                            for (int vv = -1; vv <= 1; vv++)
                            {
                                if ((xkml + uu == x0kml) && (ykml + vv == y0kml))
                                    foundorigin = true;
                            }
                        if (foundorigin)
                            break;

                        bool found3 = false;
                        for (int x = xmin3; x <= xmax3; x++) //find a starting point for kml
                            for (int y = ymin3; y <= ymax3; y++) //find a starting point for kml
                            {
                                if (fillmap[x, y] == edgevalue)
                                {
                                    found3 = true;
                                }
                            }

                        if (found3)
                        {
                            fillmap[xkml, ykml]++;
                            for (int uu = -1; uu <= 1; uu++)
                                for (int vv = -1; vv <= 1; vv++)
                                {
                                    if (uu * vv == 0)
                                    {
                                        if (fillmap[xkml + uu, ykml + vv] == donevalue)
                                        {
                                            found = true;
                                            backtrack = true;
                                            newx = xkml + uu;
                                            newy = ykml + vv;
                                        }
                                    }
                                }
                            xkml = newx;
                            ykml = newy;
                        }
                    }
                }
                while (found);
            }
            while (foundedge);

            //Add final point equal to first point, in order to get closed loop:
            dlon = (x0kml - x0 + 0.5) * one1200;
            dlat = -(y0kml - y0 + 0.5) * one1200; //reverse sign because higher pixel number is lower latitude
            coordclass cc2 = new coordclass();
            cc2.lat = gndict[gnid].latitude + dlat;
            cc2.lon = gndict[gnid].longitude + dlon;
            kmllist1.Add(cc2);

            coordclass ccfirst = new coordclass();
            coordclass ccprev = new coordclass();
            coordclass ccfirstmid = new coordclass();

            bool first = true;
            bool firstmid = true;

            foreach (coordclass cc3 in kmllist1)
            {
                if (first)
                {
                    ccfirst.lat = cc3.lat;
                    ccfirst.lon = cc3.lon;
                    first = false;
                }
                else
                {
                    coordclass ccmid = new coordclass();
                    ccmid.lat = 0.5 * (ccprev.lat + cc3.lat);
                    ccmid.lon = 0.5 * (ccprev.lon + cc3.lon);
                    kmllist2.Add(ccmid);
                    if (firstmid)
                    {
                        ccfirstmid.lat = ccmid.lat;
                        ccfirstmid.lon = ccmid.lon;
                        firstmid = false;
                    }
                }
                ccprev.lat = cc3.lat;
                ccprev.lon = cc3.lon;
            }

            kmllist2.Add(ccfirstmid); //add first point again, to close loop

            return kmllist2;
        }

        public static void check_islands() //create islands-XX file for a country
        {
            int nisl = 0;
            //int npop = 0;
            //int narea = 0;


            using (StreamWriter sw = new StreamWriter("islands-" + makecountry + ".txt"))
            {

                int ngnid = gndict.Count;

                foreach (int gnid in gndict.Keys)
                {
                    Console.WriteLine("=====" + makecountry + "======== " + ngnid.ToString() + " remaining. ===========");
                    ngnid--;
                    if ((ngnid % 1000) == 0)
                    {
                        Console.WriteLine("Garbage collection:");
                        GC.Collect();
                    }

                    if ((resume_at > 0) && (resume_at != gnid))
                        continue;
                    else
                        resume_at = -1;


                    if (categorydict[gndict[gnid].featurecode] == "islands")
                    {
                        nisl++;

                        double area = -1.0;
                        double lat = gndict[gnid].latitude;
                        double lon = gndict[gnid].longitude;
                        double scale = Math.Cos(lat * 3.1416 / 180);
                        double pixkmx = scale * 40000 / (360 * 1200);
                        double pixkmy = 40000.0 / (360.0 * 1200.0);

                        Console.WriteLine("scale,pixkmx,pixkmy = " + scale.ToString() + "; " + pixkmx.ToString() + "; " + pixkmy.ToString());
                        int[,] mainmap = get_3x3map(lat, lon);

                        int mapsize = mainmap.GetLength(0);

                        byte[,] fillmap = new byte[mapsize, mapsize];

                        for (int x = 0; x < mapsize; x++)
                            for (int y = 0; y < mapsize; y++)
                                fillmap[x, y] = 1;

                        int x0 = get_x_pixel(lon, lon);
                        int y0 = get_y_pixel(lat, lat);
                        floodfill(ref fillmap, ref mainmap, x0, y0, 0, 0, false);

                        if (fillmap[0, 0] == 3) //fill failure
                            continue;

                        int xmax = -1;
                        int ymax = -1;
                        int xmin = 99999;
                        int ymin = 99999;
                        double r2max = -1;

                        int nfill = 0;
                        for (int x = 0; x < mapsize; x++)
                            for (int y = 0; y < mapsize; y++)
                                if (fillmap[x, y] == 2)
                                {
                                    nfill++;
                                    if (x > xmax)
                                        xmax = x;
                                    if (y > ymax)
                                        ymax = y;
                                    if (x < xmin)
                                        xmin = x;
                                    if (y < ymin)
                                        ymin = y;
                                    double r2 = scale * scale * (x - x0) * (x - x0) + (y - y0) * (y - y0);
                                    if (r2 > r2max)
                                        r2max = r2;
                                }

                        double kmew = (xmax - xmin + 1) * pixkmx;
                        double kmns = (ymax - ymin + 1) * pixkmy;

                        Console.WriteLine("nfill = " + nfill.ToString());

                        if (nfill == 0)
                            continue;

                        double rmax = Math.Sqrt(r2max) * pixkmy; //r2max in pixels; rmax in km
                        Console.WriteLine("r2max, rmax = " + r2max.ToString() + "; " + rmax.ToString());

                        List<int> nblist = getneighbors(gnid, rmax);
                        List<int> onisland = new List<int>();

                        foreach (int nb in nblist)
                        {
                            int xnb = get_x_pixel(gndict[nb].longitude, lon);
                            if ((xnb < 0) || (xnb >= mapsize))
                                continue;
                            int ynb = get_y_pixel(gndict[nb].latitude, lat);
                            if ((ynb < 0) || (ynb >= mapsize))
                                continue;
                            if (fillmap[xnb, ynb] == 2)
                                onisland.Add(nb);
                        }

                        //area per pixel:
                        double km2perpixel = pixkmx * pixkmy;
                        area = nfill * km2perpixel;

                        //fillmap.Dispose();

                        //Console.WriteLine("<ret>");
                        //Console.ReadLine();
                        Console.WriteLine(gndict[gnid].Name + "; " + area.ToString() + "; " + kmew.ToString() + "; " + kmns.ToString() + "; " + onisland.Count.ToString());
                        sw.Write(gnid.ToString() + tabstring + area.ToString() + tabstring + kmew.ToString() + tabstring + kmns.ToString());
                        foreach (int oi in onisland)
                            sw.Write(tabstring + oi.ToString());
                        sw.WriteLine();
                    }
                }

            }


        }



        public static void make_ranges() //create ranges-XX file for a country
        {
            int nrange = 0;
            int nisland = 0;
            //int npop = 0;
            //int narea = 0;


            using (StreamWriter swname = new StreamWriter("rangenames-" + makecountry + ".txt"))
            using (StreamWriter sw = new StreamWriter("ranges-" + makecountry + ".txt"))
            {

                int ngnid = gndict.Count;

                foreach (int gnid in gndict.Keys)
                {
                    if ((gndict[gnid].featurecode == "MTS") || (gndict[gnid].featurecode == "HLLS"))
                    {
                        nrange++;
                    }
                    else if (categorydict[gndict[gnid].featurecode] == "islands")
                    {
                        nisland++;
                    }
                }

                if (nrange == 0)
                    return;


                foreach (int gnid in gndict.Keys)
                {
                    //if (gnid != 2700827)
                    //    continue;

                    Console.WriteLine("=====" + makecountry + "======== " + ngnid.ToString() + " remaining. ===========");
                    ngnid--;
                    if ((ngnid % 1000) == 0)
                    {
                        Console.WriteLine("Garbage collection:");
                        GC.Collect();
                    }

                    if ((resume_at > 0) && (resume_at != gnid))
                        continue;
                    else
                        resume_at = -1;


                    if ((gndict[gnid].featurecode == "MTS") || (gndict[gnid].featurecode == "HLLS"))
                    {
                        //nrange++;

                        //double area = -1.0;
                        double lat = gndict[gnid].latitude;
                        double lon = gndict[gnid].longitude;
                        double scale = Math.Cos(lat * 3.1416 / 180);
                        double pixkmx = scale * 40000 / (360 * 1200);
                        double pixkmy = 40000.0 / (360.0 * 1200.0);

                        //Console.WriteLine("scale,pixkmx,pixkmy = " + scale.ToString() + "; " + pixkmx.ToString() + "; " + pixkmy.ToString());
                        int[,] mainmap = get_3x3map(lat, lon);

                        int mapsize = mainmap.GetLength(0);

                        byte[,] fillmap = new byte[mapsize, mapsize];

                        for (int x = 0; x < mapsize; x++)
                            for (int y = 0; y < mapsize; y++)
                                fillmap[x, y] = 1;

                        int x0 = get_x_pixel(lon, lon);
                        int y0 = get_y_pixel(lat, lat);

                        long hsum = 0;
                        int nh = 0;
                        for (int x = 0; x < mapsize; x++)
                        {
                            for (int y = 0; y < mapsize; y++)
                            {
                                hsum += mainmap[x, y];
                                nh++;
                            }
                        }

                        double kmew = -1;
                        double kmns = -1;
                        double maxlength = -1;
                        List<int> inrange = new List<int>();
                        string rangedir = "....";
                        double angle = 999.9;
                        int hmax = -1;
                        double hlat = 999;
                        double hlon = 999;

                        int sealevel = 0;
                        int h0 = mainmap[x0, y0];
                        if (h0 <= 0) //range below sea level
                            continue;

                        long haverage = hsum / nh;
                        double levelfraction = -0.3; //suitable for mainland ranges
                        if (nrange == 1) //countries with single range; don't count the whole country
                            levelfraction = 0.2;

                        if (haverage < 10) //likely lots of ocean around; start higher
                            levelfraction = 0.2;

                        double levelstep = 0.1;

                        do
                        {
                            do
                            {
                                if (h0 > haverage)
                                {
                                    sealevel = (int)(levelfraction * h0 + (1 - levelfraction) * haverage);
                                }
                                else
                                {
                                    sealevel = Convert.ToInt32(h0 * levelfraction);
                                }
                                if (sealevel < 0)
                                    levelfraction += levelstep;
                            }
                            while (sealevel < 0);

                            Console.WriteLine("Base altitude = " + sealevel.ToString());

                            floodfill(ref fillmap, ref mainmap, x0, y0, sealevel, 0, false);

                            if (fillmap[0, 0] == 3) //fill failure
                            {
                                Console.WriteLine("Fill failure");
                                levelfraction += levelstep;
                                for (int x = 0; x < mapsize; x++)
                                    for (int y = 0; y < mapsize; y++)
                                        fillmap[x, y] = 1;
                                continue;
                            }

                            int xmax = -1;
                            int ymax = -1;
                            int xmin = 99999;
                            int ymin = 99999;
                            int xr2max = -1;
                            int yr2max = -1;
                            double r2max = -1;
                            double l2max = -1;
                            maxlength = -1;

                            int nfill = 0;
                            for (int x = 0; x < mapsize; x++)
                                for (int y = 0; y < mapsize; y++)
                                    if (fillmap[x, y] == 2)
                                    {
                                        nfill++;
                                        if (x > xmax)
                                            xmax = x;
                                        if (y > ymax)
                                            ymax = y;
                                        if (x < xmin)
                                            xmin = x;
                                        if (y < ymin)
                                            ymin = y;
                                        double r2 = scale * scale * (x - x0) * (x - x0) + (y - y0) * (y - y0);
                                        if (r2 > r2max)
                                        {
                                            r2max = r2;
                                            xr2max = x;
                                            yr2max = y;
                                        }
                                    }

                            kmew = (xmax - xmin + 1) * pixkmx;
                            kmns = (ymax - ymin + 1) * pixkmy;

                            int xfar = 0;
                            int yfar = 0;
                            for (int x = 0; x < mapsize; x++)
                                for (int y = 0; y < mapsize; y++)
                                    if (fillmap[x, y] == 2)
                                    {
                                        double r2 = scale * scale * (x - xr2max) * (x - xr2max) + (y - yr2max) * (y - yr2max);
                                        if (r2 > l2max)
                                        {
                                            l2max = r2;
                                            xfar = x;
                                            yfar = y;
                                        }
                                    }

                            if (l2max > 0)
                            {
                                maxlength = (Math.Sqrt(l2max) + 1) * pixkmy;
                                double roundarea = maxlength * maxlength * Math.PI / 4; //area of circle with diameter maxlength
                                double realarea = nfill * pixkmx * pixkmy; //actual area of range
                                double fillfraction = realarea / roundarea; //smaller fillfraction = elongate shape
                                //double one1200 = 1.0 / 1200.0;
                                double dx = (xfar - xr2max) * scale;
                                double dy = -(yfar - yr2max); //reverse sign because higher pixel number is lower latitude

                                angle = Math.Atan2(dy, dx);
                                //if (fillfraction < 0.5)
                                //{
                                //    if (Math.Abs(dx) > 2 * Math.Abs(dy))
                                //        rangedir = "EW..";
                                //    else if (Math.Abs(dy) > 2 * Math.Abs(dx))
                                //        rangedir = "NS..";
                                //    else if (dx * dy > 0)
                                //        rangedir = "SWNE";
                                //    else
                                //        rangedir = "SENW";
                                //}

                            }

                            Console.WriteLine("Maxlength = " + maxlength.ToString());


                            Console.WriteLine("nfill = " + nfill.ToString());

                            if (nfill == 0)
                                continue;

                            double rmax = Math.Sqrt(r2max) * pixkmy; //r2max in pixels; rmax in km
                            Console.WriteLine("r2max, rmax = " + r2max.ToString() + "; " + rmax.ToString());

                            List<int> nblist = getneighbors(gnid, rmax);
                            inrange.Clear();

                            bool badrange = false;
                            foreach (int nb in nblist)
                            {
                                if ((gndict[nb].featurecode == "MTS") || (gndict[nb].featurecode == "HLLS"))
                                {
                                    badrange = true;
                                    Console.WriteLine("Range in range");
                                    break;
                                }
                                if (!is_height(gndict[nb].featurecode))
                                    continue;
                                int xnb = get_x_pixel(gndict[nb].longitude, lon);
                                if ((xnb < 0) || (xnb >= mapsize))
                                    continue;
                                int ynb = get_y_pixel(gndict[nb].latitude, lat);
                                if ((ynb < 0) || (ynb >= mapsize))
                                    continue;
                                if (fillmap[xnb, ynb] == 2)
                                    inrange.Add(nb);
                            }

                            if (badrange)
                            {
                                levelfraction += levelstep;
                                for (int x = 0; x < mapsize; x++)
                                    for (int y = 0; y < mapsize; y++)
                                        fillmap[x, y] = 1;

                                continue;
                            }

                            hmax = 0;
                            int xhmax = 0;
                            int yhmax = 0;
                            for (int x = 0; x < mapsize; x++)
                                for (int y = 0; y < mapsize; y++)
                                {
                                    if (fillmap[x, y] == 2)
                                        if (mainmap[x, y] > hmax)
                                        {
                                            hmax = mainmap[x, y];
                                            xhmax = x;
                                            yhmax = y;
                                        }
                                }

                            int hnbmax = 0;
                            int nbmax = -1;
                            foreach (int nb in inrange)
                            {
                                if (!is_height(gndict[nb].featurecode))
                                    continue;
                                if (gndict[nb].elevation > hnbmax)
                                {
                                    hnbmax = gndict[nb].elevation;
                                    nbmax = nb;
                                }
                                int xnb = get_x_pixel(gndict[nb].longitude, lon);
                                if (xnb == xhmax)
                                {
                                    int ynb = get_y_pixel(gndict[nb].latitude, lat);
                                    if (ynb == yhmax)
                                        hmax = -nb; //negative to distinguish from heights

                                }
                            }

                            if (hnbmax >= 0.9 * hmax)
                                hmax = -nbmax;

                            if (hmax > 0)
                            {
                                double one1200 = 1.0 / 1200.0; //degrees per pixel
                                double dlon = (xhmax - x0) * one1200;
                                double dlat = -(yhmax - y0) * one1200; //reverse sign because higher pixel number is lower latitude
                                hlat = lat + dlat;
                                hlon = lon + dlon;
                            }
                            else if (gndict.ContainsKey(-hmax))
                            {
                                hlat = gndict[-hmax].latitude;
                                hlon = gndict[-hmax].longitude;
                            }

                            break;
                        }
                        while (sealevel < h0);
                        //area per pixel:
                        //double km2perpixel = pixkmx * pixkmy;
                        //area = nfill * km2perpixel;

                        //fillmap.Dispose();

                        //Console.WriteLine("<ret>");
                        //Console.ReadLine();

                        if (sealevel < h0)
                        {

                            Console.WriteLine(gndict[gnid].Name + "; " + maxlength.ToString() + "; " + kmew.ToString() + "; " + kmns.ToString() + "; " + inrange.Count.ToString());
                            if (inrange.Count > 1)
                            {
                                sw.Write(gnid.ToString() + tabstring + maxlength.ToString() + tabstring + kmew.ToString() + tabstring + kmns.ToString() + tabstring + angle.ToString() + tabstring + hmax.ToString() + tabstring + hlat.ToString() + tabstring + hlon.ToString());
                                foreach (int oi in inrange)
                                    sw.Write(tabstring + oi.ToString());
                                sw.WriteLine();
                                swname.Write("* [[" + gndict[gnid].Name_ml + "]]: " + maxlength.ToString("N1") + " km lång. Riktning: " + rangedir + " Berg: ");
                                foreach (int oi in inrange)
                                    swname.Write(", [[" + gndict[oi].Name_ml + "]]");
                                swname.WriteLine();
                            }
                        }
                        //if (gnid == 2700827)
                        //    Console.ReadLine();
                    }
                }

            }


        }

        public static int rdf_getentity(string wordpar, string prefix)
        {
            string word = wordpar.Replace("<http://www.wikidata.org/entity/", "");
            word = word.Replace(prefix, "").Replace("c", "");
            return tryconvert(word);

        }

        public static string get_in_quotes(string wordpar)
        {
            int i1 = wordpar.IndexOf('"');
            if ((i1 < 0) || (i1 + 1 >= wordpar.Length))
                return "";

            int i2 = wordpar.IndexOf('"', i1 + 1);
            if (i2 < i1 + 2)
                return "";

            return wordpar.Substring(i1 + 1, i2 - i1 - 1);
        }

        public static rdfclass rdf_parse(string line)
        {
            rdfclass rc = new rdfclass();

            string[] words = line.Split('>');

            if (words.Length < 3) //not a triplet
                return rc;

            int o1 = rdf_getentity(words[0], "Q");
            if (o1 < 0) //triplet doesn't start with object id.
            {
                rc.objstring = words[0].Replace("<http://www.wikidata.org/entity/", "");
                //Console.WriteLine("objstring = " + rc.objstring);
            }


            rc.obj = o1;
            int prop = rdf_getentity(words[1], "P");
            if (prop > 0)
                rc.prop = prop;
            else
            {
                if (words[1].Contains("rdf-schema#"))
                {
                    if (words[1].Contains("subClassOf"))
                        rc.prop = 279;
                    else
                        Console.WriteLine(words[1]);
                }
                else if (words[1].Contains("ontology#"))
                {
                    if (words[1].Contains("latitude"))
                        rc.prop = 6250001;
                    else if (words[1].Contains("longitude"))
                        rc.prop = 6250002;
                }
            }

            //<http://www.wikidata.org/entity/Q7743> 
            //<http://www.w3.org/2000/01/rdf-schema#subClassOf> 
            //<http://www.w3.org/2002/07/owl#Class> .


            int o2 = rdf_getentity(words[2], "Q");
            if (o2 > 0)
                rc.objlink = o2;
            else
            {
                rc.value = get_in_quotes(words[2]);
                if (String.IsNullOrEmpty(rc.value))
                {
                    if (words[2].Contains("wikidata.org"))
                        rc.value = words[2].Replace("<http://www.wikidata.org/entity/", "").Trim();
                }
            }

            return rc;

        }

        public static bool search_rdf_tree(int target, int wdid, int depth)
        {
            int maxdepth = 10;
            //Console.WriteLine("search_rdf_tree " + target + " " + wdid);
            if (wdid == target)
            {
                Console.WriteLine("search_rdf_tree FOUND " + target.ToString() + ", " + wdid.ToString() + ", " + depth.ToString());
                return true;
            }
            if (depth > maxdepth)
                return false;
            if (!wdtree.ContainsKey(wdid))
                return false;
            foreach (int upl in wdtree[wdid].uplinks)
                if (search_rdf_tree(target, upl, depth + 1))
                    return true;
            return false;
        }

        public static void read_rdf_tree()
        {
            Console.WriteLine("read_rdf_tree");
            using (StreamReader sr = new StreamReader(geonamesfolder + "wikidata-taxonomy.nt"))
            {
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();
                    rdfclass rc = rdf_parse(line);
                    if (rc.obj < 0)
                        continue;
                    if (rc.prop != 279)
                        continue;
                    if (rc.objlink < 0)
                        continue;

                    if (!wdtree.ContainsKey(rc.obj))
                    {
                        wdtreeclass wtc = new wdtreeclass();
                        wdtree.Add(rc.obj, wtc);
                    }
                    //Console.WriteLine("Added " + rc.obj.ToString());
                    wdtree[rc.obj].uplinks.Add(rc.objlink);
                }
            }

            List<int> dummy = new List<int>();

            foreach (int wdid in wdtree.Keys)
                dummy.Add(wdid);
            foreach (int wdid in dummy)
            {
                foreach (int uplink in wdtree[wdid].uplinks)
                    if (wdtree.ContainsKey(uplink))
                        wdtree[uplink].downlinks.Add(wdid);
            }

            //using (StreamWriter sw = new StreamWriter("wdtree.txt"))
            //{
            //    foreach (int wdid in wdtree.Keys)
            //    {
            //        sw.WriteLine(wdid.ToString());
            //        sw.Write("up");
            //        foreach (int uplink in wdtree[wdid].uplinks)
            //            sw.Write(tabstring + uplink.ToString());
            //        sw.WriteLine();
            //        sw.Write("down");
            //        foreach (int downlink in wdtree[wdid].downlinks)
            //            sw.Write(tabstring + downlink.ToString());
            //        sw.WriteLine();
            //    }
            //}
            Console.WriteLine("read_rdf_tree done");

        }

        public static void test_article_coord()
        {
            while (true)
            {
                Console.Write("Page: ");
                string title = Console.ReadLine();
                Page oldpage = new Page(makesite, title);
                tryload(oldpage, 1);
                if (oldpage.Exists())
                {
                    double[] latlong = get_article_coord(oldpage);
                    Console.WriteLine(latlong[0].ToString() + "|" + latlong[1].ToString());
                }
            }
        }

        public static void country_center_map()
        {
            Page forkpage = new Page(makesite, "Användare:Lsjbot/Landcentrum");

            int ncountry = 0;
            foreach (int cgnid in countrydict.Keys)
            {
                read_geonames(countrydict[cgnid].iso);
                string cs = countrydict[cgnid].Name;
                List<coordclass> centers = new List<coordclass>();

                coordclass cc = new coordclass();
                cc.lat = gndict[cgnid].latitude;
                cc.lon = gndict[cgnid].longitude;
                centers.Add(cc);

                double latsum = 0;
                double lonsum = 0;
                int nsum = 0;
                double latppl = 0;
                double lonppl = 0;
                int nppl = 0;
                double latmax = -999;
                double latmin = 999;
                double lonmax = -999;
                double lonmin = 999;

                foreach (int gnid in gndict.Keys)
                {
                    latsum += gndict[gnid].latitude;
                    lonsum += gndict[gnid].longitude;
                    nsum++;
                    if (gndict[gnid].latitude > latmax)
                        latmax = gndict[gnid].latitude;
                    if (gndict[gnid].latitude < latmin)
                        latmin = gndict[gnid].latitude;
                    if (gndict[gnid].longitude > lonmax)
                        lonmax = gndict[gnid].longitude;
                    if (gndict[gnid].longitude < lonmin)
                        lonmin = gndict[gnid].longitude;

                    if (gndict[gnid].featureclass == 'P')
                    {
                        latppl += gndict[gnid].latitude;
                        lonppl += gndict[gnid].longitude;
                        nppl++;
                    }
                }

                coordclass csum = new coordclass();
                coordclass cppl = new coordclass();
                csum.lat = latsum / nsum;
                csum.lon = lonsum / nsum;
                cppl.lat = latppl / nppl;
                cppl.lon = lonppl / nppl;
                centers.Add(csum);
                centers.Add(cppl);

                coordclass cmid = new coordclass();
                cmid.lat = 0.5 * (latmax + latmin);
                cmid.lon = 0.5 * (lonmin + lonmax);
                centers.Add(cmid);

                if (locatordict.ContainsKey(cs) && !makeworldmaponly)
                {
                    int mapsize = 300;

                    string caption = countrydict[cgnid].Name_ml;
                    if (makelang == "sv")
                    {
                        string ifcollapsed = "";//" mw-collapsed";
                        string collapseintro = "{| class=\"mw-collapsible" + ifcollapsed + "\" data-expandtext=\"Visa karta\" data-collapsetext=\"Dölj karta\" style=\"float:right; clear:right;\"\n|-\n!\n|-\n|\n";
                        forkpage.text += collapseintro;
                    }
                    forkpage.text += mp(72) + "+|" + locatordict[cs].locatorname + "\n |caption = " + caption + "\n  |float = right\n  |width=" + mapsize.ToString() + "\n  | places =";
                    int inum = 0;
                    foreach (coordclass ccc in centers)
                    {
                        inum++;
                        forkpage.text += mp(72) + "~|" + locatordict[cs].locatorname + "| label = " + inum.ToString() + "| mark =Blue_pog.svg|position=right|background=white|lat=" + ccc.lat.ToString(culture_en) + "|long=" + ccc.lon.ToString(culture_en) + "}}\n";
                    }
                    forkpage.text += "}}\n";
                    if (makelang == "sv")
                        forkpage.text += "|}\n"; //collapse-end
                }

                gndict.Clear();
                ghostdict.Clear();
                ncountry++;
                if (ncountry % 10 == 0)
                    trysave(forkpage, 1,"Bot creation of country center map");
                //break;
            }

            trysave(forkpage, 2, "Bot creation of country center map");
        }

        public static void fill_catwd()
        {
            //populated places
            //public static Dictionary<string, string> catwdclass = new Dictionary<string, string>(); //from category to appropriate wd top class
            //public static Dictionary<string, List<string>> catwdinstance = new Dictionary<string, List<string>>(); //from category to list of appropriate wd instance_of

            catwdclass.Add("populated places", 486972); //"human settlement"
            catwdclass.Add("subdivision1", 56061);//administrative territorial entity
            catwdclass.Add("subdivision2", 1048835);//political territorial entity
            catwdclass.Add("subdivision3", 15916867);//administrative territorial entity of a single country
            catwdclass.Add("lakes", 23397); //lake
            catwdclass.Add("canals", 355304); //watercourse
            catwdclass.Add("streams", 355304); //watercourse
            catwdclass.Add("bays", 15324); //body of water
            catwdclass.Add("wetlands", 170321); //wetland
            catwdclass.Add("waterfalls", 34038); //waterfall
            catwdclass.Add("ice", 23392); //ice
            catwdclass.Add("default", 618123); //geographical object
            catwdclass.Add("landforms", 271669); //landform
            catwdclass.Add("plains", 160091); //plain
            catwdclass.Add("straits", 37901); //strait
            catwdclass.Add("military", 18691599); //military facility
            catwdclass.Add("coasts", 19817101); //coastal landform
            catwdclass.Add("aviation", 62447); //aerodrome
            catwdclass.Add("constructions", 811430); //construction
            catwdclass.Add("caves", 35509); //cave
            catwdclass.Add("islands", 23442); //island
            catwdclass.Add("mountains1", 8502); //mountains
            catwdclass.Add("mountains2", 1437459); //mountain system
            catwdclass.Add("hills", 8502); //mountains
            catwdclass.Add("volcanoes", 8502); //mountains
            catwdclass.Add("peninsulas", 271669); //landform
            catwdclass.Add("valleys", 271669); //landform
            catwdclass.Add("deserts", 271669); //landform
            catwdclass.Add("forests", 4421); //forest

        }

        public static void list_nativenames()
        {
            List<string> nativename_countries = new List<string>();
            //countries with special iw treatment
            nativename_countries.Add("EE");
            nativename_countries.Add("LT");
            nativename_countries.Add("LV");


            using (StreamWriter sw = new StreamWriter("nativenames-" + getdatestring() + ".txt"))
            {
                foreach (string nc in nativename_countries)
                {
                    int icountry = countryid[nc];
                    string nwiki = countrydict[icountry].nativewiki;
                    Console.WriteLine(nc + " " + nwiki);
                    int nnames = 0;

                    Dictionary<int, int> wddict = read_wd_dict(nc);

                    foreach (int gnid in wddict.Keys)
                    {
                        int wdid = wddict[gnid];
                        string artname = "";
                        XmlDocument cx = get_wd_xml(wdid);
                        if (cx != null)
                        {
                            Dictionary<string, string> rd = get_wd_sitelinks(cx);
                            foreach (string wiki in rd.Keys)
                            {
                                string ssw = wiki.Replace("wiki", "");
                                if (ssw == nwiki)
                                {
                                    artname = remove_disambig(rd[wiki]);
                                }
                                else if (ssw == makelang)
                                {
                                    artname = "";
                                    break;
                                }
                            }
                        }
                        if (!String.IsNullOrEmpty(artname))
                        {
                            sw.WriteLine(gnid.ToString() + tabstring + artname);
                            Console.WriteLine(gnid.ToString() + ", " + artname);
                            nnames++;
                        }

                    }
                    Console.WriteLine("nnames = " + nnames.ToString());
                }
            }

        }

        public static void list_missing_adm1()
        {
            using (StreamWriter sw = new StreamWriter("missing-adm1-" + makecountry + getdatestring() + ".txt"))
            {
                foreach (int gnid in gndict.Keys)
                {
                    if (gndict[gnid].featurecode == "ADM1")
                    {
                        if (!gndict[gnid].articlename.Contains("*"))
                        {
                            string country = "";
                            if (gndict.ContainsKey(gndict[gnid].adm[0]))
                                country = gndict[gndict[gnid].adm[0]].Name_ml;
                            sw.WriteLine(gnid.ToString() + tabstring + country + tabstring + gndict[gnid].Name_ml);
                            Console.WriteLine(gnid.ToString() + tabstring + gndict[gnid].Name_ml);
                        }
                    }
                }
            }
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        public static void set_folders()
        {
            Console.WriteLine(Environment.MachineName);
            if (Environment.MachineName == "HP2011")
            {
                geonamesfolder = @"C:\dotnwb3\Geonames\";
                extractdir = @"O:\dotnwb3\extract\";
            }
            else if (Environment.MachineName == "KOMPLETT2015")
            {
                geonamesfolder = @"D:\dotnwb3\Geonames\";
                extractdir = @"D:\dotnwb3\extract\";
            }
            else
            {
                geonamesfolder = @"C:\dotnwb3\Geonames\";
                extractdir = @"C:\dotnwb3\extract\";
            }
            Console.WriteLine(geonamesfolder);
            Console.WriteLine(extractdir);

        }

        public static void parse_args(string[] args)
        {
            foreach (string s in args)
            {
                string[] words = s.Split(':');
                if (words.Length >= 2)
                {
                    switch (words[0])
                    {
                        case "makelang":
                            makelang = words[1];
                            break;
                        case "makecountry":
                            makecountry = words[1];
                            break;
                        case "resume_at":
                            resume_at = tryconvert(words[1]);
                            break;
                        case "resume_at_fork":
                            resume_at_fork = words[1].Replace('_',' ');
                            break;
                        case "password":
                            password = words[1];
                            break;

                        case "createclass": 
                            createclass = words[1][0];
                            break;
                        case "createexceptclass":
                            createexceptclass = words[1][0];
                            break;
                        case "createfeature":
                            createfeature = words[1];
                            break;
                        case "createexceptfeature":
                            createexceptfeature= words[1];
                            break;
                        case "createexceptcategory":
                            createexceptcategory = words[1];
                            break;
                        case "createcategory":
                            createcategory = words[1];
                            break;
                        case "createunit":
                            createunit = tryconvert(words[1]);
                            break;
                        case "createexceptunit":
                            createexceptunit = tryconvert(words[1]);
                            break;

                        case "makearticles": makearticles = (words[1].ToUpper() == "T"); break;
                        case "makespecificarticles": makespecificarticles = (words[1].ToUpper() == "T"); break;
                        case "remakearticleset": remakearticleset = (words[1].ToUpper() == "T"); break;
                        case "altnamesonly": altnamesonly = (words[1].ToUpper() == "T"); break;
                        case "makefork": makefork = (words[1].ToUpper() == "T"); break;
                        case "checkdoubles": checkdoubles = (words[1].ToUpper() == "T"); break;
                        case "checkwikidata": checkwikidata = (words[1].ToUpper() == "T"); break;
                        case "makeislands": makeislands = (words[1].ToUpper() == "T"); break;
                        case "makelakes": makelakes = (words[1].ToUpper() == "T"); break;
                        case "makerivers": makerivers = (words[1].ToUpper() == "T"); break;
                        case "makeranges": makeranges = (words[1].ToUpper() == "T"); break;
                        case "verifygeonames": verifygeonames = (words[1].ToUpper() == "T"); break;
                        case "verifywikidata": verifywikidata = (words[1].ToUpper() == "T"); break;
                        case "verifyislands": verifyislands = (words[1].ToUpper() == "T"); break;
                        case "verifylakes": verifylakes = (words[1].ToUpper() == "T"); break;
                        case "makealtitude": makealtitude = (words[1].ToUpper() == "T"); break;
                        case "maketranslit": maketranslit = (words[1].ToUpper() == "T"); break;
                        case "makeworldmaponly": makeworldmaponly = (words[1].ToUpper() == "T"); break;
                        case "statisticsonly": statisticsonly = (words[1].ToUpper() == "T"); break;
                        case "savefeaturelink": savefeaturelink = (words[1].ToUpper() == "T"); break;
                        case "savewikilinks": savewikilinks = (words[1].ToUpper() == "T"); break;
                        case "saveadmlinks": saveadmlinks = (words[1].ToUpper() == "T"); break;
                        case "manualcheck": manualcheck = (words[1].ToUpper() == "T"); break;
                        case "listnative": listnative = (words[1].ToUpper() == "T"); break;
                        case "forkduplicates": forkduplicates = (words[1].ToUpper() == "T"); break;
                        case "fixsizecats": fixsizecats = (words[1].ToUpper() == "T"); break;
                        case "testnasa": testnasa = (words[1].ToUpper() == "T"); break;
                        case "retrofitnasa": retrofitnasa = (words[1].ToUpper() == "T"); break;
                        case "checkminutes": checkminutes = (words[1].ToUpper() == "T"); break;
                        case "countrycenters": countrycenters = (words[1].ToUpper() == "T"); break;
                        case "prefergeonamespop": prefergeonamespop = (words[1].ToUpper() == "T"); break;
                        case "makedoubles": makedoubles = (words[1].ToUpper() == "T"); break;
                        case "overwrite": overwrite = (words[1].ToUpper() == "T"); break;
                        case "reallymake": reallymake = (words[1].ToUpper() == "T"); break;
                        case "pauseaftersave": pauseaftersave = (words[1].ToUpper() == "T"); break;
                        default:
                            Console.WriteLine("Unknown argument " + s);
                            break;

                    }
                }
                else
                {
                    switch (s)
                    {
                        case "makearticles": makearticles = true; break;
                        case "makespecificarticles": makespecificarticles = true; break;
                        case "remakearticleset": remakearticleset = true; break;
                        case "altnamesonly": altnamesonly = true; break;
                        case "makefork": makefork = true; break;
                        case "checkdoubles": checkdoubles = true; break;
                        case "checkwikidata": checkwikidata = true; break;
                        case "makeislands": makeislands = true; break;
                        case "makelakes": makelakes = true; break;
                        case "makerivers": makerivers = true; break;
                        case "makeranges": makeranges = true; break;
                        case "verifygeonames": verifygeonames = true; break;
                        case "verifywikidata": verifywikidata = true; break;
                        case "verifyislands": verifyislands = true; break;
                        case "verifylakes": verifylakes = true; break;
                        case "makealtitude": makealtitude = true; break;
                        case "maketranslit": maketranslit = true; break;
                        case "makeworldmaponly": makeworldmaponly = true; break;
                        case "statisticsonly": statisticsonly = true; break;
                        case "savefeaturelink": savefeaturelink = true; break;
                        case "savewikilinks": savewikilinks = true; break;
                        case "saveadmlinks": saveadmlinks = true; break;
                        case "manualcheck": manualcheck = true; break;
                        case "listnative": listnative = true; break;
                        case "forkduplicates": forkduplicates = true; break;
                        case "fixsizecats": fixsizecats = true; break;
                        case "testnasa": testnasa = true; break;
                        case "retrofitnasa": retrofitnasa = true; break;
                        case "checkminutes": checkminutes = true; break;
                        case "countrycenters": countrycenters = true; break;
                        case "prefergeonamespop": prefergeonamespop = true; break;
                        case "makedoubles": makedoubles = true; break;
                        case "overwrite": overwrite = true; break;
                        case "reallymake": reallymake = true; break;
                        case "pauseaftersave": pauseaftersave = true; break;
                        default:
                            Console.WriteLine("Unknown argument " + s);
                            Console.WriteLine("<cr>");
                            Console.ReadLine();
                            break;

                    }

                }
            }
        }

        static void Main(string[] args)
        {
            foreach (string arg in args)
                Console.WriteLine(arg);

            parse_args(args);

            DateTime starttime = DateTime.Now;
            if (String.IsNullOrEmpty(password))
            {
                Console.Write("Password: ");
                password = Console.ReadLine();
            }

            set_folders();

            //convert_shapelist("ne_10m_admin_0_countries");
            //convert_shapelist("glwd_1");
            //convert_shapelist("glwd_2");
            //Console.ReadLine();

            makesite = new Site("https://" + makelang + ".wikipedia.org", botname, password);
            //wdsite = new Site("http://wikidata.org", botname, password);
            if (makearticles)
            {
                ensite = new Site("https://en.wikipedia.org", botname, password);
                cmsite = new Site("https://commons.wikimedia.org", botname, password);
            }

            //Wikidata login:

            get_webpage("https://www.wikidata.org/w/api.php?action=login&lgname=" + botname + "&lgpassword=" + password);

            makesite.defaultEditComment = mp(60);
            makesite.minorEditByDefault = false;

            

            if (makearticles || makefork)
                pausetime = 5;
            else
                pausetime = 7;

            stats.SetMilestone(10000, makesite);

            if (makelang == "sv")
            {
                culture = CultureInfo.CreateSpecificCulture("sv-SE");
                nfi = culture.NumberFormat;
                nfi.NumberGroupSeparator = "&nbsp;";
                //nfi_space = culture.NumberFormat.Copy();
                nfi_space.NumberGroupSeparator = " ";
                locatoringeobox = true;  //only works in Swedish!
            }
            else
            {
                culture = CultureInfo.CreateSpecificCulture("en-US");
                nfi = culture.NumberFormat;
                nfi_space = culture.NumberFormat;
                locatoringeobox = false;  //only works in Swedish!
            }
            nfi_en.NumberGroupSeparator = "";

            if (makedoubles)
            {
                doubleprefix = mp(13) + botname;
                if (makelang == "sv")
                    doubleprefix += "/Dubletter/";
                else
                    doubleprefix += "/Duplicates/";
            }

            string[] makecountries = makecountry.Split(',');
            //string[] makecountrynames = makecountryname.Split(',');
            //string[] makecountrywikis = makecountrywiki.Split(',');


            //==============================
            // Read country-independent stuff:
            //==============================

            fill_propdict();
            fill_catwd();
            fill_cyrillic();
            fill_donecountries();
            read_languageiso();
            read_featurecodes();
            read_adm1();
            read_adm2();
            read_country_info();

            if (saveadmlinks)
                read_adm();
            read_locatorlist();

            if (countrycenters)
            {
                country_center_map();
                Console.ReadLine();
            }

            if (makearticles || testnasa || retrofitnasa)
                read_nasa();

            //fix_positionmaps();
            //get_lang_iw("ceb");
            //get_country_iw("sv");

            read_categories();
            read_catstat();

            int mclength = makecountries.Length;

            if (makecountry == "")
                mclength = 1;

            //==============================
            // Loop over countries:
            //==============================

            for (int icountry = 0; icountry < mclength; icountry++)
            {
                if (makecountry != "")
                {
                    makecountry = makecountries[icountry];
                    makecountryname = countrydict[countryid[makecountry]].Name;//makecountrynames[icountry];
                    //makecountrywiki = makecountrywikis[icountry];
                    anomalyheadline = false;
                    conflictheadline = false;
                }

                //==============================
                // Read country-dependent stuff:
                //==============================

                read_adm();
                read_timezone();

                if (!makefork && !checkdoubles && !forkduplicates)
                    read_geonames(makecountry);

                if (makearticles && !makespecificarticles && !remakearticleset) //Set off wdid thread
                {
                    Console.WriteLine("Thread starting branch");
                    resume_at_wdid = resume_at;
                    ThreadStart ts_wdid = new ThreadStart(fill_wdid_buffer);
                    Thread wdid_thread = new Thread(ts_wdid);
                    wdid_thread.Start();
                    Console.WriteLine("After thread start, back in main thread");
                    //Console.ReadLine();
                }

                else if (checkdoubles)
                {
                    //countries with special treatment in getadmlabel
                    read_geonames("MY");
                    read_geonames("GB");
                    read_geonames("RU");

                    read_existing_coord();
                    if (makelang == "sv")
                        read_existing_adm1();
                }

                if (makearticles || checkwikidata || makeislands || makelakes || makerivers || makeranges || retrofitnasa)
                {
                    if (firstround)
                        read_geoboxes();
                    fill_kids_features();
                    //if (!makeislands && !makelakes && !makeranges)
                    //{
                    read_artname();
                    read_altnames();
                    fix_names();
                    //}

                    if (manualcheck)
                        list_missing_adm1();
                }



                //==============================
                // Do stuff:
                //==============================


                if (makearticles)
                {
                    if (makespecificarticles)
                        make_specific_articles();
                    else if (remakearticleset)
                        remake_article_set();
                    else
                        make_articles();
                }

                if (altnamesonly)
                {
                    read_altnames();
                    read_artname();
                    add_nameforks();
                    list_nameforks();
                }

                if (maketranslit)
                {
                    read_altnames();
                    make_translit();
                }

                if (checkdoubles)
                {
                    read_altnames();
                    read_artname();
                    check_doubles();
                }

                if (checkwikidata) //identify wd links from geonames
                    check_wikidata();

                if (verifywikidata) //doublecheck geonames links in wikidata
                    verify_wd();

                if (makeislands)
                    check_islands();

                if (makelakes)
                    make_lakes();

                if (makerivers)
                    make_rivers();

                if (makeranges)
                    make_ranges();

                if (verifygeonames)
                    verify_geonames();

                if (makealtitude)
                    make_altitude_files();

                if (makefork)
                {
                    //read_altnames();
                    read_artname();
                    makeforkpages();
                }

                if (forkduplicates)
                    find_duplicate_forks();

                if (listnative)
                    list_nativenames();

                if (fixsizecats)
                    fix_sizecats2();

                if (testnasa)
                    test_nasa();

                if (retrofitnasa)
                    retrofit_nasa();

                //if ( makeworldmaponly )
                //    makeworldmap();

                if (statisticsonly)
                {
                    fchist.PrintSHist();
                    Console.WriteLine("=================================Print bad");
                    fcbad.PrintSHist();
                    //Console.WriteLine("=================================Print large");
                    //fchist.PrintLarge(1000);

                    evarhist.SetBins(0.0, 500000.0, 10);
                    slope1hist.SetBins(0.0, 30.0, 30);
                    slope5hist.SetBins(0.0, 30.0, 30);
                    slope5hist.SetBins(0.0, 50.0, 50);
                    slopermshist.SetBins(0.0, 50.0, 50);
                    elevdiffhist.SetBins(-500.0, 500.0, 100);
                    foreach (int gnid in gndict.Keys)
                    {
                        string tt = get_terrain_type(gnid, 10);
                        string ttext = terrain_text(tt, gnid);
                        Console.WriteLine(ttext);
                        terraintexthist.Add(ttext.Replace(gndict[gnid].Name_ml, "XXX"));
                    }

                    Console.WriteLine("gndict: " + gndict.Count.ToString());
                    evarhist.PrintDHist();
                    Console.WriteLine("Slope1:");
                    slope1hist.PrintDHist();
                    Console.WriteLine("Slope5:");
                    slope5hist.PrintDHist();
                    Console.WriteLine("Slope/RMS:");
                    slopermshist.PrintDHist();
                    ndirhist.PrintIHist();
                    nsameterrhist.PrintIHist();
                    terrainhist.PrintSHist();
                    terraintexthist.PrintSHist();


                    //fclasshist.PrintSHist();
                    //fcathist.PrintSHist();

                    //elevdiffhist.PrintDHist();
                    //foreach (int gnid in gndict.Keys)
                    //{
                    //    get_overrep(gnid,10.0);
                    //}
                    //foverrephist.PrintSHist();
                }

                firstround = false;
                gndict.Clear();
                ghostdict.Clear();
                wdid_buffer.Clear();
            }

            if (resume_at > 0)
                Console.WriteLine("Never reached resume_at");

            DateTime endtime = DateTime.Now;

            Console.WriteLine("starttime = " + starttime.ToString());
            Console.WriteLine("endtime = " + endtime.ToString());


        }
    }
}
