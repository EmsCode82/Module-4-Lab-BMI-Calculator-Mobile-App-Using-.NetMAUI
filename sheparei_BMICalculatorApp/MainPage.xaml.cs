using Microsoft.Maui.Controls;

namespace sheparei_BMICalculatorApp;

public partial class MainPage : ContentPage
{
    private string selectedGender = "Male";

    public MainPage()
    {
        InitializeComponent();
        UpdateSliderLabels();
        
        MaleButton.Opacity = 1.0;
        FemaleButton.Opacity = 0.5;
    }

    private void OnGenderSelected(object sender, EventArgs e)
    {
        if (sender == MaleButton)
        {
            selectedGender = "Male";
            MaleButton.Opacity = 1.0;
            FemaleButton.Opacity = 0.5;
        }
        else if (sender == FemaleButton)
        {
            selectedGender = "Female";
            MaleButton.Opacity = 0.5;
            FemaleButton.Opacity = 1.0;
        }
    }

    private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        UpdateSliderLabels();
    }

    private void UpdateSliderLabels()
    {
        HeightValue.Text = $"{(int)HeightSlider.Value} inches";
        WeightValue.Text = $"{(int)WeightSlider.Value} lbs";
    }

    private void OnCalculateClicked(object sender, EventArgs e)
    {
        double weight = WeightSlider.Value;
        double height = HeightSlider.Value;

        if (height <= 0)
        {
            DisplayAlert("Error", "Height must be greater than 0.", "OK");
            return;
        }

        double bmi = (weight * 703) / (height * height);
        bmi = Math.Round(bmi, 2);

        string status = GetHealthStatus(bmi, selectedGender);
        string recommendations = GetRecommendations(status);

        BmiLabel.Text = $"BMI: {bmi}";
        StatusLabel.Text = $"Health Status: {status}";
        RecommendationsLabel.Text = recommendations;

        ResultsFrame.IsVisible = true;
    }

    private string GetHealthStatus(double bmi, string gender)
    {
        if (gender == "Male")
        {
            if (bmi < 18.5) return "Underweight";
            else if (bmi < 25) return "Normal weight";
            else if (bmi < 30) return "Overweight";
            else return "Obese";
        }
        else
        {
            if (bmi < 18) return "Underweight";
            else if (bmi < 24) return "Normal weight";
            else if (bmi < 29) return "Overweight";
            else return "Obese";
        }
    }

    private string GetRecommendations(string status)
    {
        switch (status)
        {
            case "Underweight": return "Increase calorie intake with nutrient-rich foods (e.g., nuts, lean protein, whole grains). Incorporate strength training to build muscle mass. Consult a nutritionist if needed.";
            case "Normal weight": return "Maintain a balanced diet with proteins, healthy fats, and fiber. Stay physically active with at least 150 minutes of exercise per week. Keep regular check-ups.";
            case "Overweight": return "Reduce processed foods and focus on portion control. Engage in regular aerobic exercises (e.g., jogging, swimming) and strength training. Drink plenty of water and track your progress.";
            case "Obese": return "Consult a doctor for personalized guidance. Start with low-impact exercises (e.g., walking, cycling). Follow a structured weight-loss meal plan and consider behavioral therapy for lifestyle changes. Avoid sugary drinks and maintain a consistent sleep schedule.";
            default: return "";
        }
    }
}