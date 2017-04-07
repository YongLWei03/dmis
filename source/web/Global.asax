<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {

     }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  在应用程序关闭时运行的代码

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // 在出现未处理的错误时运行的代码
        //WebLog.InsertLog("错误", "", "系统错误");
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // 在新会话启动时运行的代码
        //查找本地化信息
        System.Configuration.ConfigurationSection sec = ( System.Configuration.ConfigurationSection)System.Configuration.ConfigurationManager.GetSection("globalization");
        if (sec == null)
        {
            //Session["UICulture"] = "en-us";
            //Session["Culture"] = "en-us";
        }
        else
        {
            //Session["UICulture"] = sec.["uiCulture"];
            //Session["Culture"] = sec["culture"];
        }
    }

    void Session_End(object sender, EventArgs e) 
    {
        // 在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
        // 或 SQLServer，则不会引发该事件。

    }
       
</script>
