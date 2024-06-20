namespace HAT_F_api.CustomModels
{
    public class ApiArgumentWrapper
    { 
        public object Value {  get; set; }

        public ApiArgumentWrapper() 
        { 
        }

        public ApiArgumentWrapper(object value) 
        {
            this.Value = value;
        }
    }
}
