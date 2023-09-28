using System;
using System.Collections.Generic;
using System.Text;
namespace Framework
{
    using RoutingTable = Dictionary<string, ControllerAction>;
    // Lưu ý khai báo delegate này là khai báo kiểu, nằm trong namespace
    /// <summary>
    /// delegate này đại diện cho tất cả các phương thức có:
    /// - kiểu ra là void,
    /// - danh sách tham số vào là (Parameter)
    /// </summary>
    /// <param name="parameter"></param>
    public delegate void ControllerAction(Parameter parameter = null);
    /// <summary>
    /// lớp cho phép ánh xạ truy vấn với phương thức
    /// </summary>
    public class Router
    {
        /// <summary>
        /// lớp xử lý truy vấn
        /// </summary>
        private class Request
        {
            /// <summary>
            /// thành phần lệnh của truy vấn
            /// </summary>
            public string Route { get; private set; }
            /// <summary>
            /// thành phần tham số của truy vấn
            /// </summary>
            public Parameter Parameter { get; private set; }
            public Request(string request)
            {
                Analyze(request);
            }
            /// <summary>
            /// phân tích truy vấn để tách ra thành phần lệnh và thành phần tham số
            /// </summary>
            /// <param name="request"></param>
            private void Analyze(string request)
            {
                // tìm xem trong chuỗi truy vấn có tham số hay không
                var firstIndex = request.IndexOf('?');
                // trườn hợp truy vấn không chứa tham số
                if (firstIndex < 0)
                {
                    Route = request.ToLower().Trim();
                }
                // trường hợp truy vấn chứa tham số
                else
                {
                    // nếu chuỗi lối (chỉ chứa tham số, không chứa route)
                    if (firstIndex <= 1) throw new Exception("Invalid request parameter");
                    // cắt chuỗi truy vấn lấy mốc là ký tự ?
                    // sau phép toán này thu được mảng 2 phần tử: thứ nhất là route, thứ hai là chuỗi parameter
                    var tokens = request.Split(new[] { '?' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    // route là thành phần lệnh của truy vấn
                    Route = tokens[0].Trim().ToLower();
                    // parameter là thành phần tham số của truy vấn
                    var parameterPart = request.Substring(firstIndex + 1).Trim();
                    Parameter = new Parameter(parameterPart);
                }
            }
        }
        // nhóm 3 lệnh dưới đây biến Router thành một singleton
        private static Router _instance;
        private Router()
        {
            _routingTable = new RoutingTable();
            _helpTable = new Dictionary<string, string>();
        }
        // để ý: constructor là private
        // người sử dụng class thông qua property này để truy xuất các phương thức của class
        // chỉ khi nào _instance == null mới tạo object. Một khi đã tạo object, _instance sẽ
        // không có giá trị null nữa.
        // vì là biến static, _instance một khi được khởi tạo sẽ tồn tại suốt chương trình
        public static Router Instance => _instance ?? (_instance = new Router());
        // lưu ý: ở đây đang sử dụng alias của Dictionary<string, ControllerAction> cho ngắn gọn
        private readonly RoutingTable _routingTable;
        private readonly Dictionary<string, string> _helpTable;
        public string GetRoutes()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var k in _routingTable.Keys)
                sb.AppendFormat("{0}, ", k);
            return sb.ToString();
        }
        public string GetHelp(string key)
        {
            if (_helpTable.ContainsKey(key))
                return _helpTable[key];
            else
                return "Documentation not ready yet!";
        }
        /// <summary>
        /// đăng ký một route mới, mỗi route ánh xạ một chuỗi truy vấn với một phương thức
        /// </summary>
        /// <param name="route"></param>
        /// <param name="action"></param>
        public void Register(string route, ControllerAction action, string help = "")
        {
            // nếu _routingTable đã chứa route này thì bỏ qua
            if (!_routingTable.ContainsKey(route))
            {
                _routingTable[route] = action;
                _helpTable[route] = help;
            }
        }
        /// <summary>
        /// phân tích truy vấn và gọi phương thức tương ứng với chuỗi truy vấn
        /// <para>chuỗi truy vấn bao gồm hai phần: route và parameter, phân tách bởi ký tự ?</para>
        /// </summary>
        /// <param name="request">chuỗi truy vấn, bao gồm hai phần: 
        /// route, paramete; phân tách bởi ký tự ?</param>
        public void Forward(string request)
        {
            var req = new Request(request);
            if (!_routingTable.ContainsKey(req.Route))
                throw new Exception("Command not found!");
            if (req.Parameter == null)
                _routingTable[req.Route]?.Invoke();
            else
                _routingTable[req.Route]?.Invoke(req.Parameter);
        }

        // Code của lớp Request (làm trong buổi trước) nằm ở đây và tạm ẩn đi cho gọn         
    }
}